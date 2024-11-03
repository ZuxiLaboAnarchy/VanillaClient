namespace Vanilla.Utils
{
    internal struct ImageDownloadContainer
    {
        internal string imageUrl;

        internal int imageSize;

        internal Il2CppSystem.Action<UnityEngine.Texture2D> onImageDownload;

        internal Il2CppSystem.Action onImageDownloadFailed;

        internal string fallbackImageUrl;

        internal bool isRetry;
    }
}