namespace Vanilla.Config
{

    internal class ServerResponce
    {
        public string Username { get; set; }
        public string IsStaff { get; set; }
        public string AquiredVIA { get; set; }
        public string UUID { get; set; }
        public bool AllowMal { get; set; }
        public string ServerUpdates { get; set; }
        public string message { get; set; }
    }

    internal class AvatarLog
    {
        public string AvatarName { get; set; }
        public string Author { get; set; }
        public string Authorid { get; set; }
        public string Avatarid { get; set; }
        public string Description { get; set; }
        public string Asseturl { get; set; }
        public string Image { get; set; }
        public string Platform { get; set; }
        public string Status { get; set; }
        public string code { get; set; }
    }
}
