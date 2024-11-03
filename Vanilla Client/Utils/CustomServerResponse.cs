// /*
//  *
//  * VanillaClient - CustomServerResponse.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Collections.Generic;

namespace Vanilla.Utils
{
    internal class CustomServerResponse
    {
        public List<AnarchyUser> TagList;
        public List<string> AvatarWhiteList;
        public List<string> ShaderWhiteList;
    }

    internal class AnarchyUser
    {
        public string anarchy_id;
        public string custom_rank;
        public string custom_tag_color;
    }
}
