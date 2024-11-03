using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib;

namespace Vanilla.Utils
{
    public static class NativePatchUtils
    {
        private static readonly List<Delegate> ourPinnedDelegates = new();
        private static readonly Dictionary<IntPtr, IntPtr> ourOriginalPointers = new();

        public static void NativePatch<T>(MethodInfo original, out T callOriginal, MethodInfo patch)
            where T : MulticastDelegate
        {
            var patchDelegate = (T)Delegate.CreateDelegate(typeof(T), patch);
            NativePatch(original, out callOriginal, patchDelegate);
        }

        public static void NativePatch<T>(IntPtr originalPointer, out T callOriginal, MethodInfo patch,
            string? context = null) where T : MulticastDelegate
        {
            var patchDelegate = (T)Delegate.CreateDelegate(typeof(T), patch);
            NativePatch(originalPointer, out callOriginal, patchDelegate, context);
        }

        public static unsafe void NativePatch<T>(MethodInfo original, out T callOriginal, T patchDelegate)
            where T : MulticastDelegate
        {
            if (original == null)
            {
                throw new ArgumentNullException(nameof(original));
            }

            var originalPointer = *(IntPtr*)(IntPtr)UnhollowerUtils
                .GetIl2CppMethodInfoPointerFieldForGeneratedMethod(original).GetValue(null);
            NativePatch(originalPointer, out callOriginal, patchDelegate, original.FullDescription());
        }

        public static unsafe void NativePatch<T>(IntPtr originalPointer, out T callOriginal, T patchDelegate,
            string? context = null) where T : MulticastDelegate
        {
            ourPinnedDelegates.Add(patchDelegate);

            var patchPointer = Marshal.GetFunctionPointerForDelegate(patchDelegate);
            MelonUtils.NativeHookAttach((IntPtr)(&originalPointer), patchPointer);
            if (ourOriginalPointers.ContainsKey(originalPointer))
            {
                MelonLogger.Warning(
                    $"Method {context ?? patchDelegate.Method.FullDescription()} has multiple native patches within single mod. Bug?");
            }

            ourOriginalPointers[originalPointer] = patchPointer;
            callOriginal = Marshal.GetDelegateForFunctionPointer<T>(originalPointer);
        }

        public static unsafe void UnpatchAll()
        {
            foreach (var keyValuePair in ourOriginalPointers)
            {
                var pointer = keyValuePair.Key;
                MelonUtils.NativeHookDetach((IntPtr)(&pointer), keyValuePair.Value);
            }

            ourOriginalPointers.Clear();
            ourPinnedDelegates.Clear();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NativeString
        {
            public IntPtr Data;
            public long Capacity;
            public long Unknown;
            public long Length;
            public int Unknown2;
        }
    }
}