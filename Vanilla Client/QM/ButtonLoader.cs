using Il2CppSystem;
using MelonLoader;
using Photon.Realtime;
using System;
using System.Collections;
using System.Diagnostics;
using System.Net;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Exploits;
using Vanilla.Modules;
using Vanilla.QM.Menu;
using Vanilla.Wrappers;
using VRC.SDKBase;

namespace Vanilla.QM
{
    internal class ButtonLoader : VanillaModule
    {
        protected override string ModuleName => "QM Loader";
       
        internal override void LateStart()
        { MelonCoroutines.Start(WaitForQMLoad()); }

        internal static IEnumerator WaitForQMLoad()
        {
            while (GameObject.Find($"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup") == null) yield return null;
            LoadButtons();
        }
        internal static void LoadButtons()
        {

            var tabMenu = new QMTabMenu("Vanilla", "Vanilla Client", ImageUtils.CreateSpriteFromTexture(AssetLoader.LoadTexture("VanillaClientLogo")));
 
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/").gameObject.SetActive(false);

            var Discord = new QMSingleButton(tabMenu, 1, 0, "Join The Discord", delegate
            { Process.Start("https://hvl.gg/discord/"); }, "Join The Discord");

            var GoToRoom = new QMSingleButton(tabMenu, 2, 0, "Go to Room", delegate
            {
                string roomid = null;
                Xrefs.Input.PopOutInput("Room Instance Id", value => roomid = value, () => {
                    Networking.GoToRoom(roomid);
                });
            }, "Go To Room");
            
            var AvatarID = new QMSingleButton(tabMenu, 3, 0, "AvatarID", delegate
            {
                var NewAvi = GeneralUtils.GetClipboard();
                if (NewAvi.Contains("avtr"))
                PlayerWrapper.ChangePlayerAvatar(NewAvi);

            }, "Change Avatar By ID");

            Settings.SettingsMenu(tabMenu);
            ExploitMenu.InitMenu(tabMenu);

            //QMImage.LoadQMImage();
            // ButInfo.Info(tabMenu);
            //Exploitable.ButtonExploits(tabMenu);
            //ButtSettings.SettingsMenu(tabMenu);
            //Selected_User.UserInteractions();

            //bool to check if we should load the staff menu
            //if (Settings.Config.IsStaff)
            //{ //Staff.Panel(tabMenu); }

            //  styles.QMModifyer.INITQM();

            //just an alert that the buttons should of loaded properly
        }

    }
}

