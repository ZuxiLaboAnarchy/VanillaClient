// /*
//  *
//  * VanillaClient - LoadMusic.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using MelonLoader;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Vanilla.Modules.Manager;

namespace Vanilla.Modules
{
    internal class LoadMusic : VanillaModule
    {
        protected override string ModuleName => "LoadMusic";

        internal override void Start()
        {
            MelonCoroutines.Start(Starter());
        }


        internal static IEnumerator Starter()
        {
            //  if (Config.MainConfig.GetInstance().LoadMusic)
            if (true)
            {
                AudioClip audioclip = null;

                if (GetInstance().MusicPath == string.Empty)
                {
                    Dev("Audio Handler", "Loading Local Audio");
                    audioclip = AssetLoader.LoadAudio("LoadMusic");
                }

                else
                {
                    var clip = UnityWebRequest.Get(GetInstance().MusicPath);

                    clip.SendWebRequest();
                    while (!clip.isDone)
                    {
                        yield return null;
                    }

                    if (clip.isHttpError)
                    {
                        yield break;
                    }

                    audioclip = WebRequestWWW.InternalCreateAudioClipUsingDH(clip.downloadHandler, clip.url, false,
                        false, AudioType.UNKNOWN);

                    audioclip.hideFlags = HideFlags.DontUnloadUnusedAsset;
                }
                //load screen when the game first starts

                while (GameObject.Find("LoadingBackground_TealGradient_Music") == null)
                {
                    yield return null;
                }

                var source1 = GameObject.Find("LoadingBackground_TealGradient_Music").transform.Find("LoadingSound")
                    .GetComponent<AudioSource>();
                source1.clip = audioclip;
                source1.Play();
                Log("LoadMod", "Background Reloaded Successfully");


                //every load screen after
                while (GameObject.Find("MenuContent/Popups/LoadingPopup") == null)
                {
                    yield return null;
                }


                var source2 = GameObject.Find("MenuContent/Popups/LoadingPopup").transform.Find("LoadingSound")
                    .GetComponent<AudioSource>();
                source2.clip = audioclip;
                source2.Play();
                GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient")
                    .SetActive(false);
                yield return new WaitForSeconds(0.5f);
            }
            /*           else

                       {
                           GetInstance().LoadMusic = true;
                       }
           */
        }
    }
}
