using Il2CppSystem.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Buttons.QM;
using Vanilla.Config;

namespace Vanilla.QM.Menu
{
    internal class DevMenu
    {
        internal static void InitMenu(QMNestedButton Menu) {

            var EventLoggerButton = new QMToggleButton(Menu, 2, 2, "LogEvent1", delegate
            {
                RuntimeConfig.EventLogger1 = true;
            }, delegate
            {
                RuntimeConfig.EventLogger1 = false;
            }, "Hide Self");

        }


    }
}
