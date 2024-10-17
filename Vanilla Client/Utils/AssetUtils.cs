using System.Collections.Generic;
using UnhollowerRuntimeLib;
using UnityEngine;
using Vanilla.Modules;

namespace Vanilla.Utils
{
    internal class AssetLoader : VanillaModule
    {
        private static readonly Dictionary<string, object> assetCache = new Dictionary<string, object>();

        private static AssetBundle cachedAssetBundle = null;

        internal override void Start() 
        {
            cachedAssetBundle = AssetBundle.LoadFromMemory_Internal(Properties.Resources.ClientBundle, 0u); //File.ReadAllBytes(filePath), 0u);
            cachedAssetBundle.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            Dev("Assets", "Assets Loaded Successfully");
        }
        internal static void LoadAssetBundle()
        {
           
        }

        internal static Texture2D LoadTexture(string textureName)
        {
           // LogHandler.Log("Assets", "Requested " + textureName); 
            if (cachedAssetBundle == null)
            { 
               LogHandler.Log("Assets", "asset bundle was null", ConsoleColor.Red);
                return null;
            } 
            string text = "Assets/" + textureName + ".png";
            if (assetCache.ContainsKey(text))
            {
                return (Texture2D)assetCache[text];
            }
            Texture2D texture2D = null;
            texture2D = cachedAssetBundle.LoadAsset_Internal(text, Il2CppType.Of<Texture2D>()).Cast<Texture2D>();
            texture2D.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            assetCache.Add(text, texture2D); 
            return texture2D;
        }

        internal static Material LoadMaterial(string materialName)
        {
            if (cachedAssetBundle == null)
            {
                return null;
            }
            string text = "Assets/" + materialName + ".mat";
            if (assetCache.ContainsKey(text))
            {
                return (Material)assetCache[text];
            }
            Material material = cachedAssetBundle.LoadAsset_Internal(text, Il2CppType.Of<Material>()).Cast<Material>();
            material.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            assetCache.Add(text, material);
            return material;
        }

        internal static AudioClip LoadAudio(string audioName)
        {
            if (cachedAssetBundle == null)
            {
                return null;
            }
            string text = "Assets/" + audioName + ".ogg";
            if (assetCache.ContainsKey(text))
            {
                return (AudioClip)assetCache[text];
            }
            AudioClip audioClip = cachedAssetBundle.LoadAsset_Internal(text, Il2CppType.Of<AudioClip>()).Cast<AudioClip>();
            audioClip.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            assetCache.Add(text, audioClip);
            return audioClip;
        }

        internal static GameObject LoadGameObject(string prefabName)
        {
            if (cachedAssetBundle == null)
            {
                return null;
            }
            string text = "Assets/" + prefabName + ".prefab";
            if (assetCache.ContainsKey(text))
            {
                return (GameObject)assetCache[text];
            }
            GameObject gameObject = cachedAssetBundle.LoadAsset_Internal(text, Il2CppType.Of<GameObject>()).Cast<GameObject>();
            gameObject.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            assetCache.Add(text, gameObject);
            return gameObject;
        }
        internal static Cubemap LoadCubeMap(string cubemapName)
        {
            if (cachedAssetBundle == null)
            {
                return null;
            }
            string text = "Assets/" + cubemapName + ".png";
            if (assetCache.ContainsKey(text))
            {
                return (Cubemap)assetCache[text];
            }
            Cubemap gameObject = cachedAssetBundle.LoadAsset_Internal(text, Il2CppType.Of<Cubemap>()).Cast<Cubemap>();
            gameObject.hideFlags |= HideFlags.DontUnloadUnusedAsset;
            assetCache.Add(text, gameObject);
            return gameObject;
        }


    }
}


