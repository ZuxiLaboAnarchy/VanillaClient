
using Vanilla.Modules;
using Vanilla.Patches;
using System.Runtime.CompilerServices;
using static Vanilla.Utils.Performance;
using UnityEngine;

namespace Vanilla
{
    internal class Main
    {
        [CompilerGenerated]
        internal protected static void CallOnStart()
        {
            FileHelper.Setup();

            PatchManager.Patch();

            ModuleManager.InitModules();

            AssetLoader.LoadAssetBundle();


            
            try { for (int i = 0; i < PatchManager.Patches.Count; i++) PatchManager.Patches[i].Patch(); } catch (Exception e) { ExceptionHandler("Patches", e); }

            Log("Patch Manager", $"Patched {PatchManager.PatchedMethods} Methods", ConsoleColor.Green);

            try { for (int i = 0; i < ModuleManager.Modules.Count; i++) ModuleManager.Modules[i].Start(); } catch (Exception e) { ExceptionHandler("Modules", e); }

            Dev("OnStart", "On App Start Complete");

            Log("Performance", $"Client Init Took: " + GetProfiling("OnStart").ToString() + " ms", ConsoleColor.Green);

        }
        public class Nig
        {
            public static bool flytoggle = false;
        }
        internal protected static void CallOnGUI()
        {
            ModuleManager.OnGUI();
        }

        private Transform camera() => GameObject.Find("Camera (eye)").transform;
        internal protected static void CallOnUpdate()
        {
            ModuleManager.Update();
            if (!Nig.flytoggle) return;

            if (VRC.Player.prop_Player_0 == null) return;

            float flyspeed = Input.GetKey(KeyCode.LeftShift) ? Time.deltaTime * 50 : Time.deltaTime * 25;
            if (VRC.Player.prop_Player_0.field_Private_VRCPlayerApi_0.IsUserInVR())
            {
                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0f)
                    VRC.Player.prop_Player_0.transform.position += camera().up * flyspeed;
                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0f)
                    VRC.Player.prop_Player_0.transform.position -= camera().up * flyspeed;

                if (Input.GetAxis("Vertical") != 0f)
                    VRC.Player.prop_Player_0.transform.position += camera().forward * (flyspeed * Input.GetAxis("Vertical"));

                if (Input.GetAxis("Horizontal") != 0f)
                    VRC.Player.prop_Player_0.transform.position += camera().transform.right * (flyspeed * Input.GetAxis("Horizontal"));
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                    VRC.Player.prop_Player_0.transform.position += camera().forward * flyspeed;

                if (Input.GetKey(KeyCode.S))
                    VRC.Player.prop_Player_0.transform.position -= camera().forward * flyspeed;

                if (Input.GetKey(KeyCode.A))
                    VRC.Player.prop_Player_0.transform.position -= camera().right * (flyspeed / 2);

                if (Input.GetKey(KeyCode.D))
                    VRC.Player.prop_Player_0.transform.position += camera().right * (flyspeed / 2);

                if (Input.GetKey(KeyCode.LeftControl))
                    VRC.Player.prop_Player_0.transform.position -= camera().up * (flyspeed / 2);

                if (Input.GetKey(KeyCode.Space))
                    VRC.Player.prop_Player_0.transform.position += camera().up * (flyspeed / 2);
            }
        }
        internal protected static void CallOnLateStart()
        {
            ModuleManager.LateStart();
        }
        internal protected static void CallOnGameQuit()
        {
            PatchManager.Stop();
            ModuleManager.Stop();

            LogHandler.Pop();


        }

    }
}
