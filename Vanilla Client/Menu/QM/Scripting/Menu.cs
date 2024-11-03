using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Buttons.QM;

namespace Vanilla.Menu.QM.Scripting
{
    internal class ScriptingMenu
    {
        internal static ButtonHelper _buttonHelper = new();
        internal static List<QMSingleButton> qMSingleButtons = new();

        internal static void CreateScriptButtons(QMTabMenu tab)
        {
            var ScriptingMenu = new QMNestedButton(tab, 2, 3, "Scripting", "Scripting", "Script Menu");
            for (var i = 0; i < 10; i++)
            {
                qMSingleButtons.Add(new QMSingleButton(ScriptingMenu, _buttonHelper.NextXValue,
                    _buttonHelper.NextYValue, i.ToString(), null, i.ToString()));
            }
        }
    }
}