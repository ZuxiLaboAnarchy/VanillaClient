﻿// /*
//  *
//  * VanillaClient - UploadQueConfig.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Collections.Generic;
using Vanilla.APIs.ServerAPI;

namespace Vanilla.Config
{
    internal class UploadQueueConfig
    {
        public Queue<TempUploadContainer> UploadQueue = new();
    }
}
