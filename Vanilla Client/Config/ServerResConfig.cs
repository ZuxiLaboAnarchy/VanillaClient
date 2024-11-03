namespace Vanilla.Config
{
    internal class ServerResponce
    {
        internal string Username { get; set; }
        internal string IsStaff { get; set; }
        internal string AquiredVIA { get; set; }
        internal string UUID { get; set; }
        internal bool AllowMal { get; set; }
        internal string ServerUpdates { get; set; }
        internal string message { get; set; }
    }

    internal class AvatarLog
    {
        internal string AvatarName { get; set; }
        internal string Author { get; set; }
        internal string Authorid { get; set; }
        internal string Avatarid { get; set; }
        internal string Description { get; set; }
        internal string Asseturl { get; set; }
        internal string Image { get; set; }
        internal string Platform { get; set; }
        internal string Status { get; set; }
        internal string code { get; set; }
    }
}