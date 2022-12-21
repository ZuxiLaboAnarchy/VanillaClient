using MelonLoader;
using Vanilla.Config;

namespace Vanilla.Helpers
{
    internal class Compatibility
    {

        private static bool ShouldLoad { get; set; } = true;

        internal static bool CheckGameVersion()
        {

#if DEBUG

            if (MelonLoader.InternalUtils.UnityInformationHandler.GameVersion != RuntimeConfig.GameVER)
            {
                MelonLogger.Error("Game Has Updated. Update Me you Fucking Whore");
                MelonLogger.Error("CURRENT GAMEVERSION");
                MelonLogger.Msg(MelonLoader.InternalUtils.UnityInformationHandler.GameVersion);
                ShouldLoad = true;

            }
#else
            if (MelonLoader.InternalUtils.UnityInformationHandler.GameVersion != Properties.Resources.GameVersion)
            {
                MelonLogger.Error("Game Has Updated Please Wait For Cypher To Update");
                MelonLogger.Error("Skipping Load...");
                ShouldLoad = false;
                
            }
#endif

            return ShouldLoad;
        }



    }
}
