using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vanilla.Config;

namespace Vanilla.Modules
{
    internal class LoadScreen : VanillaModule
    {
        protected override string ModuleName => "LoadingScreen";
        static GameObject partsystem;
        internal override void WaitForAPIUser()
        {
            UpdateImages();
            MelonCoroutines.Start(loadParticles());
             
        }
        private static void UpdateImages()
        {
            var Images = GameObject.Find("UserInterface/MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress").GetComponentsInChildren<UnityEngine.UI.Image>(true);
            foreach (var image in Images)
            {
                image.color = MainConfig.GetInstance().GlobalColors; 
            }
        }

        internal static IEnumerator loadParticles()
        {
            partsystem = AssetLoader.LoadGameObject("particleloader");
            var gmj = GameObject.Instantiate(partsystem, GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup").transform);
            gmj.transform.localPosition = new Vector3(0, 0, 8000);
            gmj.transform.Find("finished").gameObject.transform.localPosition = new Vector3(0, 0, 10000);
            gmj.transform.Find("finished/Other").gameObject.transform.localPosition = new Vector3(0, 0, 3000);
            gmj.transform.Find("middle").gameObject.transform.localPosition = new Vector3(-50, 0f, 10000);
            gmj.transform.Find("cirlce mid").gameObject.transform.localPosition = new Vector3(-673.8608f, 0, 4000);
            gmj.transform.Find("spawn").gameObject.transform.localPosition = new Vector3(800, 0, -8500f);

            foreach (var gmjs in gmj.GetComponentsInChildren<ParticleSystem>(true)) 
            {
                gmjs.startColor = MainConfig.GetInstance().GlobalColors;
                gmjs.trails.colorOverTrail = MainConfig.GetInstance().GlobalColors;
            }
            GameObject.Find("/UserInterface").transform.Find("MenuContent/Popups/LoadingPopup/3DElements").gameObject.SetActive(false);

            while (GameObject.Find("/UserInterface").transform.Find("DesktopUImanager") == null)
                yield return null;


            var toload = AssetLoader.LoadGameObject("holder");

         
            var gmjsa = GameObject.Instantiate(toload, GameObject.Find("/UserInterface").transform.Find("DesktopUImanager").transform);
            gmjsa.transform.localPosition = new Vector3(0, 360.621f, 700);
            gmjsa.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gmjsa.transform.localScale = new Vector3(1, 1, 1);
            var p1 = gmjsa.transform.Find("Particle System").transform;
            p1.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            p1.localPosition = new Vector3(0, 64.16f, 7.2f);
            var p2 = gmjsa.transform.Find("Particle System (1)").transform;
            p2.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            p2.localPosition = new Vector3(-30.78f, -321.5403f, 8.54f);
            yield return null;


        }
    }
}
