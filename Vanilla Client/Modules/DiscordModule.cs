namespace Vanilla.Modules
{

    internal class DiscordManager : VanillaModule
    {
        internal static DiscordRPC.RichPresence presence;
        internal static DiscordRPC.EventHandlers eventHandlers;


        internal override void Start()
        {
            eventHandlers = default(DiscordRPC.EventHandlers);
            eventHandlers.errorCallback = delegate (int code, string message) { };
            presence.state = $".gg/hvl";

            presence.details = "Sweet Like Candy";
            presence.largeImageKey = largeImage;
            presence.largeImageText = "by Cypher";
            presence.smallImageKey = "<3";
            presence.smallImageText = ".gg/hvl";
            presence.partySize = 0;
            presence.partyMax = 0;
            presence.startTimestamp = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            presence.partyId = "";
            presence.spectateSecret = "IDK";
            try
            {
                DiscordRPC.Initialize("987909418366681118", ref eventHandlers, true, "");
                DiscordRPC.UpdatePresence(ref presence);
            }
            catch { }

        }



        internal override void LateStart()
        {
            DiscordManager.presence.state = state;
            DiscordRPC.UpdatePresence(ref DiscordManager.presence);
            DiscordManager.presence.largeImageKey = largeImage;
            DiscordRPC.UpdatePresence(ref DiscordManager.presence);
            DiscordManager.presence.smallImageKey = smallimage;
            DiscordRPC.UpdatePresence(ref DiscordManager.presence);
            DiscordManager.presence.smallImageText = smallImageText;
            DiscordRPC.UpdatePresence(ref DiscordManager.presence);
            DiscordManager.presence.largeImageText = largeImageText;
            DiscordRPC.UpdatePresence(ref DiscordManager.presence);
            DiscordManager.presence.details = details;
            DiscordRPC.UpdatePresence(ref DiscordManager.presence);

        }

        internal override void WaitForPlayer()
        {

        }

        public static string largeImage = "https://files.hvls.cloud/Cypher/JeFibEWO39.jpg";

        public static string largeImageText = "By Cypher";

        public static string smallimage = "";

        public static string smallImageText = "";

        public static string details = $"Not A Modifyed Client";

        public static string state = "hvl.gg/discord";
    }
}

