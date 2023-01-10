using System.Collections.Concurrent;
using UnityEngine;
using Vanilla.Config;

namespace Vanilla.Patches.Harmony
{
    internal class ImageDownloaderPatch : VanillaPatches
    {

        internal static readonly ConcurrentQueue<ImageDownloadContainer> imageDownloadQueue = new ConcurrentQueue<ImageDownloadContainer>();

        internal static bool imageDownloadCallOriginal = false;

        protected override string patchName => "ImageDownloaderPatch";

        internal override void Patch()
        {
            InitializeLocalPatchHandler(typeof(ImageDownloaderPatch));
            PatchMethod(typeof(ImageDownloader).GetMethod(Strings.DownloadImageInternal), GetLocalPatch(Strings.OnImageDownloadPatch), null);
        }

        private static bool OnImageDownloadPatch(string __0, int __1, Il2CppSystem.Action<Texture2D> __2, Il2CppSystem.Action __3, string __4, bool __5)
        {
            if (MainConfig.ImageCache && !imageDownloadCallOriginal)
            {
                Texture2D cachedImage = CacheUtils.GetCachedImage(__0);
                if (cachedImage != null)
                {
                    __2.Invoke(cachedImage);
                    return false;
                }
                imageDownloadQueue.Enqueue(new ImageDownloadContainer
                {
                    imageUrl = __0,
                    imageSize = __1,
                    onImageDownload = __2,
                    onImageDownloadFailed = __3,
                    fallbackImageUrl = __4,
                    isRetry = __5
                });
                return false;
            }

            return true;
        }

        internal static void DownloadImage(string imageUrl, int imageSize, Il2CppSystem.Action<Texture2D> onImageDownload, Il2CppSystem.Action onImageDownloadFailed, string fallbackImageUrl = "", bool isRetry = false)
        {
            imageDownloadCallOriginal = true;
            ImageDownloader.Instance.DownloadImageInternal(imageUrl, imageSize, onImageDownload, onImageDownloadFailed, fallbackImageUrl, isRetry);
            imageDownloadCallOriginal = false;
        }
    }
}
