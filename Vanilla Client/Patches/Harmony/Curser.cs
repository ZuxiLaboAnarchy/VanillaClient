using UnityEngine;

namespace VanillaClient.Patches.Harmony
{
    internal class CurserPatch : VanillaPatches
    {

        protected override string patchName => "CurserPatch";

        public override void Patch()
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
