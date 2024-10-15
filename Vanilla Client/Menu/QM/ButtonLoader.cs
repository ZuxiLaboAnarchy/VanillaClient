using MelonLoader;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Misc.MonoBehaviors;
using Vanilla.Modules;
using Vanilla.QM.Menu;
using Vanilla.Wrappers;
using VRC.SDKBase;
using VRC.UI.Core.Styles;

namespace Vanilla.QM
{
    //[Zuxi.SDK.DoNotObfuscate]
    internal class ButtonLoader : VanillaModule
    {
        protected override string ModuleName => "QM Loader";

       
        // [Zuxi.SDK.DoNotObfuscate]
        internal override void  OnQuickMenuLoaded()
        {

            var tabMenu = new QMTabMenu("Vanilla", "Abandon Ware", ImageUtils.CreateSprite(AssetLoader.LoadTexture("VanillaClientLogo")));

            //  GameObject.Find("Canvas_QuickMenu(Clone)/UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/").gameObject.SetActive(false);

            var Discord = new QMSingleButton(tabMenu, 1, 0, "Join The Discord", delegate
            { Process.Start("https://imzuxi.com/galaxydiscord/"); }, "Join The Discord");
/*
            var GoToRoom = new QMSingleButton(tabMenu, 2, 0, "Go to Room", delegate
            {

                string roomid = null;
                Xrefs.Input.PopOutInput("Room Instance Id", value => roomid = value, () =>
                {
                    Networking.GoToRoom(roomid);
                });
            }, "Go To Room");
*/
            var AvatarID = new QMSingleButton(tabMenu, 3, 0, "AvatarID", delegate
            {
                InternalUIManager.RunKeyBoardPopup("Enter Avatar ID", "AvatarID", "Change Avatar", null, PlayerWrapper.ChangePlayerAvatar, null);


            }, "Change Avatar By ID");

            var JoinWorld = new QMSingleButton(tabMenu, 2, 0, "JoinWorld", delegate
            {
                InternalUIManager.RunKeyBoardPopup("Enter WorldID", "WorldID", "Go to World", null, WorldWrapper.GoToRoom, null);
            }, "Change Your Current World");


            var RestartAndRejoin = new QMSingleButton(tabMenu, 1, 1, "Restart And Rejoin", delegate
            {
                GeneralWrappers.CopyInstanceToClipboard();
                GeneralUtils.Restart();
            }, "Change Your Current World");

            // var SelectedPlayerMenu = new QMNestedButton("", 2, 2, "Mic Settings", "Vanilla", "AbandonWare");
            var selectedmenu = GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions");
            //selectedmenu.FindObject


            //  var selectedGroup = UnityEngine.Object.Instantiate(selectedmenu.gameObject,selectedmenu.gameObject.transform.parent);
            var Selected = new QMNestedButton("Menu_SelectedUser_Local", 0, 0, "Vanilla", "Target functions for AbandonWare", "Vanilla");

            Selected.GetMainButton().SetBackgroundImage(ImageUtils.CreateSprite(AssetLoader.LoadTexture("VanillaClientLogo")));
            Selected.GetMainButton().GetGameObject().transform.SetParent(selectedmenu.transform);

            UnityEngine.Object.Destroy(Selected.GetMainButton().GetGameObject().GetComponent<StyleElement>());

            Selected.GetMainButton().SetAction(delegate
            {
                RuntimeConfig.SelectedPlayer = PlayerWrapper.GetSelectedUser();

                Selected.OpenMe();
            });

            SelectedPlayer.InitMenu(Selected);

            //   var SelectedPlayerMenu = new QMNestedButton("Menu_SelectedUser_Local", 20, 4, "Vanilla", "AbandonWare Selected User Menu", "AbandonWare");
            //  Selected = new QMNestedButton("Menu_SelectedUser_Remote", "", 0, 0, "Target functions for Blaze's Client", "Blaze's Client");

            var GeneralMenuButton = new QMNestedButton(tabMenu, 4, 0, "General\nMenu", "Vanilla", "AbandonWare");
            GeneralMenu.InitMenu(GeneralMenuButton);

            var MovementMenu = new QMNestedButton(GeneralMenuButton, 4, 0, "Movement Settings", "Vanilla", "AbandonWare");
            Movement.InitMenu(MovementMenu);

            var ExploitMenuButton = new QMNestedButton(tabMenu, 2, 3, "Exploits", "Vanilla", "AbandonWare");
            ExploitMenu.InitMenu(ExploitMenuButton);

            var MicMenu = new QMNestedButton(ExploitMenuButton, 4, 3, "Mic Settings", "Vanilla", "AbandonWare");
            Micfuckery.InitMenu(MicMenu);

            var AmonUsMenu = new QMNestedButton(ExploitMenuButton, 4, 1, "Among Us", "Vanilla", "AbandonWare");
            AmongUsHacks.InitMenu(AmonUsMenu);

            var Muder4 = new QMNestedButton(ExploitMenuButton, 4, 2, "Murder 4", "Vanilla", "AbandonWare");
            MurderHacks.InitMenu(Muder4);

            var settingsmenu = new QMNestedButton(tabMenu, 1, 3, "Vanilla Settings", "Vanilla", "AbandonWare");
            Settings.InitMenu(settingsmenu);

            var SafeMenu = new QMNestedButton(tabMenu, 3, 3, "Safety Settings", "Vanilla", "AbandonWare");
            SafetyMenu.InitMenu(SafeMenu);
#if DEBUG
            var DevMenuButton = new QMNestedButton(tabMenu, 4, 3, "DevMenu", "Vanilla", "AbandonWare");
            Menu.DevMenu.InitMenu(DevMenuButton);

#endif
            // MelonCoroutines.Start(UpdateQuickMenuBackGround());

            Vanilla.Modules.MenuModification.UpdateQuickMenuColors(); 

            //QMImage.LoadQMImage(); 
            // ButInfo.Info(tabMenu);
            //Exploitable.ButtonExploits(tabMenu);
            //ButtSettings.SettingsMenu(tabMenu);
            //Selected_User.UserInteractions();

            //bool to check if we should load the staff menu
            //if (Settings.Config.IsStaff)
            //  { Staff.Panel(tabMenu); }

            //  styles.QMModifyer.INITQM();

            //just an alert that the buttons should of loaded properly
        }

    
    }
}


