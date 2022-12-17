namespace Vanilla.Helpers
{
    internal class UnityExplorerHelper
    {
        /*  internal static void CheckLegacyExplorerFolder()
          {
              string legacyPath = Path.Combine(Loader.ExplorerFolderDestination, "UnityExplorer");
              if (Directory.Exists(legacyPath))
              {
                  Log("UnityExplorer",$"Attempting to migrate old 'UnityExplorer/' folder to 'sinai-dev-UnityExplorer/'...");

                  // If new folder doesn't exist yet, let's just use Move().
                  if (!Directory.Exists(ExplorerFolder))
                  {
                      try
                      {
                          Directory.Move(legacyPath, ExplorerFolder);
                          Log("UnityExplorer","Migrated successfully.");
                      }
                      catch (Exception ex)
                      {
                          Log("UnityExplorer", $"Exception migrating folder: {ex}");
                      }
                  }
                  else // We have to merge
                  {
                      try
                      {
                         ExplorerCore.CopyAll(new(legacyPath), new(ExplorerFolder));
                          Directory.Delete(legacyPath, true);
                          Log("UnityExplorer", "Migrated successfully.");
                      }
                      catch (Exception ex)
                      {
                          Log("UnityExplorer", $"Exception migrating folder: {ex}");
                      }
                  }
              }
          }*/
    }
}
