using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanillaClient.Patches.Harmony
{
    internal class Ani : VanillaPatches
    {
        public override void Patch()
        {
            PatchMethod(typeof(Analytics).GetMethod(nameof(Analytics.Update)), GetPatch(nameof(ReturnFalse)), null);
        }
    }
}
