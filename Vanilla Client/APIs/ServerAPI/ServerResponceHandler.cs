using Newtonsoft.Json.Linq;
using UnityEngine;
using Vanilla.AvatarFavorites;
using Vanilla.Config;
using Vanilla.TagManager;
using Vanilla.Wrappers;
using WebSocketSharp;

namespace Vanilla.ServerAPI
{


    internal class ServerResponceHandler
    {

        internal static bool WSDone = true;
     

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
        
        static string LastUpdate = null;
        internal static void HandleUpdate(string WSResponce)
        {
            Dev("RH", "Handleing Update");
            //  Dev("WSSRH", WSResponce);
            

            if (WSResponce.IsNullOrEmpty())
            {
                Dev("PlayerChanges", "Update Was Null Returning");
                return;
            }


            if (WSResponce == LastUpdate && !RuntimeConfig.isForced)
            {
                Dev("PlayerChanges", "Update Was Same Returning");
                return;
            }
            LastUpdate = WSResponce;

            RuntimeConfig.isForced = false;

            string text = WSResponce.Trim();
            if (string.IsNullOrEmpty(text))
            {
                return;
            }
            JObject jObject = JObject.Parse(text);
            //   WSDone = false;


            try
            {
                PlayerUtils.playerCustomTags.Clear();
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
                    string CustomTag = ((string?)jArray3[k]["custom_Tag"])?.Trim();
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


                    Dev("SRH", "Adding: " + VRChatID + " To Tag List");


                    //  if (!PlayerUtils.playerCustomTags.ContainsKey(VRChatID) && VRChatID != string.Empty)
                    PlayerUtils.playerCustomTags.Add(VRChatID, customtag);

                    PlayerInformation playerInformationByID = PlayerWrapper.GetPlayerInformationByID(VRChatID);
                    if (playerInformationByID != null && TagEnabled)
                    {
                        PlayerUtils.playerColorCache[playerInformationByID.displayName] = color;
                    }
                }

                Dev("SRH", "Finished Handleing TagList ");



            }
            catch (Exception e) { ExceptionHandler("SRH", e); }



            return;

#pragma warning disable CS0162 // Unreachable code detected
            JArray jArray4 = (JArray)jObject["Avatars"];
#pragma warning restore CS0162 // Unreachable code detected
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
