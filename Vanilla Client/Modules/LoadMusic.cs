using MelonLoader;
using System.Collections;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Networking;

namespace VanillaClient.Modules
{
    internal class LoadMusic : VanillaModule
    {
        public override void Start()
        {
            Dev("LoadMusic", "Loading Music");
            MelonCoroutines.Start(Starter());
        }



        public static IEnumerator Starter()
        {
           
            if (true)
            {
                var clip = UnityWebRequest.Get(@"C:\Users\Shadow\Downloads\SnapSave.io - Yuno Miles - First Day Of Christmas (Official Video) (Prod.YunoMarr) (128 kbps).mp3");

                clip.SendWebRequest();
                while (!clip.isDone) yield return null;
                if (clip.isHttpError) yield break;

                var audioclip = WebRequestWWW.InternalCreateAudioClipUsingDH(clip.downloadHandler, clip.url, false, false, AudioType.UNKNOWN);

                audioclip.hideFlags = HideFlags.DontUnloadUnusedAsset;

                //load screen when the game first starts

                while (GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music") == null) yield return null;

                var source1 = GameObject.Find("UserInterface/LoadingBackground_TealGradient_Music").transform.Find("LoadingSound").GetComponent<AudioSource>();
                source1.clip = audioclip;
                source1.Play();
                Log("LoadMusic", "Loading Music 1 Set Succes");


                //every load screen after
                while (GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup") == null) yield return null;


                var source2 = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup").transform.Find("LoadingSound").GetComponent<AudioSource>();
                source2.clip = audioclip;
                source2.Play();
                GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient").SetActive(false);
                yield return new WaitForSeconds(0.5f);
                Log("LoadMusic", "Loading Music 2 Set Succes");

            }
        }



     /*   public static void LoadSkyWhenever()
        {

            SkyBoxAssetBundle = AssetBundle.LoadFromFile($"{MelonUtils.GameDirectory}\\Galaxy\\Dependencies\\clientassetbundle");
            skyBoxMaterial = SkyBoxAssetBundle.LoadAsset_Internal("Load.mat", Il2CppType.Of<Material>()).Cast<Material>();
            UnityEngine.Object.Instantiate<Material>(skyBoxMaterial);
            bool flag = skyBoxMaterial == null;
            if (flag)
            {
                API.LogHandler.Error("SkyBox Mat was null ping Hyper", "Skyboxes");
            }
            RenderSettings.skybox = skyBoxMaterial;
            SkyBoxAssetBundle.Unload(false);
            firstload = false;
        }*/

        private static AssetBundle SkyBoxAssetBundle { get; set; }
        private static Material skyBoxMaterial;
        public static bool firstload = true;
    }
}
