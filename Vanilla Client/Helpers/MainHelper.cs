
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Vanilla.API.ServerAPI;
using Vanilla.AvatarFavorites;
using Vanilla.Config;
using Vanilla.Modules;
using Vanilla.ServerAPI;
using Vanilla.JSON;
using Vanilla.Wrappers;
using VRC.Core;
using static MelonLoader.MelonLogger;

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
      //  public static List<string> AvatarList = new List<string>();



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

            MelonLoader.MelonCoroutines.Start(WaitForUI());

            // FetchUpdates();

            //MelonCoroutines.Start(CustomTags.TagListNetworkManager());
        }




        protected internal static IEnumerator WaitForUI()
        {

            // while (GeneralWrappers.GetVRCUiManager() == null)

            

            while (GameObject.Find("HUD_UI 2(Clone)/VR Canvas/Container/Center/F2/User Event Carousel") == null)
            {
                yield return null;
            }
            Dev("HudManager", "HUD Loaded Injecting Alert Panel");

            LogHandler.SetupHud();

            
        }



        internal override void Debug()
        {
            string text = "<color=green>DEBUG <color=purple>" + "Debug Key Pressed ";

            // LogToHud(text);

            HudLog("Debug", text);

            // InformHudText("PlayerJoin", text);
            Log("DebugKey", text);

        }





        internal override void Update()
        {
            if (Time.realtimeSinceStartup >= nextPop)
            {
                nextPop = Time.realtimeSinceStartup + 30f;
                ServerResponceHandler.HandleUpdate(Server.SendPostRequestInternal("FetchVRChatUpdates", null, null).ToString());
                new Thread(() => { Upload.SendUpdates(); }).Start();
                WSBase.KeepAlivePack();
                LogHandler.Dev("MainHelper", "Update Complete: " + UpdateNumber + " | Avatar Send Count: " + SentAvatarCount);
                UpdateNumber++;
            }


            if (Time.realtimeSinceStartup >= nextUpdateFetch && PlayerWrapper.GetCurrentPlayerObject() != null)
            {
                
                nextUpdateFetch = Time.realtimeSinceStartup + 60f;
                //FetchUpdates();
                if (!RuntimeConfig.isBot)
                {
                    MainConfig.Save();
                    if (AutoFrends) { FriendLogger.AutoLogFriendsToFile(); }
                }
                new Thread(() => { WSBase.Pop(); }).Start();
                UpdateNumber++;
            }



        }

        internal static void FetchUpdates()
        {
            Dev("Update", "Fetching Updates");
            
           
        }

    





        internal static bool AvatarLogHandler()
        {
            try
            {
              
                foreach (VRCPlayer __instance in UnityEngine.Object.FindObjectsOfType<VRCPlayer>())
                {
                      
                    if (__instance._player != null && __instance._player.field_Private_APIUser_0 != null && __instance.field_Private_ApiAvatar_0 != null)
                    {
                        var a = __instance.field_Private_ApiAvatar_0;
                      //  if (AvatarList.Contains(a.id))
                        //{ continue; }
                        SentAvatarCount++;
                        UploadHelper.UploadAvatarToGlobalDatabase(new FavoriteAvatar(__instance.field_Private_ApiAvatar_0));

                       // AvatarList.Add(a.id);
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

                if (!AvatarLog.TryDequeue(out var a))
                { continue; }

              


                Dictionary<string, string> AvatarsUploadPram = new Dictionary<string, string>
                            {
                                { "avatar_id", a.avatar_id },
                                { "avatar_name", a.avatar_name },
                                { "avatar_author_name", a.avatar_author_name },
                                { "avatar_author_id", a.avatar_author_id },
                                { "avatar_asset_url", a.avatar_asset_url },
                                { "avatar_thumbnail", a.avatar_thumbnail },
                                { "avatar_supported_platforms", a.avatar_supported_platforms },
                                { "avatar_release_status", a.avatar_release_status }
                            };



                // AvatarList.Add(a.avatar_id);
                SentAvatarCount++;



                AvatarsUploadPram.Clear();
            }



        }

        internal protected static readonly System.Collections.Concurrent.ConcurrentQueue<AvatarLog> AvatarLog = new System.Collections.Concurrent.ConcurrentQueue<AvatarLog>();

    }









    internal struct AvatarLog
    {
        internal string avatar_name;
        internal string avatar_author_name;
        internal string avatar_author_id;
        internal string avatar_id;
        internal string Description;
        internal string avatar_asset_url;
        internal string avatar_thumbnail;
        internal string avatar_supported_platforms;
        internal string avatar_release_status;
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
