// /*
//  *
//  * VanillaClient - UploadHelper.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vanilla.APIs.ServerAPI;
using Vanilla.Config;
using Vanilla.JSON;
using Vanilla.Misc.AvatarFavorites;

namespace Vanilla.Helpers
{
    internal class UploadHelper
    {
        internal static readonly JsonSerializerSettings jsonSerializerSettings = new()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            ContractResolver = new CustomContractResolver("normalized")
        };


        private static UploadQueueConfig uploadQueueConfig;

        private static bool uploadQueueConfigSaving = false;


        private static readonly string ConfigLocation = FileHelper.GetCheatFolder() + "UploadQueue.json";

        internal static UploadQueueConfig GetUploadQueueConfig()
        {
            if (uploadQueueConfig == null)
            {
                CreateUploadQueueConfig();
            }

            return uploadQueueConfig;
        }

        internal static void CreateUploadQueueConfig()
        {
            uploadQueueConfig = new UploadQueueConfig();
            SaveUploadQueueConfig();
        }

        internal static void SaveUploadQueueConfig(bool forceSave = false)
        {
            if (uploadQueueConfigSaving && !forceSave)
            {
                return;
            }

            uploadQueueConfigSaving = true;
            try
            {
                File.WriteAllText(ConfigLocation, Json.Encode(uploadQueueConfig, true));
            }
            catch (Exception e2)
            {
                ExceptionHandler("UploadAPI", e2);
            }

            uploadQueueConfigSaving = false;
        }

        internal static void UploadAvatarToGlobalDatabase(FavoriteAvatar avatar)
        {
            var tempUploadContainer = default(TempUploadContainer);
            //tempUploadContainer.uploadType = 7;
            tempUploadContainer.saved_avatar = avatar;
            Dev("AVILOG", "Added: " + avatar.AvatarID);
            var item = tempUploadContainer;
            GetUploadQueueConfig().UploadQueue.Enqueue(item);
            SaveUploadQueueConfig();
        }
    }

    internal class CustomContractResolver : DefaultContractResolver
    {
        private readonly string propertyNameToExclude;

        internal CustomContractResolver(string propertyNameToExclude)
        {
            this.propertyNameToExclude = propertyNameToExclude;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var source = base.CreateProperties(type, memberSerialization);
            return source.Where((JsonProperty p) =>
                    string.Compare(p.PropertyName, propertyNameToExclude, StringComparison.OrdinalIgnoreCase) != 0)
                .ToList();
        }
    }
}
