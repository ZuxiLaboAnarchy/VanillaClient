namespace Vanilla.Modules
{

    internal class DiscordManager : VanillaModule
    {
        internal static DiscordRPC.RichPresence presence;
        internal static DiscordRPC.EventHandlers eventHandlers;


        public override void Start()
        {
            eventHandlers = default(DiscordRPC.EventHandlers);
            eventHandlers.errorCallback = delegate (int code, string message) { };
            presence.state = $".gg/hvl";

            presence.details = "Encrypted";
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



        public override void LateStart()
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

        public override void WaitForPlayer()
        {

        }

        public static string largeImage = "https://files.hvls.cloud/Cypher/KEzARUlo09.png";

        public static string largeImageText = "By Cypher";

        public static string smallimage = "";

        public static string smallImageText = "";

        public static string details = $"World Boss ({Cypher.CoreMain.ReleaseID})";

        public static string state = "hvl.gg/discord";
    }
}

