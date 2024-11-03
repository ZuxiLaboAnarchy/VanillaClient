// /*
//  *
//  * VanillaClient - MonoBehavior.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using UnityEngine;
using Vanilla.Modules.Manager;

namespace Vanilla.Modules.VanillaGameObject
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class VanillaBehavior : MonoBehaviour
    {
        public VanillaBehavior(IntPtr ptr) : base(ptr)
        {
        }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            ModuleManager.OnApplicationFocus(hasFocus);
        }
    }
}
