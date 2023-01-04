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
                Dev("Game Has Updated. Update Me you Fucking Whore", ConsoleColor.Red);
                Dev("Core", "CURRENT GAMEVERSION");
                Dev("Core" , MelonLoader.InternalUtils.UnityInformationHandler.GameVersion);
                ShouldLoad = true;

            }
#else
            if (MelonLoader.InternalUtils.UnityInformationHandler.GameVersion != Properties.Resources.GameVersion)
            {
                Log("Core","Game Has Updated Please Wait For Cypher To Update", ConsoleColor.Red);
                Log("Core", "Skipping Load...", ConsoleColor.Red);
                ShouldLoad = false;
                
            }
#endif

            return ShouldLoad;
        }



    }
}
