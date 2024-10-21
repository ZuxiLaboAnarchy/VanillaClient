﻿using MelonLoader;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Helpers;
using Vanilla.ServerAPI;
using VRC.SDKBase;

namespace Vanilla.QM.Menu
{
    internal class Settings
    {
        internal static IntPtr _hwnd = IntPtr.Zero;
        // internal static extern System.IntPtr SetActiveWindow(System.IntPtr hWnd);
        internal static VRC_Pickup[] array;

#pragma warning disable CS0626 // Method, operator, or accessor is marked external and has no attributes on it
        internal static extern System.IntPtr SetActiveWindow(System.IntPtr hWnd);
#pragma warning restore CS0626 // Method, operator, or accessor is marked external and has no attributes on it
        internal static VRC_Pickup[] GetAllPickups { get; set; }
        internal static void InitMenu(QMNestedButton settingsmenu)
        {

            

            //  var miscsettings = new QMNestedButton(settingsmenu, 3, 3, "Misc Settings", "Miscellaneous Settings", "Galaxy Client");

            var JoinLogger = new QMToggleButton(settingsmenu, 1, 0, "Join Logger", delegate

            {
                MainConfig.GetInstance().ESP = true;
                MainConfig.GetInstance().Save();
            }, delegate
            {
                MainConfig.GetInstance().ESP = false;
                MainConfig.GetInstance().Save();
            }, "Toggle ESP Saving", true);

            var LoadMusicToggle = new QMToggleButton(settingsmenu, 2, 0, "LoadMusic", delegate

            {
                MainConfig.GetInstance().LoadMusic = true;
                MelonCoroutines.Start(Modules.LoadMusic.Starter());
                MainConfig.GetInstance().Save();
            }, delegate
            {
                MainConfig.GetInstance().LoadMusic = false;
                MainConfig.GetInstance().Save();
            }, "Toggle Load Music", true);





            var ESP = new QMToggleButton(settingsmenu, 3, 0, "ESP", delegate

            {
                MainConfig.GetInstance().ESP = true;
                MainConfig.GetInstance().Save();
            }, delegate
            {
                MainConfig.GetInstance().ESP = false;
                MainConfig.GetInstance().Save();
            }, "Toggle ESP Saving", true);

            var FriendsToggle = new QMToggleButton(settingsmenu, 4, 0, "Auto-Save Friends", delegate

            {
                MainConfig.GetInstance().AutoFrends = true;
                MainConfig.GetInstance().Save();
            }, delegate
            {
                MainConfig.GetInstance().AutoFrends = false;
                MainConfig.GetInstance().Save();
            }, "Toggle AutoFriendsList Saving", MainConfig.GetInstance().AutoFrends);

            var RestartGame = new QMSingleButton(settingsmenu, 1, 1, "Restart Game", delegate
            {
                MainConfig.GetInstance().Save();
                GeneralUtils.Restart();
            }, "Restart Game");


            var QuickMenuMusic = new QMToggleButton(settingsmenu, 3, 3, "Quick Menu Music", delegate
            {
                MainConfig.GetInstance().QuickMenuMusic = true;
                MainConfig.GetInstance().Save();
            }, delegate
            {
                MainConfig.GetInstance().QuickMenuMusic = false;
                MainConfig.GetInstance().Save();
            }, "(Requires Restart)", true);

            var closeGame = new QMSingleButton(settingsmenu, 2, 1, "Close Game", delegate
            {
                MainConfig.GetInstance().Save();
                GeneralUtils.CloseGame();
            }, "Close Game");

            var ForceUpdate = new QMSingleButton(settingsmenu, 3, 1, "Force Updates", delegate
            {
                RuntimeConfig.isForced = true; 
                MainHelper.FetchUpdates();
                new Thread(() => { MainHelper.PopAvatarLog(); }).Start();
                new Thread(() => { WSBase.Pop(); }).Start();
            }, "Force Server Sync");

           

            var Pickups = new QMNestedButton(settingsmenu, 4, 1, "PickUps", "Vanilla", "AbandonWare");

            var respawnpicks = new QMSingleButton(Pickups, 1, 0, "Respawn Pickup", delegate
            {
                foreach (VRC_Pickup item in UnityEngine.Object.FindObjectsOfType<VRC_Pickup>())
                {
                    Networking.SetOwner(Networking.LocalPlayer, item.gameObject);
                    item.transform.position = new Vector3(0f, -9999f, 0f);
                }
            }, "Respawn Items");


            var OWnerShipItem = new QMSingleButton(Pickups, 2, 0, "Pickup OwnerShip", delegate
            {
                for (int i = 0; i < GetAllPickups.Length; i++)
                {
                    VRC_Pickup pickup = GetAllPickups[i];
                    TakeOwnerShipPickup(pickup);
                }
            }, "Pickup OwnerShip");


            static void TakeOwnerShipPickup(VRC_Pickup pickup)
            {
                if (!(pickup == null))
                {
                    Networking.SetOwner(Networking.LocalPlayer, pickup.gameObject);
                }
            }



            var Media = new QMNestedButton(settingsmenu, 1, 3, "Media Control", "Vanilla", "AbandonWare");

            var MediaControl = new QMSingleButton(Media, 1, 0, "Previous Track (Spotify)", delegate
            {
                var p = Process.GetProcessesByName("Spotify").FirstOrDefault();

                try
                {


                    Thread.Sleep(100);
                    UnmanagedUtils.SetForegroundWindow(p.MainWindowHandle);
                    Thread.Sleep(100);
                    UnmanagedUtils.keybd_event(0x11, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0x25, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0x11, 0, 2, 0);
                    UnmanagedUtils.keybd_event(0x25, 0, 2, 0);
                }
                catch { }
                Thread.Sleep(100);
                UnmanagedUtils.SetForegroundWindow(_hwnd);
            }, "Pervious Track");

            /*
            var custom = new QMToggleButton(settingsmenu,1,1,"Custom Plate", delegate
            {
                foreach (KeyValuePair<string, PlayerInformation> playerCaching in PlayerWrapper.playerCachingList)
                {
                    if (!playerCaching.Value.isLocalPlayer)
                    {
                        playerCaching.Value.customNameplateObject.SetActive(value: true);
                    }
                }
            },delegate
            {
                foreach (KeyValuePair<string, PlayerInformation> playerCaching2 in PlayerWrapper.playerCachingList)
                {
                    if (!playerCaching2.Value.isLocalPlayer)
                    {
                        playerCaching2.Value.customNameplateObject.SetActive(value: false);
                    }
                }
            },"Yes Fps and ping and such");
            */

            var playpause = new QMSingleButton(Media, 2, 0, "Play pause (Spotify)", delegate
            {
                var p = Process.GetProcessesByName("Spotify").FirstOrDefault();

                try
                {
                    Thread.Sleep(100);

                    UnmanagedUtils.SetForegroundWindow(p.MainWindowHandle);
                    Thread.Sleep(100);

                    UnmanagedUtils.keybd_event(0x20, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0x20, 0, 2, 0);
                }
                catch { }

                Thread.Sleep(100);
                UnmanagedUtils.SetForegroundWindow(_hwnd);
            }, "Play pause Track");

            var nexttrack = new QMSingleButton(Media, 3, 0, "Next Trak (Spotify)", delegate
            {
                var p = Process.GetProcessesByName("Spotify").FirstOrDefault();
                try
                {
                    Thread.Sleep(100);
                    UnmanagedUtils.SetForegroundWindow(p.MainWindowHandle);
                    Thread.Sleep(100);
                    UnmanagedUtils.keybd_event(0x11, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0x27, 0, 0, 0);
                    UnmanagedUtils.keybd_event(0x11, 0, 2, 0);
                    UnmanagedUtils.keybd_event(0x27, 0, 2, 0);
                }
                catch { }
                Thread.Sleep(100);
                UnmanagedUtils.SetForegroundWindow(_hwnd);
            }, "Play Next Track");

            var Chachedimg = new QMToggleButton(settingsmenu, 4, 3, "Cached Img", delegate

            {
                MainConfig.GetInstance().ImageCache = true;
                MainConfig.GetInstance().Save();
            }, delegate
            {
                MainConfig.GetInstance().ImageCache = false;
                MainConfig.GetInstance().Save();
            }, "Toggle Cached img Saving", MainConfig.GetInstance().ImageCache);




            /* World History button Somthing like this 
             
            Nocturnal.Ui.qm.Worldhistory.worldhistorymenu = submenu.Create("World History", Menu);
            new Submenubutton(Menu.GetMenu(), "World History", Nocturnal.Ui.qm.Worldhistory.worldhistorymenu, Download_Images._WorldIcon, true, 2, 6);
            
             */

            /*
            if (MainConfig.GetInstance().LoadMusic == true)
            { LoadMusicToggle.ClickMe(); }
            if (MainConfig.GetInstance().ESP == true)
            { ESP.ClickMe(); }
            if (MainConfig.GetInstance().JoinLogger == true)
            { JoinLogger.ClickMe(); }
            if (MainConfig.GetInstance().ImageCache == true)
            { Chachedimg.ClickMe(); }*/
        }
    }
}