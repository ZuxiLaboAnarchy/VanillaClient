using System.Collections.Generic;
using Vanilla.Helpers;
using Vanilla.ServerAPI;
using Vanilla.JSON;
using System.Threading;

namespace Vanilla.API.ServerAPI
{
    internal class Upload
    {
        
        private static TempUploadContainer tempUploadContainer;

        internal static void SendUpdates()
        {
            if (UploadHelper.GetUploadQueueConfig().UploadQueue.Count == 0)
            {
                
                return;
            }
            tempUploadContainer = UploadHelper.GetUploadQueueConfig().UploadQueue.Dequeue();
            Dictionary<string, string> parameters;

            if (tempUploadContainer.saved_avatar == null || string.IsNullOrEmpty(tempUploadContainer.saved_avatar.AvatarAssetUrl) || string.IsNullOrEmpty(tempUploadContainer.saved_avatar.AvatarImageUrl))
            {
              
                return;
            }
            int avatarSupportedPlatforms = (int)tempUploadContainer.saved_avatar.AvatarSupportedPlatforms;
            parameters = new Dictionary<string, string>
                {
                    
                    {
                        "avatar_version_system",
                        tempUploadContainer.saved_avatar.AvatarVersionSystem.ToString()
                    },
                    {
                        "avatar_id",
                        tempUploadContainer.saved_avatar.AvatarID
                    },
                    {
                        "avatar_name",
                        tempUploadContainer.saved_avatar.AvatarName
                    },
                    {
                        "avatar_description",
                        tempUploadContainer.saved_avatar.AvatarDescription
                    },
                    {
                        "avatar_version",
                        tempUploadContainer.saved_avatar.AvatarVersion.ToString()
                    },
                    {
                        "avatar_api_version",
                        tempUploadContainer.saved_avatar.AvatarApiVersion.ToString()
                    },
                    {
                        "avatar_asset_url",
                        tempUploadContainer.saved_avatar.AvatarAssetUrl
                    },
                    {
                        "avatar_thumbnail",
                        tempUploadContainer.saved_avatar.AvatarImageUrl
                    },
                    {
                        "avatar_release_status",
                        tempUploadContainer.saved_avatar.AvatarReleaseStatus
                    },
                    {
                        "avatar_author_id",
                        tempUploadContainer.saved_avatar.AvatarAuthorID
                    },
                    {
                        "avatar_author_name",
                        tempUploadContainer.saved_avatar.AvatarAuthorName
                    },
                    {
                        "avatar_platform",
                        tempUploadContainer.saved_avatar.AvatarPlatform
                    },
                    {
                        "avatar_supported_platforms",
                        avatarSupportedPlatforms.ToString()
                    }
                };
            new Thread(() => { Server.SendPostRequestInternal("vrchat/upload-avatar", parameters, OnUploadFinished); }).Start();


        }

        internal static void OnUploadFinished(bool error, string response)
        {
            if (error)
            {
                UploadHelper.GetUploadQueueConfig().UploadQueue.Enqueue(tempUploadContainer);
               
                Log("UploadAPI", "Failed To Upload Data Server down? Report to Cypher", ConsoleColor.Red);
                return;
            }
            string text = response.Trim();
            if (string.IsNullOrEmpty(text))
            {
                UploadHelper.GetUploadQueueConfig().UploadQueue.Enqueue(tempUploadContainer);
               
                Log("UploadAPI", "Failed To Upload Server down? Report to Cypher", ConsoleColor.Red);
                return;
            }
            if (UploadHelper.GetUploadQueueConfig().UploadQueue.Count != 0)
            {
                SendUpdates();
            }

      
        }
    }
}

