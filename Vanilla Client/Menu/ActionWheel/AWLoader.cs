using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Modules;
using static Blaze.API.AW.ActionWheelAPI;

namespace Vanilla.Menu.ActionWheel
{
    internal class AWLoader : VanillaModule
    {
        static ActionMenuPage mainAW = null;
        internal override void LateStart()
        {
        }


        internal override void OnQuickMenuLoaded()
        {
            mainAW = new ActionMenuPage(ActionMenuBaseMenu.MainMenu, "Vanilla Client", AssetLoader.LoadTexture("VanillaClientLogo"));
        }
    }
}
