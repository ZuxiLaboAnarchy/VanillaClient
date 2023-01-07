﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests.SkeinEngine;

namespace Vanilla.Utils
{
    internal class CacheUtils
    {
        private static readonly ConcurrentDictionary<string, Texture2D> cachedImages = new ConcurrentDictionary<string, Texture2D>();

        private static readonly string imageCachePath = FileHelper.GetCheatFolder() + "Image Cache/";
        private static string TrimFileID(string id)
        {
            string result;
            try
            {
                string[] array = id.Split('_');
                if (array.Length == 1)
                {
                    int num = array[0].LastIndexOf('/');
                    if (num == -1)
                    {
                        result = "file_" + array[0];
                    }
                    else
                    {
                        string text = array[0].Substring(num + 1);
                        int length = text.LastIndexOf('.');
                        result = "file_" + text.Substring(0, length);
                    }
                }
                else
                {
                    int num2 = array[1].IndexOf('/');
                    result = ((num2 != -1) ? ("file_" + array[1].Substring(0, num2)) : ("file_" + array[1]));
                }
            }
            catch (Exception e)
            {
                result = id;
               ExceptionHandler("Cache", e);
            }
            return result;
        }

        internal static Texture2D GetCachedImage(string imageName)
        {
            string text = TrimFileID(imageName);
            if (cachedImages.ContainsKey(text))
            {
                return cachedImages[text];
            }
            string path = imageCachePath + text + ".png";
            if (!File.Exists(path))
            {
                return null;
            }
            byte[] array = File.ReadAllBytes(path);
            Texture2D texture2D = new Texture2D(2, 2);
            if (!ImageConversion.LoadImage(texture2D, array))
            {
                return null;
            }
            cachedImages.TryAdd(text, texture2D);
            return texture2D;
        }

        internal static void AddCachedImage(string imageName, Texture2D image)
        {
            string text = TrimFileID(imageName);
            if (!cachedImages.ContainsKey(text))
            {
                cachedImages.TryAdd(text, image);
                if (!Directory.Exists(imageCachePath))
                {
                    Directory.CreateDirectory(imageCachePath);
                }
                string path = imageCachePath + text + ".png";
                if (!File.Exists(path) && !FileHelper.SaveTextureToDisk(image, imageCachePath, text))
                {
                    Log("Performance", "Failed To Cache Img");
                }
            }
        }



    }
}
