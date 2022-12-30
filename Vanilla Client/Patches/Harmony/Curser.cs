﻿using System.Reflection;
using UnityEngine;

namespace Vanilla.Patches.Harmony
{
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    internal class CurserPatch : VanillaPatches
    {

        protected override string patchName => "CurserPatch";

        internal override void Patch()
        {
            try
            {
                InitializeLocalPatchHandler(typeof(CurserPatch));

                PatchMethod(typeof(Cursor).GetProperty("lockState").GetSetMethod(), GetLocalPatch("CursorSetLockStatePatch"), null);
                PatchMethod(typeof(Cursor).GetProperty("visible").GetSetMethod(), GetLocalPatch("CursorSetVisiblePatch"), null);

            }
            catch (Exception e)
            {
                Utils.LogHandler.ExceptionHandler(patchName, e);
            }
        }



        private static bool CursorSetLockStatePatch()
        {
            return false;
        }

        private static bool CursorSetVisiblePatch()
        {
            return false;
        }
    }
}
