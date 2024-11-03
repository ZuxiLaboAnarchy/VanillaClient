using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.API.ServerAPI;

namespace Vanilla.Config
{
    internal class UploadQueueConfig
    {
        public Queue<TempUploadContainer> UploadQueue = new();
    }
}