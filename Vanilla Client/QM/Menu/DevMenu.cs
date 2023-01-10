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

            var Event1LoggerButton = new QMToggleButton(Menu, 1, 0, "LogEvent1", delegate
            {
                RuntimeConfig.EventLogger1 = true;
            }, delegate
            {
                RuntimeConfig.EventLogger1 = false;
            }, "E1 Log");

            var Event6LoggerButton = new QMToggleButton(Menu, 1, 0, "Log \nEvent \n6", delegate
            {
                RuntimeConfig.EventLogger6 = true;
            }, delegate
            {
                RuntimeConfig.EventLogger6 = false;
            }, "Hide Self");

        }


    }
}
