namespace Vanilla.Protections
{
    internal class LoaderProtections
    {
        // LoaderID is used to verify that the correct loader is being used
        private const string LoaderID = "e16c539d-ea67-45ae-b41f-4e75fc7d0f6d";

        internal protected static bool CheckLoader(string LoaderID)
        {
            if (LoaderID != LoaderProtections.LoaderID)
            {
                Utils.LogHandler.Log("Verifier", "Core Failed Verification", ConsoleColor.Yellow);
                Utils.LogHandler.Log("Verifier", "For Your Protection VanillaClient Will not Load", ConsoleColor.Yellow);

                return false;
            }
            return true;
        }
    }
}