using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Vanilla.Utils
{
    internal class MiscUtils
    {
        static bool NotoInstalled = false;
        internal static Vector3 GetNameplateOffset(bool open)
        {
           // if (CompatibilityLayer.IsNotoriousInstalled())
           if (NotoInstalled)
            {
                return open ? new Vector3(0f, -85f, 0f) : new Vector3(0f, -58f, 0f);
            }
            return open ? new Vector3(0f, 60f, 0f) : new Vector3(0f, 30f, 0f);
        }
    }
}
