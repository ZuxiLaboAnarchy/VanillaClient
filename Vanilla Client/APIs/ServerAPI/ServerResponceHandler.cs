using Newtonsoft.Json.Linq;
using UnityEngine;
using Vanilla.AvatarFavorites;
using Vanilla.Config;
using Vanilla.Modules;
using Vanilla.TagManager;
using Vanilla.Wrappers;

namespace Vanilla.ServerAPI
{


    internal class ServerResponceHandler
    {

        internal static bool WSDone = true;
     

        internal static void HandlePostRequest(string PostRequestData)
        {
            Dev("ServerAPI", "Handling \n " + PostRequestData, logToHud: false);
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
            Dev("RH", "Handleing Update", logToHud: false);
          
          string text = WSResponce.Trim();
          if (string.IsNullOrEmpty(text))
          {
              return;
          }

            if (WSResponce == LastUpdate && !RuntimeConfig.isForced)
            {
                Dev("PlayerChanges", "Update Was Same Returning", logToHud: false);
                return;
            }
            LastUpdate = WSResponce;

            RuntimeConfig.isForced = false;

           
            JObject jObject = JObject.Parse(text);
            //   WSDone = false;


            try
            {
               
                JArray jArray3 = (JArray)jObject["TagList"];
                for (int k = 0; k < jArray3.Count; k++)
                {
                    string VRChatID = ((string?)jArray3[k]["anarchy_id"])?.Trim();
                    string CustomTag = ((string?)jArray3[k]["custom_rank"])?.Trim();
                    string CustomTagColor = ((string?)jArray3[k]["custom_tag_color"])?.Trim();
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
                    
                    if (!PlayerUtils.playerCustomTags.ContainsKey(VRChatID) && VRChatID != string.Empty)
                    { 
                        Dev("SRH", "Adding: " + VRChatID + " To Tag List");
                        PlayerUtils.playerCustomTags.Add(VRChatID, customtag);
                    }
                    else
                    {
                        PlayerUtils.playerCustomTags[VRChatID] = customtag;
                    }

                    PlayerInformation playerInformationByID = PlayerWrapper.GetPlayerInformationByID(VRChatID);
                    if (playerInformationByID != null && TagEnabled)
                    {
                        PlayerUtils.playerColorCache[playerInformationByID.displayName] = color;
                    }
                }

                JArray jArray10 = (JArray)jObject["ShaderWhiteList"];
                for (int k = 0; k < jArray10.Count; k++)
                {
                   if(! MainConfig.GetInstance().WhiteListedShaderList.Contains(jArray10[k].ToString()))
                   {
                        if (string.IsNullOrEmpty(jArray10[k].ToString())) continue;
                        MainConfig.GetInstance().WhiteListedShaderList.Add(jArray10[k].ToString());
                   }
                }

                JArray jArray11 = (JArray)jObject["AvatarWhiteList"];
                for (int k = 0; k < jArray11.Count; k++)
                {
                    if (!MainConfig.GetInstance().WhiteListedAvatarIDs.Contains(jArray11[k].ToString()))
                    {
                        if (string.IsNullOrEmpty(jArray10[k].ToString())) continue;
                        MainConfig.GetInstance().WhiteListedAvatarIDs.Add(jArray11[k].ToString());
                    }
                }

                Dev("SRH", "Finished Handling TagList ", logToHud: false);



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
