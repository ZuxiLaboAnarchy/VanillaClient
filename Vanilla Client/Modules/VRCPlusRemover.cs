using MelonLoader;
using System.Collections;
using UnityEngine;
using Vanilla.Modules;
using VRC.UI.Elements.Controls;
using VRTK;

namespace Vanilla.Modules
{
    internal class VRCPlus : VanillaModule
    {
        protected override string ModuleName => "AntiVRCPlus";

        static object VRCRemoveE = null;
        internal override void LateStart()
        {
            VRCRemoveE = MelonCoroutines.Start(WaitForMMLoad());
        }

        internal static IEnumerator WaitForMMLoad()
        {
            while (GameObject.Find("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_VRCPlus") == null) yield return null;
            ToggleVRCPlus(false);
            
        }
      
        internal static void ToggleVRCPlus(bool state)
        {

            GameObject AVIUPSELL = GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Viewport/VerticalLayoutGroup/VRC+ Upsell/");
            UnityEngine.Object.Destroy(AVIUPSELL);

            GameObject.Find("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_VRCPlus").gameObject.SetActive(state);
            GameObject ImgLines = GameObject.Find("Container/MMParent/Menu_Dashboard/ScrollRect_MM/Viewport/Content/Panel/Carousel_Banners/Image_MASK/Image/").gameObject; //SetActive(state);
            UnityEngine.Object.Destroy(ImgLines);
            GameObject Banner = GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_Dashboard/ScrollRect_MM/Viewport/Content/Panel/Carousel_Banners/Image_MASK/Image/Banners");
            GameObject Controls_Right = GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_Dashboard/ScrollRect_MM/Viewport/Content/Panel/Carousel_Banners/Image_MASK/Image/Controls_Right");
            GameObject Controls_Left = GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_Dashboard/ScrollRect_MM/Viewport/Content/Panel/Carousel_Banners/Image_MASK/Image/Controls_Left");
            UnityEngine.Object.Destroy(Controls_Right);
            UnityEngine.Object.Destroy(Controls_Left);
           
/*
            GameObject MainBanner = GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_Dashboard/ScrollRect_MM/Viewport/Content/Panel/Carousel_Banners/Image_MASK");
            GameObject MainBanerClone = UnityEngine.Object.Instantiate(MainBanner, MainBanner.transform.parent);
            Component BannerChangerClone= MainBanerClone.GetComponent<MonoBehaviourPublicLi1ObAc1ObOb1ObUnique>();
            UnityEngine.Object.Destroy(BannerChangerClone);
            Transform transform = MainBanerClone.transform.Find("Horizontal");


            "Canvas_MainMenu(Clone)/Container/MMParent/Menu_Dashboard/ScrollRect_MM/Viewport/Content/Panel/Carousel_Banners/Image_MASK/Image/Banners/IPS_Template_Banner(Clone)"

*/






            try
            {
                MelonCoroutines.Stop(VRCRemoveE);
                Dev("VRC+", "Stopped Coroutines");
            }
            catch (Exception e) { ExceptionHandler("VRC+", e); }
        }

    }
}
