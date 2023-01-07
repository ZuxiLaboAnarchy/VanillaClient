using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Modules;
using Vanilla.Wrappers;
using VRC;

namespace Vanilla.QM.Menu
{
    internal class GeneralMenu
    {
        private static IntPtr _hwnd;

        internal static void InitMenu(QMNestedButton GeneralMenu)
        {

            var FlyToggle = new QMToggleButton(GeneralMenu, 1, 0, "Fly", delegate
            {

                FlyManager.ToggleFly();

            }, delegate
            {

                FlyManager.ToggleFly();

            }, "I believe i can Fly");


            var Hidequest = new QMToggleButton(GeneralMenu, 2, 0, "Hidequest", delegate
            {
                var playes = GetAllPlayers();
                for (int i = 0; i < playes._size; i++)
                {
                    if (playes[i].prop_APIUser_0.last_platform != "standalonewindows" && !playes[i].IsFriend())
                        playes[i].gameObject.SetActive(false);
                }
                LogToHud("Quest are Hidden");
            }, delegate
            {
                var playes = GetAllPlayers();
                for (int i = 0; i < playes._size; i++)
                {
                    if (playes[i].prop_APIUser_0.last_platform != "standalonewindows" && !playes[i].IsFriend())
                        playes[i].gameObject.SetActive(true);
                }
                LogToHud("Quest are Shown");
            }, "Hide Quest (Does not block but local hide them)");

            var forceJump = new QMToggleButton(GeneralMenu, 3, 0, "ForceJump", delegate
            {
                EnableDisableJumping(state: true);
                LogToHud("Force Jump Enabled");
            }, delegate
            {
                EnableDisableJumping(state: false);
                LogToHud("Force Jump Disabled");
            }, "Force Jump for those annoying worlds");


            var Mutedisc = new QMSingleButton(GeneralMenu, 2, 1, "Mute Discord", delegate
            {
                var p = Process.GetProcessesByName("Discord").FirstOrDefault();
                try
                {
                    Thread.Sleep(100);
                    UnmanagedUtils.SetForegroundWindow(p.MainWindowHandle);
                    Thread.Sleep(100);
                    UnmanagedUtils.keybd_event(0xA2, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0xA0, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0x4D, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0xA2, 0, 0x0002, 0);
                    UnmanagedUtils.keybd_event(0xA0, 0, 0x0002, 0);
                    UnmanagedUtils.keybd_event(0x4D, 0, 0x0002, 0);
                }
                catch (Exception ex) { ExceptionHandler("Settings", ex, "Mute"); }
                Thread.Sleep(100);
                UnmanagedUtils.SetForegroundWindow(_hwnd);

            }, "Mutes Discord");

            

            var Deafndisc = new QMSingleButton(GeneralMenu, 1, 1, "Deafen Discord", delegate
            {
                var p = Process.GetProcessesByName("Discord").FirstOrDefault();
                try
                {
                    Thread.Sleep(100);
                    UnmanagedUtils.SetForegroundWindow(p.MainWindowHandle);
                    Thread.Sleep(100);
                    UnmanagedUtils.keybd_event(0xA2, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0xA0, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0x44, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0xA2, 0, 2, 0);
                    UnmanagedUtils.keybd_event(0xA0, 0, 2, 0);
                    UnmanagedUtils.keybd_event(0x44, 0, 2, 0);
                }
                catch (Exception ex) { ExceptionHandler("Buttons", ex, "Defen"); }
                Thread.Sleep(100);
                UnmanagedUtils.SetForegroundWindow(_hwnd);

            }, "Deafen Discord");

            var ClearVRam = new QMSingleButton(GeneralMenu, 3, 1, "ClearVRAM", delegate
            {
                GeneralUtils.ClearVRAM();
            }, "ClearVRAM");
        }

        static void EnableDisableJumping(bool state)
        {
            GameObject gameObject = VRCPlayer.field_Internal_Static_VRCPlayer_0.gameObject;
            if (state && gameObject.GetComponent<PlayerModComponentJump>() == null)
            {
                gameObject.AddComponent<PlayerModComponentJump>();
                gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_0 = 3f;
                gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_1 = 3f;
            }
            else
            {
                gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_0 = 3f;
                gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_1 = 3f;
            }
            if (!state && gameObject.GetComponent<PlayerModComponentJump>() != null)
            {
                gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_0 = 0f;
                gameObject.GetComponent<PlayerModComponentJump>().field_Private_Single_1 = 0f;
                UnityEngine.Object.Destroy(gameObject.GetComponent<PlayerModComponentJump>());
            }
        }

        static Il2CppSystem.Collections.Generic.List<VRC.Player> GetAllPlayers() => PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0;
    }
}
