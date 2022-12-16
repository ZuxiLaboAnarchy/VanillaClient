using MelonLoader;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Vanilla.Config;
using Vanilla.Modules;
using Vanilla.Patches.Harmony;
using Vanilla.ServerAPI;

namespace Vanilla.Helpers
{
    internal class MainHelper : VanillaModule
    {
        internal static int SentAvatarCount = 0;
        private float nextPop = 0f;
        static List<string> AvatarList = new List<string>();



        /*private static System.Timers.Timer WSHelperTimer;
           public override void Start()
           {
               new Thread(() => { ThreadStart(); }).Start();
           }


           public static void ThreadStart()
           {
               WSHelperTimer = new System.Timers.Timer(10000);
               WSHelperTimer.Elapsed += CheckConnection;
               WSHelperTimer.AutoReset = true;
               WSHelperTimer.Enabled = true;
               Dev("ServerAPI", "Started Heart Beat");
           }


           private static void CheckConnection(Object source, ElapsedEventArgs e)
           {
               WSBase.IsConnected();



       

           }*/

        internal override void Start()
        {
            MainConfig.Load();
            WSBase.Pop();
            MelonCoroutines.Start(CustomTags.TagListNetworkManager());
        }


        internal override void Update()
        {
            if (Time.realtimeSinceStartup >= nextPop)
            {
                nextPop = Time.realtimeSinceStartup + 20f;
                LogHandler.Dev("MainHelper", "Saving Config");
                MainConfig.Save();
                LogHandler.Dev("MainHelper", "Poping WS Bubble");
                new Thread(() => { WSBase.Pop(); }).Start()  ;
                LogHandler.Dev("MainHelper", "Poping Avatar Log");
                new Thread(() => { PopAvatarLog(); }).Start();
                if (AutoFrends) { FriendLogger.AutoLogFriendsToFile(); }
                // WSBase.sendmessage("Test", "1", false);
            }

        }

        internal static bool AvatarLogHandler()
        {

            try
            {
                foreach (VRCPlayer __instance in UnityEngine.Object.FindObjectsOfType<VRCPlayer>())
                {
                    if (__instance._player != null && __instance._player.field_Private_APIUser_0 != null && __instance.field_Private_ApiAvatar_0 != null)
                    {




                        try
                        {
                            var a = __instance.field_Private_ApiAvatar_0;
                           
                                



                            AvatarLog.Enqueue(new AvatarLog
                            {
                                AvatarName = a.name,

                                Author = a.authorName,

                                Authorid = a.authorId,

                                Avatarid = a.id,

                                Description = a.description,

                                Asseturl = a.assetUrl,

                                Image = a.imageUrl,

                                Platform = a.platform,

                                Status = a.releaseStatus,

                                code = "9",
                            });
                            

                            //WSBase.sendmsg($"{JsonConvert.SerializeObject(senda)}");
                        }
                        catch { }

                        Pop();





                        ///Logging.ExecuteLog(player._player, true);
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.ExceptionHandler("AvatarPatch", e);
            }




            return true;
        }



        internal static void PopAvatarLog()
        {
            for (int i = 0; i < AvatarLog.Count; i++)
            {
                if (!WSBase.wss.IsAlive && WSBase.HasConn)
                { WSBase.wss.Connect(); continue; }

                if (!AvatarLog.TryDequeue(out var a))
                { continue; }

                if (a.code == null)
                { Log("Server API", "Failed To Send Message Message Identifyer wass Null", ConsoleColor.Red, "OnPop"); }
                if (AvatarList.Contains(a.Avatarid))
                { continue; }


                    var PopMessage = new AvatarSender()
                {
                    AvatarName = a.AvatarName,

                    Author = a.Author,

                    Authorid = a.Authorid,

                    Avatarid = a.Avatarid,

                    Description = a.Description,

                    Asseturl = a.Asseturl,

                    Image = a.Image,

                    Platform = a.Platform,

                    Status = a.Status,

                    code = "9",

                    UserKey = ServerHelper.GetKey(),

                    HWID = ServerHelper.GetHWID(),
                };

                AvatarList.Add(a.Avatarid);
                  SentAvatarCount++;   
                WSBase.sendmsg(JsonConvert.SerializeObject(PopMessage));
            }

            Dev("Avatar", "Send Count: " + SentAvatarCount);

        }

        internal protected static readonly ConcurrentQueue<AvatarLog> AvatarLog = new ConcurrentQueue<AvatarLog>();

    }
    internal struct AvatarLog
    {
        public string AvatarName;
        public string Author;
        public string Authorid;
        public string Avatarid;
        public string Description;
        public string Asseturl;
        public string Image;
        public string Platform;
        public string Status;
        public string code;

    }

    internal class AvatarSender
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
        public string UserKey { get; set; }
        public string HWID { get; set; }
    }
}
