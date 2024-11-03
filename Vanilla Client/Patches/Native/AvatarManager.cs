using MelonLoader;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using VRC.Core;
using Vanilla.Utils;
using Vanilla.Modules;

namespace Vanilla.Patches.Native
{
    internal class AvatarManager : VanillaPatches
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void VoidDelegate(IntPtr thisPtr, IntPtr nativeMethodInfo);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr ObjectInstantiateDelegate(IntPtr assetPtr, Vector3 pos, Quaternion rot, byte allowCustomShaders, byte isUI, byte validate, IntPtr nativeMethodPointer);


        internal override void Patch()
        {
            
           if  (Environment.UserName != "ZuxiOT")
            {
                Log("Anticrash", Environment.UserName + " ANTICRASH REMOVED VIA REQUEST BY STAFF SORRY...");
                return;// Dev("Anticrash", Environment.UserName + " ANTICRASH REMOVED VIA REQUEST BY STAFF SORRY...");
            }
            // Object Instantiated Hook
            var matchingMethods = typeof(AssetManagement)
                            .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly).Where(it =>
                                it.Name.StartsWith("Method_Public_Static_Object_Object_Vector3_Quaternion_Boolean_Boolean_Boolean_") && it.GetParameters().Length == 6).ToList();

            foreach (var matchingMethod in matchingMethods)
            {
                ObjectInstantiateDelegate originalInstantiateDelegate = null;

                ObjectInstantiateDelegate replacement = (assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer) =>
                    ObjectInstantiatePatch(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer, originalInstantiateDelegate);

                NativePatchUtils.NativePatch(matchingMethod, out originalInstantiateDelegate, replacement);
            }

            // Avatar Manager Caching Hook
            foreach (var nestedType in typeof(VRCAvatarManager).GetNestedTypes())
            {
                var moveNext = nestedType.GetMethod("MoveNext");
                if (moveNext == null) continue;
                var avatarManagerField = nestedType.GetProperties().SingleOrDefault(it => it.PropertyType == typeof(VRCAvatarManager));
                if (avatarManagerField == null) continue;

                MelonDebug.Msg($"Patching UniTask type {nestedType.FullName}");

                var fieldOffset = (int)IL2CPP.il2cpp_field_get_offset((IntPtr)UnhollowerUtils
                    .GetIl2CppFieldInfoPointerFieldForGeneratedFieldAccessor(avatarManagerField.GetMethod)
                    .GetValue(null));

                unsafe
                {
                    var originalMethodPointer = *(IntPtr*)(IntPtr)UnhollowerUtils.GetIl2CppMethodInfoPointerFieldForGeneratedMethod(moveNext).GetValue(null);

                    originalMethodPointer = XrefScannerLowLevel.JumpTargets(originalMethodPointer).First();

                    VoidDelegate originalDelegate = null;

                    void TaskMoveNextPatch(IntPtr taskPtr, IntPtr nativeMethodInfo)
                    {
                        var avatarManager = *(IntPtr*)(taskPtr + fieldOffset - 16);
                        using (new AvatarManagerCookie(new VRCAvatarManager(avatarManager)))
                            originalDelegate(taskPtr, nativeMethodInfo);
                    }

                    var patchDelegate = new VoidDelegate(TaskMoveNextPatch);

                    NativePatchUtils.NativePatch(originalMethodPointer, out originalDelegate, patchDelegate);
                }
            }
        }

        private static IntPtr ObjectInstantiatePatch(IntPtr assetPtr, Vector3 pos, Quaternion rot,
            byte allowCustomShaders, byte isUI, byte validate, IntPtr nativeMethodPointer, ObjectInstantiateDelegate originalInstantiateDelegate)
        {
            if (AvatarManagerCookie.CurrentManager == null || assetPtr == IntPtr.Zero)
                return originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);

            var avatarManager = AvatarManagerCookie.CurrentManager;
            var vrcPlayer = avatarManager.field_Private_VRCPlayer_0;
            if (vrcPlayer == null) return originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);

            if (vrcPlayer == VRCPlayer.field_Internal_Static_VRCPlayer_0) // never apply to self
                return originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);

            var go = new UnityEngine.Object(assetPtr).TryCast<GameObject>();
            if (go == null)
                return originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);

            var wasActive = go.activeSelf;
            go.SetActive(false);
            var result = originalInstantiateDelegate(assetPtr, pos, rot, allowCustomShaders, isUI, validate, nativeMethodPointer);
            go.SetActive(wasActive);
            if (result == IntPtr.Zero) return result;
            var instantiated = new GameObject(result);
            try
            {
                if (!Anticrash.ProcessAvatar(instantiated, vrcPlayer)) {
                    instantiated = new GameObject();
                }

            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Exception when cleaning avatar: {ex}");
            }

            return result;
        }

        private readonly struct AvatarManagerCookie : IDisposable
        {
            internal static VRCAvatarManager CurrentManager;
            private readonly VRCAvatarManager myLastManager;

            public AvatarManagerCookie(VRCAvatarManager avatarManager)
            {
                myLastManager = CurrentManager;
                CurrentManager = avatarManager;
            }
            public void Dispose()
            {
                CurrentManager = myLastManager;
            }
        }
    }
}
