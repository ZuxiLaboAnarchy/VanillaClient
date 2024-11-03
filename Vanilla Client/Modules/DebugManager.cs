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
        protected override string ModuleName => "Debug Manager";

        internal override void Debug()
        {
            GeneralWrappers.CopyInstanceToClipboard();
        }
    }
}