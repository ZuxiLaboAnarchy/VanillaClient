using MelonLoader;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Modules;
using Vanilla.QM.Menu;
using Vanilla.Wrappers;
using VRC.SDKBase;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Menus;

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

            var tabMenu = new QMTabMenu("Vanilla", "Vanilla Client", ImageUtils.CreateSprite(AssetLoader.LoadTexture("VanillaClientLogo")));

            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/").gameObject.SetActive(false);

            var Discord = new QMSingleButton(tabMenu, 1, 0, "Join The Discord", delegate
            { Process.Start("https://hvl.gg/discord/"); }, "Join The Discord");

            var GoToRoom = new QMSingleButton(tabMenu, 2, 0, "Go to Room", delegate
            {
                string roomid = null;
                Xrefs.Input.PopOutInput("Room Instance Id", value => roomid = value, () =>
                {
                    Networking.GoToRoom(roomid);
                });
            }, "Go To Room");

            var AvatarID = new QMSingleButton(tabMenu, 3, 0, "AvatarID", delegate
            {
                InternalUIManager.RunKeyBoardPopup("Enter Avatar ID", "AvatarID", "Change Avatar",null, PlayerWrapper.ChangePlayerAvatar, null);
               

            }, "Change Avatar By ID");

            var JoinWorld = new QMSingleButton(tabMenu, 2, 0, "JoinWorld", delegate
            {
                InternalUIManager.RunKeyBoardPopup("Enter WorldID", "WorldID", "Go to World", null, WorldWrapper.GoToRoom, null);
            }, "Change Your Current World");



            // var SelectedPlayerMenu = new QMNestedButton("", 2, 2, "Mic Settings", "Vanilla", "Vanilla Client");
            var selectedmenu = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions");
            //selectedmenu.FindObject


           //  var selectedGroup = UnityEngine.Object.Instantiate(selectedmenu.gameObject,selectedmenu.gameObject.transform.parent);
           var Selected = new QMNestedButton("Menu_SelectedUser_Local", 0, 0, "Vanilla", "Target functions for Vanilla Client", "Vanilla");



            Selected.GetMainButton().SetBackgroundImage(ImageUtils.CreateSprite(AssetLoader.LoadTexture("VanillaClientLogo")));
            Selected.GetMainButton().GetGameObject().transform.SetParent(selectedmenu.transform);




            UnityEngine.Object.Destroy(Selected.GetMainButton().GetGameObject().GetComponent<StyleElement>());

            Selected.GetMainButton().SetAction(delegate
            {
                RuntimeConfig.SelectedPlayer = PlayerWrapper.GetSelectedUser();
 
                Selected.OpenMe();
            });





            SelectedPlayer.InitMenu(Selected); 

            


         //   var SelectedPlayerMenu = new QMNestedButton("Menu_SelectedUser_Local", 20, 4, "Vanilla", "Vanilla Client Selected User Menu", "Vanilla Client");
          //  Selected = new QMNestedButton("Menu_SelectedUser_Remote", "", 0, 0, "Target functions for Blaze's Client", "Blaze's Client");

            var GeneralMenuButton = new QMNestedButton(tabMenu, 4, 0, "General\nMenu", "Vanilla", "Vanilla client");
            GeneralMenu.InitMenu(GeneralMenuButton);
         
            var MovementMenu = new QMNestedButton(GeneralMenuButton, 4, 0, "Movement Settings", "Vanilla", "Vanilla Client");
            Movement.InitMenu(MovementMenu);

            var ExploitMenuButton = new QMNestedButton(tabMenu, 2, 3, "Exploits", "Vanilla", "Vanilla Client");
            ExploitMenu.InitMenu(ExploitMenuButton);

            var MicMenu = new QMNestedButton(ExploitMenuButton, 4, 3, "Mic Settings", "Vanilla", "Vanilla Client");
            Micfuckery.InitMenu(MicMenu);

            var AmonUsMenu = new QMNestedButton(ExploitMenuButton, 4, 1, "Among Us", "Vanilla", "Vanilla client");
            AmongUsHacks.InitMenu(AmonUsMenu);

            var Muder4 = new QMNestedButton(ExploitMenuButton, 4, 2, "Murder 4", "Vanilla", "Vanilla client");
            MurderHacks.InitMenu(Muder4);

            var settingsmenu = new QMNestedButton(tabMenu, 1, 3, "Vanilla Settings", "Vanilla", "Vanilla Client");
            Settings.InitMenu(settingsmenu);

            var SafeMenu = new QMNestedButton(tabMenu, 3, 3, "Safety Settings", "Vanilla", "Vanilla Client");
            SafetyMenu.InitMenu(SafeMenu);
#if DEBUG
            var DevMenuButton = new QMNestedButton(tabMenu, 4, 3, "DevMenu", "Vanilla", "Vanilla Client");
            DevMenu.InitMenu(DevMenuButton);

#endif








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

