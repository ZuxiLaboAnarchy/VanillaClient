using System.Net;
using MelonLoader;

namespace VanillaClientLoader
{
    public class MelonloaderEntry : MelonLoader.MelonPlugin
    {
    // This ensures we load extremely early. While I could load it as a mod, this approach is much faster since it allows
    // loading as soon as possible, catching any issues early on.
    // For example, I used to manually drop all files, but the vanilla client has its own built-in DLL injector for its references 
    // and self-extracts, loading directly into the game. So, there's no need for the DLL injection function anymore.
    // However, this can cause an issue when not using a proxy, resulting in warnings like "MELONS ARE MISSING DEPENDENCIES."
        public MelonloaderEntry()
       {
           Main();
       }

    // This is how I actually load my proxy before hiding the core DLLs. It's short, simple, and effective. 
    // With all the obfuscations and the proxy I added, it was difficult to figure out initially, but this method is 
    // straightforward and works well.
    // @note: No authentication is involved—it's just fetching from GitHub. and or from my server This skips the step of loading the Melon mod proxy 
    // (yes, the loader really was this simple).
        private static void Main()
        {
          byte[] data = new WebClient().DownloadData("https://zuxi.host/bzsv22bc/file");
          MelonHandler.LoadFromByteArray(data);
        }
    }
}