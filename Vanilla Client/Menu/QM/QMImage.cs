using MelonLoader;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Vanilla.Config;
using Object = UnityEngine.Object;

namespace Vanilla.QM
{
    internal class QMImage
    {
        internal static void LoadQMImage()
        {
            var backroundimage = GameObject.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01")
                .GetComponent<Image>();
            Object.Destroy(backroundimage.gameObject.GetComponent<VRC.UI.Core.Styles.StyleElement>());
            // MelonCoroutines.Start(image.loadspriterest(backroundimage, URI.assets + "Mask.png"));
            backroundimage.sprite = ImageUtils.CreateSpriteFromTexture(AssetLoader.LoadTexture("VanillaClientLogo"));
            var backroundtwo = Object.Instantiate(backroundimage, backroundimage.transform);
            backroundtwo.name = "QMBackground";
            backroundtwo.gameObject.transform.localPosition = new Vector3(0, 26f, 0);
            backroundtwo.gameObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            backroundimage.gameObject.AddComponent<Mask>();

            var Qm = GameObject.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/BackgroundLayer01/QMBackground")
                .gameObject.GetComponent<Image>();

            Qm.sprite = ImageUtils.CreateSpriteFromTexture(AssetLoader.LoadTexture("VanillaClientLogo"));

            MelonCoroutines.Start(QMMusic());
        }

        internal static IEnumerator QMMusic()
        {
            var qm = GameObject.Find("Canvas_QuickMenu(Clone)").gameObject;
            var aud = qm.AddComponent<AudioSource>();
            aud.playOnAwake = true;
            aud.loop = true;
            aud.clip = null;
            var www = UnityWebRequest.Get(GetInstance().MusicPath);
            www.SendWebRequest();

            while (!www.isDone)
            {
                yield return null;
            }

            if (www.isHttpError)
            {
                yield break;
            }

            var t = WebRequestWWW.InternalCreateAudioClipUsingDH(www.downloadHandler, www.url, false, false,
                AudioType.MPEG);
            t.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            aud.clip = t;

            if (GetInstance().LoadMusic == false)
            {
                GameObject.Find("Canvas_QuickMenu(Clone)").GetComponent<AudioSource>().enabled = false;
            }
        }
    }
}