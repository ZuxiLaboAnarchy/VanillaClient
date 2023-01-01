using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Vanilla.Utils
{
    internal static class GameObjectUtils
    {
        public static GameObject FindObject(this GameObject parent, string name)
        {
            Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trs)
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
