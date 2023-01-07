using UnityEngine;
using Vanilla.Patches.Harmony;

namespace Vanilla.Modules
{
    internal class PerfModule : VanillaModule
    {
        internal override void LateStart()
        {
            Application.targetFrameRate = 1000;
            Dev("Perf", "Set Application Target Framerate Success");
        }

        internal override void AppFocus(bool state)
        {
            
            if (!state)
            {
                GeneralUtils.ClearVRAM();
                Application.targetFrameRate = 15;
                Dev("PerfModule", "Set Framerate to 15 Successfully");
                return;
            }
         
            Application.targetFrameRate = 1000;
            Dev("PerfModule", "Set Framerate to 1000 Successfully");
        }

        internal override void Update()
        {

            try
            {
                if (ImageDownloaderPatch.imageDownloadQueue.Count > 0 && ImageDownloaderPatch.imageDownloadQueue.TryDequeue(out var imageContainer))
                {

                    ImageDownloaderPatch.DownloadImage(imageContainer.imageUrl, imageContainer.imageSize, (Action<Texture2D>)delegate (Texture2D tex)
                    {
                        CacheUtils.AddCachedImage(imageContainer.imageUrl, tex);
                        imageContainer.onImageDownload.Invoke(tex);
                    }, (Action)delegate
                    {
                        imageContainer.onImageDownloadFailed.Invoke();
                    }, imageContainer.fallbackImageUrl, imageContainer.isRetry);
                }
            }

            catch (Exception e5)
            {
                ExceptionHandler("Perf", e5);
            }

        }
    }

}
