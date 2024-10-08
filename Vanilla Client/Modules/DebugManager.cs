using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Wrappers;

namespace Vanilla.Modules
{
    internal class DebugManager : VanillaModule
    {
        internal override void Debug()
        {
            GeneralWrappers.CopyInstanceToClipboard();
        }
    }
}
