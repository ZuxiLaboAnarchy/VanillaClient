using UnityEngine;

//using Galaxy.API;

namespace Vanilla.Utils
{
    internal class ImageUtils
    {
        internal static byte[] _PageIcon { get; set; }


        internal static byte[] tabImage;


        internal static Sprite CreateSprite(Texture2D texture)
        {
            var rect = new Rect(0f, 0f, texture.width, texture.height);
            var pivot = new Vector2(0.5f, 0.5f);
            var border = Vector4.zero;
            var sprite = Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, 100f, 0u, SpriteMeshType.Tight,
                ref border, false);
            sprite.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            return sprite;
        }

        internal static Sprite CreateSpriteFromTexture(Texture2D texture)
        {
            // Texture2D texture = CreateTextureFromBase64(data);

            var rect = new Rect(0.0f, 0.0f, texture.width, texture.height);

            var pivot = new Vector2(0.5f, 0.5f);
            var border = Vector4.zero;

            var sprite = Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, 100.0f, 0, SpriteMeshType.Tight,
                ref border, false);

            return sprite;
        }


        /*


        internal static Texture2D CreateTextureFromBase64(string data)
        {
            Texture2D texture = new Texture2D(2, 2);
            Il2CppImageConversionManager.LoadImage(texture, Convert.FromBase64String(data));

            texture.hideFlags |= HideFlags.DontUnloadUnusedAsset;

            return texture;
        }

        internal static IEnumerator loadspriterest(Image Instance, string url)
        {

            var www = UnityWebRequestTexture.GetTexture(url);
            _ = www.downloadHandler;
            var asyncOperation = www.SendWebRequest();
            Func<bool> func = () => asyncOperation.isDone;
            yield return new WaitUntil(func);
            if (www.isHttpError || www.isNetworkError)
            {
                LogHandler.Error("Image Loader", $"{www.error}");
                Console.WriteLine(url);
                yield break;
            }

            var content = DownloadHandlerTexture.GetContent(www);
            var sprite2 = Instance.sprite = Sprite.CreateSprite(content,
                new Rect(0f, 0f, content.width, content.height), new Vector2(0f, 0f), 100000f, 1000u,
                SpriteMeshType.FullRect, Vector4.zero, false);

            if (sprite2 != null) Instance.sprite = sprite2;
        }

        internal static Sprite InitIcon(string URL)
        {
            using (WebClient wc = new WebClient())
                tabImage = wc.DownloadData(URL);
            var tex2D = new Texture2D(256, 256, TextureFormat.ARGB32, false);
            if (!Il2CppImageConversionManager.LoadImage(tex2D, tabImage)) MelonLogger.Error("Couldn't load image");
            var sprite = Sprite.CreateSprite(tex2D, new Rect(0f, 0f, tex2D.width, tex2D.height), new Vector2(0.5f, 0.5f), 100f, 0u, SpriteMeshType.FullRect, default, false);
            sprite.hideFlags += 32;
            return sprite;
        }

        */
    }
}