using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
