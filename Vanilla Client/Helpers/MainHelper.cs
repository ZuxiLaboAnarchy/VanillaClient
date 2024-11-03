// /*
//  *
//  * VanillaClient - MainHelper.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using Vanilla.APIs.ServerAPI;
using Vanilla.Config;
using Vanilla.Misc;
using Vanilla.Misc.AvatarFavorites;
using Vanilla.Modules.Manager;
using Vanilla.Wrappers;
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
        public static List<string> AvatarList = new();


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
        private static WebClient webClient = new();

        internal override void Start()
        {
            GetInstance().Load();
            webClient.Headers["User-Agent"] = "ZuxiUA.Anarchy-AAA7FB29";
            MelonLoader.MelonCoroutines.Start(WaitForUI());

            FetchUpdates();

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

            //LogHandler.SetupHud();

            //GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_MM_WorldDialog/Background_Scrim").SetActive(false);
        }


        internal override void Debug()
        {
            var text = "<color=green>DEBUG <color=purple>" + "Debug Key Pressed ";

            // LogToHud(text);

            HudLog("Debug", text);

            // InformHudText("PlayerJoin", text);
            Log("DebugKey", text);
        }


        internal override void Update()
        {
            if (Time.realtimeSinceStartup >= nextPop)
            {
                nextPop = Time.realtimeSinceStartup + 10f;
                //TODO Update API for Anarchy bc this is based off of old VRC stuff wich is still used lol
                //   new Thread(() => { ServerResponceHandler.HandleUpdate(Server.SendPostRequestInternal("FetchVRChatUpdates", null, null).ToString()); }).Start();
                //   new Thread(() => { Upload.SendUpdates(); }).Start();
                WSBase.KeepAlivePack();
                //   LogHandler.Dev("MainHelper", "Webcock Complete: " + UpdateNumber);
                //  UpdateNumber++;
            }


            if (Time.realtimeSinceStartup >= nextUpdateFetch && PlayerWrapper.GetCurrentPlayerObject() != null)
            {
                nextUpdateFetch = Time.realtimeSinceStartup + 60f;
                FetchUpdates();
                if (!RuntimeConfig.isBot)
                {
                    //  MainConfig.GetInstance().Save();
                    if (GetInstance().AutoFrends)
                    {
                        FriendLogger.AutoLogFriendsToFile();
                    }

                    WSBase.KeepAlivePack();
                    Dev("MainHelper", "Update Complete: " + UpdateNumber);
                }
                //   new Thread(() => { WSBase.Pop(); }).Start();
                //UpdateNumber++;
            }
        }

        internal static void FetchUpdates()
        {
            Dev("Update", "Fetching Updates");

            var data = webClient.DownloadString("https://anarchy.zuxi.dev/api/getdata");

            ServerResponceHandler.HandleUpdate(data);
        }


        internal static bool AvatarLogHandler()
        {
            try
            {
                foreach (var __instance in UnityEngine.Object.FindObjectsOfType<VRCPlayer>())
                {
                    if (__instance._player != null && __instance._player.field_Private_APIUser_0 != null &&
                        __instance.field_Private_ApiAvatar_0 != null)
                    {
                        var a = __instance.field_Private_ApiAvatar_0;
                        if (AvatarList.Contains(a.id))
                        {
                            continue;
                        }

                        AvatarList.Add(a.id);
                        SentAvatarCount++;
                        UploadHelper.UploadAvatarToGlobalDatabase(
                            new FavoriteAvatar(__instance.field_Private_ApiAvatar_0));
                    }
                    else
                    {
                        Dev("Avatar", "Found Avatar Already");
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler("AvatarLogger", e);
            }

            return true;
        }

        internal static void PopAvatarLog()
        {
            for (var i = 0; i < AvatarLog.Count; i++)
            {
                if (!AvatarLog.TryDequeue(out var a))
                {
                    continue;
                }


                var AvatarsUploadPram = new Dictionary<string, string>
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

        protected internal static readonly System.Collections.Concurrent.ConcurrentQueue<AvatarLog> AvatarLog = new();
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
