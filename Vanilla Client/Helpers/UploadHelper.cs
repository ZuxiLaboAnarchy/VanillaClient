using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.API.ServerAPI;
using Vanilla.AvatarFavorites;
using Vanilla.Config;
using Vanilla.TinyJSON;
using VRC.Core;
using static BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests.SkeinEngine;

namespace Vanilla.Helpers
{
    internal class UploadHelper
    {

        internal static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
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
            TempUploadContainer tempUploadContainer = default(TempUploadContainer);
            //tempUploadContainer.uploadType = 7;
            tempUploadContainer.saved_avatar = avatar;
            Dev("Added" , avatar.AvatarID);
            TempUploadContainer item = tempUploadContainer;
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
                IList<JsonProperty> source = base.CreateProperties(type, memberSerialization);
                return source.Where((JsonProperty p) => string.Compare(p.PropertyName, propertyNameToExclude, StringComparison.OrdinalIgnoreCase) != 0).ToList();
            }
        }
    }


