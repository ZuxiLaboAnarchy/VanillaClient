namespace Vanilla.Patches.Harmony
{
    internal class AntiConsoleClearPatch : VanillaPatches
    {
        internal override void Patch()
        {
            try
            {
                InitializeLocalPatchHandler(typeof(AntiConsoleClearPatch));
                PatchMethod(typeof(Console).GetMethod(nameof(Console.Clear)), GetLocalPatch(nameof(ReturnFalse)), null);
            }
            catch (Exception e)
            {
                ExceptionHandler(patchName, e);
            }
        }

        private static bool ReturnFalse()
        {
            return false;
        }
    }
}