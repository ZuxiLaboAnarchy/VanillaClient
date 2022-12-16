using MelonLoader;
using Photon.Realtime;
using System.Collections;
using System.Diagnostics;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Exploits;
using Vanilla.Modules;
using Vanilla.QM.Menu;
using VRC.SDKBase;

namespace Vanilla.QM
{
    internal class ButtonLoader : VanillaModule
    {
        internal override void Start()
        {
            MelonCoroutines.Start(WaitForQMLoad());
        }



        internal static IEnumerator WaitForQMLoad()
        {

            while (GameObject.Find($"Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup") == null) yield return null;
            //while("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/") == null
            //   while (GameObject.Find($"UserInterface/Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup") == null) yield return null;
            // GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners").SetActive(false); ;
            LoadButtons();
        }
        internal static void LoadButtons()
        {
#if DEBUG
            Dev("QM", "QM Found Loading buttons");

#endif

            var VanillaIcon = ImageUtils.CreateSpriteFromTexture(AssetLoader.LoadTexture("VanillaClientLogo")); //                  InitIcon("https://files.hvls.cloud/Cypher/NOvIRasu72.jpg");

            //     GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/Image_MASK/Image/Banners/").gameObject.SetActive(false);
            //   GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer02").gameObject.SetActive(false);
            var tabMenu = new QMTabMenu("Vanilla", "Vanilla Client", VanillaIcon);
           // var menu = new QMNestedButton(tabMenu, 1, 3, "Vanilla Client", "Running Stock", "Vanilla");

            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners/Image_MASK/").gameObject.SetActive(false);

            var Discord = new QMSingleButton(tabMenu, 1, 0, "Join The Discord", delegate
            { Process.Start("https://hvl.gg/discord/"); }, "Join The Discord");

            var GoToRoom = new QMSingleButton(tabMenu, 1, 3, "Go to Room", delegate
            {
                string roomid = null;
                Xrefs.Input.PopOutInput("Room Instance Id", value => roomid = value, () => {
                    Networking.GoToRoom(roomid);
                });
            }, "Go To Room");

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
#if DUBUG
            LogHandler.Log("Buttons", "Buttons Loaded");
#endif
        }

        internal static void StartEarrapeExploit()
        {
         
        }
    }
}

