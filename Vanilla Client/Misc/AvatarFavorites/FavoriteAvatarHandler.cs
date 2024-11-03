// /*
//  *
//  * VanillaClient - FavoriteAvatarHandler.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Vanilla.Wrappers;
using VRC.Core;

namespace Vanilla.Misc.AvatarFavorites
{
    internal class FavoriteAvatarHandler
    {
        internal static Dictionary<string, FavoriteAvatar> VanillaAvatarsFav = new();


        internal static void AddAvatarByID(string avatarId, Il2CppSystem.Collections.Generic.List<KeyCode> keycodeList,
            Text text)
        {
            var apiAvatar = new ApiAvatar();
            apiAvatar.id = avatarId;
            ((ApiModel)apiAvatar).Get((Il2CppSystem.Action<ApiContainer>)(Action<ApiContainer>)AvatarFoundHandlerSilent,
                (Il2CppSystem.Action<ApiContainer>)(Action<ApiContainer>)AvatarNotFoundHandlerSilent,
                (Il2CppSystem.Collections.Generic.Dictionary<string, BestHTTP.JSON.Json.Token>)null, false);
        }

        private static void AvatarNotFoundHandlerSilent(ApiContainer apiContainer)
        {
            Log("AvatarAPI", "Avatar not Found", ConsoleColor.Red);
        }

        private static void AvatarFoundHandlerSilent(ApiContainer apiContainer)
        {
            var apiAvatar = apiContainer.Model.Cast<ApiAvatar>();
            if (apiAvatar.authorId != PlayerWrapper.GetLocalAPIUser().id && apiAvatar.releaseStatus == "private")
            {
                Log("AvatarAPI", "Avatar Is Private", ConsoleColor.Red);
            }
            else if (VanillaAvatarsFav.ContainsKey(apiAvatar.id))
            {
                return;
            }
            else
            {
                AddRemoveAvatarToFavoriteList(apiAvatar, false);
            }
        }

        internal static bool AddRemoveAvatarToFavoriteList(ApiAvatar avi, bool notifyUser)
        {
            if (avi.authorId != PlayerWrapper.GetLocalAPIUser().id && avi.releaseStatus == "private")
            {
                if (notifyUser)
                {
                    //GeneralWrappers.AlertPopup(LanguageManager.GetUsedLanguage().NoticeText, LanguageManager.GetUsedLanguage().AvatarPrivate);
                }

                return false;
            }

            if (!VanillaAvatarsFav.ContainsKey(avi.id))
            {
                var source = VanillaAvatarsFav.ToList().OrderByDescending(
                    delegate (KeyValuePair<string, FavoriteAvatar> entry)
                    {
                        var keyValuePair = entry;
                        return keyValuePair.Value.AvatarSortIndex;
                    });
                var avatarSortIndex = source.Count() <= 0 ? 1 : source.ElementAt(0).Value.AvatarSortIndex + 1;
                var favoriteAvatar = new FavoriteAvatar(avi)
                {
                    AvatarSortIndex = avatarSortIndex
                };
                VanillaAvatarsFav.Add(avi.id, favoriteAvatar);
                /*TODO Add Ability to Favorite Avatars*/
                // ServerAPICore.GetInstance().UploadAvatarToDatabase(favoriteAvatar);
                if (notifyUser)
                {
                    //  GeneralWrappers.AlertPopup(LanguageManager.GetUsedLanguage().SuccessText, LanguageManager.GetUsedLanguage().AvatarFavorited.Replace("{name}", avi.name));
                }
            }
            else
            {
                /*TODO Add Ability to delete Avatars*/
                // ServerAPICore.GetInstance().DeleteAvatarFromDatabase(VanillaAvatarsFav[avi.id]);
                VanillaAvatarsFav.Remove(avi.id);
                if (notifyUser)
                {
                    // GeneralWrappers.AlertPopup(LanguageManager.GetUsedLanguage().SuccessText, LanguageManager.GetUsedLanguage().AvatarUnfavorited.Replace("{name}", avi.name));
                }
            }

            // RefreshList(string.Empty);
            return true;
        }
    }
}
