// /*
//  *
//  * VanillaClient - MenuModifcation.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vanilla.Misc;
using Vanilla.Misc.MonoBehaviors;
using Vanilla.Modules.Manager;
using VRC.UI.Core.Styles;

namespace Vanilla.Modules
{
    internal class MenuModification : VanillaModule
    {
        protected override string ModuleName => "Menu Modifier";

        private static GameObject _qmMusicObject;

        internal override void OnQuickMenuLoaded()
        {
            UpdateQuickMenuColors();
            ApplyIfApplicable();
            UpdateMenuBackgroundImages();
            GetAvatarAndUpdate();

            var _AvatarPane = GameObject.Find("UserInterface/MenuContent/Screens/Avatar").gameObject
                .AddComponent<EnableDisableListener>();
            _AvatarPane.OnEnabled += UpdateBigMenuColorsToPastel;
            var _SocialPane = GameObject.Find("UserInterface/MenuContent/Screens/Social").gameObject
                .AddComponent<EnableDisableListener>();
            _SocialPane.OnEnabled += UpdateBigMenuColorsToPastel;
            var _SettingsPane = GameObject.Find("UserInterface/MenuContent/Screens/Settings").gameObject
                .AddComponent<EnableDisableListener>();
            _SettingsPane.OnEnabled += UpdateBigMenuColorsToPastel;
            var _WorldsPane = GameObject.Find("UserInterface/MenuContent/Screens/Worlds").gameObject
                .AddComponent<EnableDisableListener>();
            _WorldsPane.OnEnabled += UpdateBigMenuColorsToPastel;

            AddQuickMenuAudio();
            //   EnableDisableListener _QuickMenu = GameObject.Find("\"Canvas_QuickMenu(Clone)/Container/Window/QMParent").gameObject.AddComponent<EnableDisableListener>();
            //   _AvatarPane.OnEnabled += UpdateQuickMenuColors;


            // ToDo Move to its own thing
            // if (GeneralUtils.GetClipboard().Contains("wrld_"))
            //   Networking.GoToRoom(GeneralUtils.GetClipboard());
        }

        internal override void OnUiManagerInit()
        {
            //ApplyIfApplicable();
        }


        private static void Run()
        {
            // UpdateQuickMenuColors();
            // UpdateMenuBackgroundImages();
        }

        private static void UpdateMenuBackgroundImages()
        {
            //  while (GameObject.Find($"Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer02") == null) yield return null;
            var QMBackGround = GameObject.Find("UserInterface").transform
                .Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer02").gameObject
                .GetComponent<Image>();
            QMBackGround.sprite = ImageUtils.CreateSprite(AssetLoader.LoadTexture("QuickMenuBackground"));
            var BMBackGround = GameObject.Find("UserInterface/MenuContent/Backdrop/Backdrop/Background").gameObject
                .GetComponent<Image>();
            BMBackGround.sprite = ImageUtils.CreateSprite(AssetLoader.LoadTexture("BigMenuBackground"));
        }


        // Update Avatar Pane

        internal static void GetAvatarAndUpdate()
        {
            var _bigMenuMenuContent = GameObject.Find("/UserInterface").transform.Find("MenuContent").gameObject;
            foreach (var _button in _bigMenuMenuContent.GetComponentsInChildren<UiAvatarList>(true))
            {
                _button.field_Public_Color_0 = GetInstance().GlobalColors;
            }

            foreach (var _button in _bigMenuMenuContent.GetComponentsInChildren<UiFeaturedButton>(true))
            {
                var cb = _button.colors;
                cb.normalColor = GetInstance().GlobalColors;
                cb.highlightedColor = GetInstance().GlobalColors;
                cb.pressedColor = GetInstance().GlobalColors;
                ;
                cb.disabledColor = GetInstance().GlobalColors;
                ;
                cb.selectedColor = GetInstance().GlobalColors;
                ;
                _button.colors = cb;
            }
        }

        internal static void UpdateBigMenuColorsToPastel()
        {
            /// CYAN
            ///
            var _bigMenuMenuContent = GameObject.Find("/UserInterface").transform.Find("MenuContent").gameObject;
            foreach (var _button in _bigMenuMenuContent.GetComponentsInChildren<Button>(true))
            {
                var cb = _button.colors;
                cb.normalColor = GetInstance().GlobalColors;
                cb.highlightedColor = GetInstance().GlobalColors;
                cb.pressedColor = GetInstance().GlobalColors;
                ;
                cb.disabledColor = GetInstance().GlobalColors;
                ;
                cb.selectedColor = GetInstance().GlobalColors;
                ;
                _button.colors = cb;
            }

            foreach (var _menuText in _bigMenuMenuContent.GetComponentsInChildren<Text>(true))
            {
                _menuText.color = GetInstance().GlobalColors;
            }

            foreach (var _menuImage in _bigMenuMenuContent.GetComponentsInChildren<Image>(true))
            {
                if (_menuImage.gameObject.name.Contains("Fill") || _menuImage.gameObject.name.Contains("Checkmark"))
                {
                    _menuImage.color = GetInstance().GlobalColors;
                }
            }
        }

        public static void UpdateQuickMenuColors()
        {
            var template = GameObject
                .Find("UserInterface/MenuContent/Backdrop/Header/Tabs/ViewPort/Content/WorldsPageTab")
                .GetComponent<Image>();

            foreach (var _QuickMenuContainer in GameObject.Find("/UserInterface").transform
                         .Find("Canvas_QuickMenu(Clone)/Container").gameObject.GetComponentsInChildren<Image>(true))
            {
                try

                {
                    if (_QuickMenuContainer.name ==
                        "Background") // && _QuickMenuContainer.gameObject.transform.parent.name.Contains("button_"))
                    {
                        var darkerT = new Color(GetInstance().GlobalColors.r / 2.5f,
                            GetInstance().GlobalColors.g / 2.5f, GetInstance().GlobalColors.b / 2.5f, 0.9f);
                        UnityEngine.Object.Destroy(_QuickMenuContainer.gameObject.GetComponent<StyleElement>());
                        //    _QuickMenuContainer.sprite = ImageUtils.CreateSprite(AssetLoader.LoadTexture("QuickMenuBackground"));
                        _QuickMenuContainer.sprite.ReplaceTexture(template.sprite.UnpackTexture()
                            .Desaturate()); //.color = MainConfig.GetInstance().GlobalColors;
                        _QuickMenuContainer.color = darkerT;
                        _QuickMenuContainer.m_Color = darkerT;
                    }

                    if (_QuickMenuContainer.name == "Badge_MMJump")
                    {
                        _QuickMenuContainer.color = GetInstance().GlobalColors;
                    }

                    if (_QuickMenuContainer.name == "Icon")
                    {
                        UnityEngine.Object.Destroy(_QuickMenuContainer.gameObject.GetComponent<StyleElement>());
                        _QuickMenuContainer.color = GetInstance().GlobalColors;
                    }
                }
                catch (Exception e)
                {
                    ExceptionHandler("Style", e);
                }
            }


            foreach (var _QuickMenuContainer in GameObject.Find("/UserInterface").transform
                         .Find("Canvas_QuickMenu(Clone)/Container").gameObject
                         .GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                _QuickMenuContainer.color = GetInstance().GlobalColors;
            }

            var color = GetInstance().GlobalColors;
            var darker = new Color(color.r / 2.5f, color.g / 2.5f, color.b / 2.5f);
            var theme = new ColorBlock()
            {
                colorMultiplier = 1f,
                disabledColor = Color.grey,
                highlightedColor = darker,
                normalColor = color,
                pressedColor = Color.gray,
                fadeDuration = 0.1f
            };
            foreach (var _QuickMenuContainer in GameObject.Find("/UserInterface").transform
                         .Find("Canvas_QuickMenu(Clone)/Container").gameObject.GetComponentsInChildren<Button>(true))
            {
                _QuickMenuContainer.colors = theme;
            }
        }

        public static void AddQuickMenuAudio()
        {
             _qmMusicObject = new GameObject("QMusic");
            var _AudioSource = _qmMusicObject.AddComponent<AudioSource>();
            _AudioSource.loop = true;
            _AudioSource.clip = AssetLoader.LoadAudio("QMusic");
            _AudioSource.Play();

            _qmMusicObject.transform.SetParent(GameObject.Find("UserInterface").transform
                .Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent").transform);
            _qmMusicObject.SetActive(GetInstance().QuickMenuMusic);
        }

        internal static void ToggleMusic()
        {
            _qmMusicObject.SetActive(GetInstance().QuickMenuMusic);
        }


        private static List<Image> _normalColorImage;
        private static List<Image> _dimmerColorImage;
        private static List<Image> _darkerColorImage;
        private static List<Text> _normalColorText;
        private static bool _setupSkybox = false;
        private static GameObject _loadingBackground;
        //  private static GameObject _initialLoadingBackground;
        private static Dictionary<UnityEngine.Object, Color> _originalColours;
        private static Dictionary<UnityEngine.Object, Texture2D> _originalTextures;
        private static Dictionary<UnityEngine.Object, Sprite> _originalSprites;
        private static Dictionary<UnityEngine.Object, ColorBlock> _originalColourBlocks;
        private static bool _collectingColours = false;

        internal static void ApplyIfApplicable()
        {
            if (_originalColours == null || _originalTextures == null || _originalSprites == null)
            {
                _originalColours = new Dictionary<UnityEngine.Object, Color>();
                _originalTextures = new Dictionary<UnityEngine.Object, Texture2D>();
                _originalSprites = new Dictionary<UnityEngine.Object, Sprite>();
                _originalColourBlocks = new Dictionary<UnityEngine.Object, ColorBlock>();
                _collectingColours = true;
            }

            //  Color color = Config.MainConfig.GetInstance().UiRecolor ? Configuration.menuColor() : Configuration.defaultMenuColor();
            var color = GetInstance().GlobalColors;
            var colorT = new Color(color.r, color.g, color.b, 0.7f);
            var dimmer = new Color(color.r / 0.75f, color.g / 0.75f, color.b / 0.75f);
            var dimmerT = new Color(color.r / 0.75f, color.g / 0.75f, color.b / 0.75f, 0.9f);
            var darker = new Color(color.r / 2.5f, color.g / 2.5f, color.b / 2.5f);
            var darkerT = new Color(color.r / 2.5f, color.g / 2.5f, color.b / 2.5f, 0.9f);

            var quickMenu = VRC.UI.UIManagerImpl.prop_UIManagerImpl_0.field_Private_Transform_0.Find("MenuContent")
                .gameObject;

            if (_normalColorImage == null || _normalColorImage.Count == 0)
            {
                Dev("MenuRecolor", "Gathering elements to color normally...");
                _normalColorImage = new List<Image>();
                _normalColorImage.Add(quickMenu.transform.Find("Screens/Settings_Safety/_Description_SafetyLevel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform
                    .Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Custom/ON").GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform
                    .Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_None/ON").GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform
                    .Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Normal/ON").GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform
                    .Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Maxiumum/ON").GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/InputKeypadPopup/Rectangle/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/InputKeypadPopup/InputField")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/StandardPopupV2/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/StandardPopup/InnerDashRing")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/StandardPopup/RingGlow").GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/UpdateStatusPopup/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/InputPopup/InputField").GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/UpdateStatusPopup/Popup/InputFieldStatus")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/AdvancedSettingsPopup/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/AddToFavoriteListPopup/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/EditFavoriteListPopup/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/PerformanceSettingsPopup/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/AlertPopup/Lighter").GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/RoomInstancePopup/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/ReportWorldPopup/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/ReportUserPopup/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/SearchOptionsPopup/Popup/Panel (1)")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/SendInvitePopup/SendInviteMenu/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/RequestInvitePopup/RequestInviteMenu/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/ControllerBindingsPopup/Popup/Panel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/ChangeProfilePicPopup/Popup/PanelBackground")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/ChangeProfilePicPopup/Popup/TitlePanel")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Screens/UserInfo/User Panel/PanelHeaderBackground")
                    .GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/StandardPopup/ArrowLeft").GetComponent<Image>());
                _normalColorImage.Add(quickMenu.transform.Find("Popups/StandardPopup/ArrowRight").GetComponent<Image>());
                //normalColorImage.Add(quickMenu.transform.Find("Screens/UserInfo/User Panel/Panel (1)").GetComponent<Image>());
                foreach (var obj in quickMenu.GetComponentsInChildren<Transform>(true)
                             .Where(x => x.name.Contains("Panel_Header")))
                {
                    foreach (var img in obj.GetComponentsInChildren<Image>())
                    {
                        if (img.gameObject.name != "Checkmark")
                        {
                            _normalColorImage.Add(img);
                        }
                    }
                }

                foreach (var obj in quickMenu.GetComponentsInChildren<Transform>(true)
                             .Where(x => x.name.Contains("Handle")))
                {
                    foreach (var img in obj.GetComponentsInChildren<Image>())
                    {
                        if (img.gameObject.name != "Checkmark")
                        {
                            _normalColorImage.Add(img);
                        }
                    }
                }

                try
                {
                    _normalColorImage.Add(quickMenu.transform
                        .Find("Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop")
                        .GetComponent<Image>());
                    _normalColorImage.Add(quickMenu.transform
                        .Find("Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left")
                        .GetComponent<Image>());
                    _normalColorImage.Add(quickMenu.transform
                        .Find("Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right")
                        .GetComponent<Image>());
                    _normalColorImage.Add(quickMenu.transform
                        .Find(
                            "Popups/LoadingPopup/MirroredElements/ProgressPanel (1)/Parent_Loading_Progress/Panel_Backdrop")
                        .GetComponent<Image>());
                    _normalColorImage.Add(quickMenu.transform
                        .Find(
                            "Popups/LoadingPopup/MirroredElements/ProgressPanel (1)/Parent_Loading_Progress/Decoration_Left")
                        .GetComponent<Image>());
                    _normalColorImage.Add(quickMenu.transform
                        .Find(
                            "Popups/LoadingPopup/MirroredElements/ProgressPanel (1)/Parent_Loading_Progress/Decoration_Right")
                        .GetComponent<Image>());
                }
                catch (Exception ex)
                {
                    ex = new Exception();
                }
            }

            if (_dimmerColorImage == null || _dimmerColorImage.Count == 0)
            {
                Dev("MenuRecolor", "Gathering elements to color lighter...");
                _dimmerColorImage = new List<Image>();
                _dimmerColorImage.Add(quickMenu.transform
                    .Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Custom/ON/TopPanel_SafetyLevel")
                    .GetComponent<Image>());
                _dimmerColorImage.Add(quickMenu.transform
                    .Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_None/ON/TopPanel_SafetyLevel")
                    .GetComponent<Image>());
                _dimmerColorImage.Add(quickMenu.transform
                    .Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Normal/ON/TopPanel_SafetyLevel")
                    .GetComponent<Image>());
                _dimmerColorImage.Add(quickMenu.transform
                    .Find("Screens/Settings_Safety/_Buttons_SafetyLevel/Button_Maxiumum/ON/TopPanel_SafetyLevel")
                    .GetComponent<Image>());
                _dimmerColorImage.Add(quickMenu.transform.Find("Popups/ChangeProfilePicPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                foreach (var obj in quickMenu.GetComponentsInChildren<Transform>(true)
                             .Where(x => x.name.Contains("Fill")))
                {
                    foreach (var img in obj.GetComponentsInChildren<Image>())
                    {
                        if (img.gameObject.name != "Checkmark")
                        {
                            _dimmerColorImage.Add(img);
                        }
                    }
                }
            }

            if (_darkerColorImage == null || _darkerColorImage.Count == 0)
            {
                Dev("MenuRecolor", "Gathering elements to color darker...");
                _darkerColorImage = new List<Image>();
                _darkerColorImage.Add(
                    quickMenu.transform.Find("Popups/InputKeypadPopup/Rectangle").GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/StandardPopupV2/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/StandardPopup/Rectangle").GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/StandardPopup/MidRing").GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/UpdateStatusPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/AdvancedSettingsPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/AddToFavoriteListPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/EditFavoriteListPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/PerformanceSettingsPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/RoomInstancePopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/RoomInstancePopup/Popup/BorderImage (1)")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/ReportWorldPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/ReportUserPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/SearchOptionsPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/SendInvitePopup/SendInviteMenu/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/RequestInvitePopup/RequestInviteMenu/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Popups/ControllerBindingsPopup/Popup/BorderImage")
                    .GetComponent<Image>());
                _darkerColorImage.Add(quickMenu.transform.Find("Screens/UserInfo/ModerateDialog/Panel/BorderImage")
                    .GetComponent<Image>());
                foreach (var obj in quickMenu.GetComponentsInChildren<Transform>(true).Where(x =>
                             (x.name.Contains("Background") || x.name.Contains("TitlePanel")) &&
                             x.name != "PanelHeaderBackground" && !x.transform.parent.name.Contains("UserIcon") &&
                             x.transform.name != "Button_PerformanceOptions"))
                {
                    foreach (var img in obj.GetComponentsInChildren<Image>())
                    {
                        if (img.gameObject.name != "Checkmark")
                        {
                            _darkerColorImage.Add(img);
                        }
                    }
                }
            }

            if (_normalColorText == null || _normalColorText.Count == 0)
            {
                Dev("MenuRecolor", "Gathering text elements to color...");
                _normalColorText = new List<Text>();
                foreach (var txt in quickMenu.transform.Find("Popups/InputPopup/Keyboard/Keys")
                             .GetComponentsInChildren<Text>(true))
                {
                    _normalColorText.Add(txt);
                }

                foreach (var txt in quickMenu.transform.Find("Popups/InputKeypadPopup/Keyboard/Keys")
                             .GetComponentsInChildren<Text>(true))
                {
                    _normalColorText.Add(txt);
                }

                _normalColorText.Add(quickMenu.transform.Find("Screens/Settings/VolumePanel/VolumeGameWorld/Label")
                    .GetComponentInChildren<Text>(true));
                _normalColorText.Add(quickMenu.transform.Find("Screens/Settings/VolumePanel/VolumeGameVoice/Label")
                    .GetComponentInChildren<Text>(true));
                _normalColorText.Add(quickMenu.transform.Find("Screens/Settings/VolumePanel/VolumeGameAvatars/Label")
                    .GetComponentInChildren<Text>(true));
                _normalColorText.AddRange(quickMenu.transform.Find("Screens/Social/UserProfileAndStatusSection")
                    .GetComponentsInChildren<Text>(true));
                //normalColorText.Add(quickMenu.transform.Find("Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_Percent").GetComponentInChildren<Text>(true));
                _normalColorText.Add(quickMenu.transform
                    .Find("Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_LOADING_Size")
                    .GetComponentInChildren<Text>(true));
                //normalColorText.Add(quickMenu.transform.Find("Popups/LoadingPopup/MirroredElements/ProgressPanel (1)/Parent_Loading_Progress/Loading Elements/txt_Percent").GetComponentInChildren<Text>(true));
                _normalColorText.Add(quickMenu.transform
                    .Find(
                        "Popups/LoadingPopup/MirroredElements/ProgressPanel (1)/Parent_Loading_Progress/Loading Elements/txt_LOADING_Size")
                    .GetComponentInChildren<Text>(true));
            }

            if (_collectingColours)
            {
                foreach (var img in _normalColorImage)
                {
                    _originalColours.Add(img, img.color);
                    if (img.sprite != null && img.sprite.texture != null)
                    {
                        _originalSprites.Add(img, img.sprite);
                    }
                }

                foreach (var img in _dimmerColorImage)
                {
                    _originalColours.Add(img, img.color);
                    if (img.sprite != null && img.sprite.texture != null)
                    {
                        _originalSprites.Add(img, img.sprite);
                    }
                }

                foreach (var img in _darkerColorImage)
                {
                    _originalColours.Add(img, img.color);
                    if (img.sprite != null && img.sprite.texture != null)
                    {
                        _originalSprites.Add(img, img.sprite);
                    }
                }

                foreach (var txt in _normalColorText)
                {
                    _originalColours.Add(txt, txt.color);
                }
            }

            if (GetInstance().UiRecolor)
            {
                Dev("MenuRecolor", "Coloring normal elements...");
                foreach (var img in _normalColorImage)
                {
                    if (img.sprite != null && img.sprite.texture != null)
                    {
                        img.sprite = _originalSprites[img].ReplaceTexture(img.sprite.UnpackTexture().Desaturate());
                    }

                    img.color = colorT;
                }

                Dev("MenuRecolor", "Coloring lighter elements...");
                foreach (var img in _dimmerColorImage)
                {
                    if (img.sprite != null && img.sprite.texture != null)
                    {
                        img.sprite = _originalSprites[img].ReplaceTexture(img.sprite.UnpackTexture().Desaturate());
                    }

                    img.color = dimmerT;
                }

                Dev("MenuRecolor", "Coloring darker elements...");
                foreach (var img in _darkerColorImage)
                {
                    if (img.sprite != null && img.sprite.texture != null)
                    {
                        img.sprite = _originalSprites[img].ReplaceTexture(img.sprite.UnpackTexture().Desaturate());
                    }

                    img.color = darkerT;
                }

                Dev("MenuRecolor", "Coloring text elements...");
                foreach (var txt in _normalColorText)
                {
                    txt.color = color;
                }
            }
            else if (!GetInstance().UiRecolor && _originalColours != null)
            {
                foreach (var img in _normalColorImage)
                {
                    img.color = _originalColours
                        .FirstOrDefault(a => a.Key.GetType() == typeof(Image) && (Image)a.Key == img).Value;
                }

                foreach (var img in _dimmerColorImage)
                {
                    img.color = _originalColours
                        .FirstOrDefault(a => a.Key.GetType() == typeof(Image) && (Image)a.Key == img).Value;
                }

                foreach (var img in _darkerColorImage)
                {
                    img.color = _originalColours
                        .FirstOrDefault(a => a.Key.GetType() == typeof(Image) && (Image)a.Key == img).Value;
                }

                foreach (var txt in _normalColorText)
                {
                    txt.color = _originalColours
                        .FirstOrDefault(a => a.Key.GetType() == typeof(Text) && (Text)a.Key == txt).Value;
                }
            }

            if (!GetInstance().UiRecolor && !_collectingColours)
            {
                foreach (var kvp in _originalSprites)
                {
                    if (kvp.Key != null && kvp.Key.GetType() == typeof(Image))
                    {
                        ((Image)kvp.Key).sprite = kvp.Value;
                    }
                }
            }

            if (!_setupSkybox && !ModCompatibility.BetterLoadingScreen)
            {
                try
                {
                    Dev("MenuRecolor", "Setting up skybox coloring...");
                    //Resources.blankGradient = new Texture2D(16, 16);
                    //UnityEngine.ImageConversion.LoadImage(Resources.blankGradient, Convert.FromBase64String(B64Textures.Gradient), false);
                    _loadingBackground = quickMenu.transform
                        .Find("Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked").gameObject;
                    _loadingBackground.GetComponent<MeshRenderer>().material
                        .SetTexture("_Tex", AssetLoader.LoadCubeMap("Gradient"));
                    _loadingBackground.GetComponent<MeshRenderer>().material.SetColor("_Tint",
                        new Color(color.r / 2f, color.g / 2f, color.b / 2f, color.a));
                    _loadingBackground.GetComponent<MeshRenderer>().material
                        .SetTexture("_Tex", AssetLoader.LoadCubeMap("Gradient"));
                    _setupSkybox = true;

                    //    initialLoadingBackground = UnityEngine.Object.Instantiate(loadingBackground, GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup").transform);

                    //   initialLoadingBackground.GetComponent<MeshRenderer>().material.SetTexture("_Tex", AssetLoader.LoadCubeMap("Gradient"));
                    //   initialLoadingBackground.GetComponent<MeshRenderer>().material.SetColor("_Tint", new Color(color.r / 2f, color.g / 2f, color.b / 2f, color.a));
                    //   initialLoadingBackground.GetComponent<MeshRenderer>().material.SetTexture("_Tex", AssetLoader.LoadCubeMap("Gradient"));
                }
                catch (Exception ex)
                {
                    Error("MenuRecolor", ex.ToString());
                }
            }

            if (_setupSkybox && _loadingBackground != null && !ModCompatibility.BetterLoadingScreen)
            {
                Dev("MenuRecolor", "Coloring skybox...");
                _loadingBackground.GetComponent<MeshRenderer>().material.SetColor("_Tint",
                    new Color(color.r / 2f, color.g / 2f, color.b / 2f, color.a));
            }

            var buttonTheme = new ColorBlock()
            {
                colorMultiplier = 1f,
                disabledColor = Color.grey,
                highlightedColor = color * 1.5f,
                normalColor = color / 1.5f,
                pressedColor = new Color(1f, 1f, 1f, 1f),
                fadeDuration = 0.1f,
                selectedColor = color / 1.5f
            };
            color.a = 0.9f;
            foreach (var highlights in Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>())
            {
                if (highlights != null && _collectingColours)
                {
                    _originalColours.Add(highlights, highlights.highlightColor);
                }

                if (GetInstance().UiRecolor)
                {
                    highlights.highlightColor = color;
                }
                else if (!GetInstance().UiRecolor && _originalColours != null)
                {
                    highlights.highlightColor = _originalColours
                        .FirstOrDefault(a => a.Key.GetType() == typeof(HighlightsFXStandalone)).Value;
                }
            }

            try
            {
                var HudVoiceIndicator = Resources.FindObjectsOfTypeAll<FadeCycleEffect>()
                    .First(a => a.gameObject.name == "VoiceDotDisabled").transform.parent.gameObject;
                if (GetInstance().UiRecolor && GetInstance().UiRecolor)
                {
                    Dev("MenuRecolor", "Coloring Push To Talk icons...");
                    foreach (var img in HudVoiceIndicator.transform.GetComponentsInChildren<Image>())
                    {
                        if (img.gameObject.name != "PushToTalkKeybd" && img.gameObject.name != "PushToTalkXbox")
                        {
                            img.color = color;
                        }
                    }
                }
                else
                {
                    Dev("MenuRecolor", "Decoloring Push To Talk icons...");
                    foreach (var img in HudVoiceIndicator.transform.GetComponentsInChildren<Image>())
                    {
                        if (img.gameObject.name != "PushToTalkKeybd" && img.gameObject.name != "PushToTalkXbox")
                        {
                            img.color = Color.red;
                        }
                    }
                }

                HudVoiceIndicator.transform.Find("VoiceDotDisabled").GetComponent<FadeCycleEffect>().enabled =
                    GetInstance().UiRecolor;
            }
            catch (Exception ex)
            {
                ex = new Exception();
            }

            Dev("MenuRecolor", "Coloring input popup...");
            try
            {
                var inputHolder = quickMenu.transform.Find("Popups/InputPopup");
                darker.a = 0.8f;
                inputHolder.Find("Rectangle").GetComponent<Image>().color = darker;
                darker.a = .5f;
                color.a = 0.8f;
                inputHolder.Find("Rectangle/Panel").GetComponent<Image>().color = color;
                color.a = .5f;
                var holder = quickMenu.transform.Find("Backdrop/Header/Tabs/ViewPort/Content/Search");
                holder.Find("SearchTitle").GetComponent<Text>().color = color;
                holder.Find("InputField").GetComponent<Image>().color = color;
            }
            catch (Exception ex)
            {
                Error("MenuRecolor", ex.ToString());
            }
            //SpriteRenderer cursorRenderer = UnityEngine.Resources.FindObjectsOfTypeAll<VRC.UI.CursorIcon>().FirstOrDefault().field_Public_SpriteRenderer_0;
            //if (collectingColours)
            //    originalColours.Add(cursorRenderer, cursorRenderer.color);
            //if (Config.MainConfig.GetInstance().UiRecolor )
            //    cursorRenderer.color = color;
            //else if (!Config.MainConfig.GetInstance().UiRecolor  && !collectingColours)
            //    cursorRenderer.color = originalColours[cursorRenderer];

            Dev("MenuRecolor", "Coloring QM buttons...");
            try
            {
                var theme = new ColorBlock()
                {
                    colorMultiplier = 1f,
                    disabledColor = Color.grey,
                    highlightedColor = darker,
                    normalColor = color,
                    pressedColor = Color.gray,
                    fadeDuration = 0.1f
                };

                color.a = .5f;
                darker.a = 1f;
                theme.normalColor = darker;
                foreach (var sldr in quickMenu.GetComponentsInChildren<Slider>(true))
                {
                    if (_collectingColours)
                    {
                        _originalColourBlocks.Add(sldr, sldr.colors);
                    }

                    if (GetInstance().UiRecolor)
                    {
                        sldr.colors = theme;
                    }
                    else if (!GetInstance().UiRecolor && !_collectingColours)
                    {
                        sldr.colors = _originalColourBlocks.FirstOrDefault(a => a.Key != null && a.Key == sldr).Value;
                    }
                }

                darker.a = .5f;
                theme.normalColor = color;
                foreach (var btn in quickMenu.GetComponentsInChildren<Button>(true))
                {
                    if (btn.gameObject.GetComponentsInChildren<Transform>(true)
                            .Any(a => a.name == "emmVRCDoNotColor") || (btn.name != "SubscribeToAddPhotosButton" &&
                                                                        btn.name != "SupporterButton" &&
                                                                        btn.name != "ModerateButton" &&
                                                                        btn.transform.parent.name != "VRC+PageTab" &&
                                                                        (btn.name != "ReportButton" ||
                                                                         btn.transform.parent.name
                                                                             .Contains("WorldInfo"))))
                    {
                        if (_collectingColours)
                        {
                            _originalColourBlocks.Add(btn, btn.colors);
                        }

                        if (GetInstance().UiRecolor)
                        {
                            btn.colors = buttonTheme;
                        }
                        else if (!GetInstance().UiRecolor && !_collectingColours)
                        {
                            btn.colors = _originalColourBlocks.FirstOrDefault(a => a.Key != null && a.Key == btn).Value;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception();
            }

            if (GetInstance().UiRecolor)
            {
                try
                {
                    Dev("MenuRecolor", "Coloring Action Menu...");
                    // Color referenceColor = (Config.MainConfig.GetInstance().UiRecolor  ? Configuration.menuColor() : new Color(Configuration.defaultMenuColor().r * 1.5f, Configuration.defaultMenuColor().g * 1.5f, Configuration.defaultMenuColor().b * 1.5f));
                    var referenceColor = GetInstance().GlobalColors;
                    var transparent = new Color(referenceColor.r, referenceColor.g, referenceColor.b,
                        referenceColor.a / 1.25f);
                    foreach (var grph in Resources.FindObjectsOfTypeAll<PedalGraphic>())
                    {
                        if (grph.gameObject.name != "Center")
                        {
                            //grph.material.SetColor("_Color", Color.white);
                            if (grph._texture != null)
                            {
                                grph._texture = grph._texture.ToTexture2D().Desaturate().ToTexture();
                            }

                            grph.color = referenceColor;
                        }
                        //grph.CrossFadeColor(Color.white, 0f, false, false);
                    }

                    foreach (var menu in Resources.FindObjectsOfTypeAll<ActionMenu>())
                    {
                        var baseImage = menu.transform.Find("Main/Cursor").GetComponentInChildren<Image>();
                        if (baseImage == null)
                        {
                            return;
                        }

                        baseImage.sprite =
                            baseImage.sprite.ReplaceTexture(baseImage.sprite.UnpackTexture().Desaturate());
                        baseImage.color = transparent;
                        //menu.cursor.GetComponentInChildren<Image>().color = transparent;
                    }
                }
                catch (Exception ex)
                {
                    Error("MenuRecolor", ex.ToString());
                }
            }


            _collectingColours = false;
            //try
            //{
            //    foreach (Image img in ButtonAPI.GetQuickMenuInstance().GetComponentsInChildren<Image>(true))
            //    {
            //        if (img == null || img.sprite == null || img.sprite.texture == null) return;
            //        img.sprite = img.sprite.ReplaceTexture(img.sprite.UnpackTexture().Desaturate());
            //        img.color = color;
            //    }
            //} catch (Exception ex)
            //{
            //    emmVRCLoader.Logger.LogError(ex.ToString());
            //}
        }
    }
}
