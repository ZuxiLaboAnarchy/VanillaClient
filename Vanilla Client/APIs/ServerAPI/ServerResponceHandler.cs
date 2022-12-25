using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vanilla.AvatarFavorites;
using Vanilla.Config;
using Vanilla.TagManager;
using Vanilla.Wrappers;
using VRC.Core;
using VRC.SDKBase.Validation.Performance;

namespace Vanilla.ServerAPI
{
    internal class ServerResponceHandler
    {
        internal static void HandlePostRequest(string PostRequestData)
        {
            Dev("ServerAPI", "Handleing \n " + PostRequestData);
            string text = PostRequestData.Trim();
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            JObject jObject = JObject.Parse(text);

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
            ServerHelper.SetJWT((string?)jObject["Token"]);

        }

        internal static void HandleWSUpdate(string WSResponce)
        {
            string text = WSResponce.Trim();
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            JObject jObject = JObject.Parse(text);



            try
            {
                TagUtils.TagList.Clear();


              
             
                RuntimeConfig.SetUserName((string?)jObject["Username"]);
                RuntimeConfig.SetStaff((string?)jObject["IsStaff"]);
                RuntimeConfig.SetUUID((string?)jObject["UUID"]);
                RuntimeConfig.SetSubTime((string?)jObject["SubTime"]);
                RuntimeConfig.SetCrashingAvatarPC((string?)jObject["PCCrash"]);
                RuntimeConfig.SetCrashingAvatarQuest((string?)jObject["QuestCrash"]);


                JArray jArray3 = (JArray)jObject["TagList"];
                for (int k = 0; k < jArray3.Count; k++)
                {
                    string VRChatID = ((string?)jArray3[k]["vrchat_id"])?.Trim();
                    string CustomTag = ((string?)jArray3[k]["Custom_Tag"])?.Trim();
                    string CustomTagColor = ((string?)jArray3[k]["custom_Tag_color"])?.Trim();
                    Color color = default(Color);
                    bool TagEnabled = false;
                    bool customRankEnabled = false;
                    if (CustomTagColor != null && !string.IsNullOrEmpty(CustomTagColor))
                    {
                        TagEnabled = ColorUtility.TryParseHtmlString(CustomTagColor, out color);
                    }
                    if (CustomTag != null)
                    {
                        customRankEnabled = !string.IsNullOrEmpty(CustomTag);
                    }
                    CustomTagInfo customtag = new CustomTagInfo
                    {
                        customTagEnabled = customRankEnabled,
                        customTag = CustomTag,
                        customTagColorEnabled = TagEnabled,
                        customTagColor = color
                    };

                    if (!TagUtils.TagList.ContainsKey(VRChatID))
                    TagUtils.TagList.Add(VRChatID, customtag);
                    /* PlayerInformation playerInformationByID = PlayerWrappers.GetPlayerInformationByID(text2);
                     if (playerInformationByID != null && flag)
                     {
                         PlayerUtils.playerColorCache[playerInformationByID.displayName] = color;
                     }*/
                }
                Dev("SRH", "Finished Handleing TagList");
                RuntimeConfig.nextUpdateCheckComplete = true;
            }catch (Exception e) { ExceptionHandler("SRH", e); }



                return;

            JArray jArray4 = (JArray)jObject["Avatars"];
            for (int l = 0; l < jArray4.Count; l++)
            {
                FavoriteAvatar favoriteAvatar = new FavoriteAvatar
                {
                    AvatarName = (string?)jArray4[l]["avatar_name"],
                    AvatarID = (string?)jArray4[l]["avatar_id"],
                    AvatarAssetUrl = (string?)jArray4[l]["avatar_asset_url"],
                    AvatarImageUrl = (string?)jArray4[l]["avatar_thumbnail"],
                    AvatarReleaseStatus = "public",
                    AvatarAuthorID = (string?)jArray4[l]["avatar_author_id"],
                    AvatarAuthorName = (string?)jArray4[l]["avatar_author_name"],
                };
                if (favoriteAvatar.AvatarVersionSystem == 2)
                {
                    if (!FavoriteAvatarHandler.VanillaAvatarsFav.ContainsKey(favoriteAvatar.AvatarID))
                    {
                        FavoriteAvatarHandler.VanillaAvatarsFav.Add(favoriteAvatar.AvatarID, favoriteAvatar);
                    }
                    else
                    {
                        FavoriteAvatarHandler.VanillaAvatarsFav[favoriteAvatar.AvatarID] = favoriteAvatar;
                    }
                    continue;
                }
                if (FavoriteAvatarHandler.VanillaAvatarsFav.ContainsKey(favoriteAvatar.AvatarID))
                {
                    FavoriteAvatarHandler.VanillaAvatarsFav.Remove(favoriteAvatar.AvatarID);
                }
              //  DeleteAvatarFromDatabase(favoriteAvatar);
                FavoriteAvatarHandler.AddAvatarByID(favoriteAvatar.AvatarID, null, null);
            }
            if (FavoriteAvatarHandler.VanillaAvatarsFav.Count > 0)
            {
               // FavoriteAvatarHandler.RefreshList(string.Empty);
            }
            Dev("SRH", "Finished Handleing Avatar Favs");




#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.



            Dev("ServerAPI", "Done Handleing WS Responce");

        }




    }
}
