// /*
//  *
//  * VanillaClient - GameObjectUtils.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using UnityEngine;

namespace Vanilla.Utils
{
    internal static class GameObjectUtils
    {
        public static GameObject FindObject(this GameObject parent, string name)
        {
            Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
            foreach (var t in trs)
            {
                if (t.name == name)
                {
                    return t.gameObject;
                }
            }

            return null;
        }
    }
}
