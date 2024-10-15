namespace Vanilla.Modules
{

    internal class DiscordManager : VanillaModule
    {
        protected override string ModuleName => "Discord Manager";
        internal static DiscordRPC.RichPresence presence;
        internal static DiscordRPC.EventHandlers eventHandlers;


        internal override void Start()
        {
            eventHandlers = default(DiscordRPC.EventHandlers);
            eventHandlers.errorCallback = delegate (int code, string message) { };
            presence.state = $"upset.moe";

            presence.details = "Sweet Like Candy";
            presence.largeImageKey = largeImage;
            presence.largeImageText = "by Vanilla Labs";
            presence.smallImageKey = "<3";
            presence.smallImageText = "";
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
       
        internal static string largeImage = "zuxilogo";

        internal static string largeImageText = "By Zuxi";

        internal static string smallimage = "discordrpcsmall";

        internal static string smallImageText = "When in The light You are still in the dark";

        internal static string details = $"Opsie Woopsie";

        internal static string state = "upset.moe";
    }
}

