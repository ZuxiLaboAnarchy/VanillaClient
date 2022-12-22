using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Vanilla.Config;
using Vanilla.Modules;
using Vanilla.ServerAPI;
using Vanilla.TinyJSON;
using Vanilla.Wrappers;
using VRC.Core;

namespace Vanilla.Helpers
{
    internal class MainHelper : VanillaModule
    {
        protected override string ModuleName => "MainHelper";
        internal static int SentAvatarCount = 0;
        internal static int UpdateNumber = 0;
        internal static int GameUpdate = 0;

        private float nextPop = 0f;
        private float nextUpdateFetch = 0f;
        static List<string> AvatarList = new List<string>();



        /*private static System.Timers.Timer WSHelperTimer;
           internal override void Start()
           {
               new Thread(() => { ThreadStart(); }).Start();
           }


           internal static void ThreadStart()
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
            
           // FetchUpdates();

            //MelonCoroutines.Start(CustomTags.TagListNetworkManager());
        }


        internal override void Update()
        {
            if (Time.realtimeSinceStartup >= nextPop && PlayerWrapper.GetCurrentPlayerObject() != null)
            {
                nextPop = Time.realtimeSinceStartup + 30f;
                
                new Thread(() => { WSBase.Pop(); }).Start();
                new Thread(() => { PopAvatarLog(); }).Start();
               // WSBase.Pop();
               // PopAvatarLog();
                if (!RuntimeConfig.isBot)
                {
                    MainConfig.Save();
                    if (AutoFrends) { FriendLogger.AutoLogFriendsToFile(); }
                }
                LogHandler.Dev("MainHelper", "Update Complete: " + UpdateNumber + " | Avatar Send Count: " + SentAvatarCount);
                UpdateNumber++;
            }



            if (Time.realtimeSinceStartup >= nextUpdateFetch && PlayerWrapper.GetLocalAPIUser() != null)
            {
                nextUpdateFetch = Time.realtimeSinceStartup + 60f;
                FetchUpdates();
                UpdateNumber++;
            }



        }

        internal static void FetchUpdates()
        {
            Dev("MainHelper", "Fetching Updates");
            var FetchModelRaw = new sendsinglemsg()
            {
                uid = APIUser.CurrentUser.id,

                code = "1",

                Key = ServerHelper.GetKey(),
            };
            WSBase.sendmsg($"{Json.Encode(FetchModelRaw)}");

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
                        }
                        catch { }

                        Pop();
                    }
                }
            }
            catch (Exception e)
            {
                LogHandler.ExceptionHandler("AvatarLogger", e);
            }
            return true;
        }

        internal static void PopAvatarLog()
        {
            for (int i = 0; i < AvatarLog.Count; i++)
            {
                if (!RuntimeConfig.WSAuthed)
                { WSBase.wss.Connect(); continue; }

                if (!WSBase.wss.IsAlive && WSBase.HasConn)
                { WSBase.wss.Connect(); continue; }

                if (!AvatarLog.TryDequeue(out var a))
                { continue; }

                if (a.code == null)
                { Log("Server API", "Failed To Send Message Message Identifyer was Null", ConsoleColor.Red, "AvaterLog"); }
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
                WSBase.sendmsg(Json.Encode(PopMessage));
            }



        }

        internal protected static readonly System.Collections.Concurrent.ConcurrentQueue<AvatarLog> AvatarLog = new System.Collections.Concurrent.ConcurrentQueue<AvatarLog>();

    }
    internal struct AvatarLog
    {
        internal string AvatarName;
        internal string Author;
        internal string Authorid;
        internal string Avatarid;
        internal string Description;
        internal string Asseturl;
        internal string Image;
        internal string Platform;
        internal string Status;
        internal string code;

    }

    internal class AvatarSender
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
        internal string UserKey { get; set; }
        internal string HWID { get; set; }
    }
}
