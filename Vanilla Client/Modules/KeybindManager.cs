using System.Diagnostics;
using System.IO;
using UnityEngine;
using Vanilla.Config;

namespace Vanilla.Modules
{
    internal class KeybindManager : VanillaModule
    {
      
        public override void Update()
        {

            if (UnityEngine.Input.GetKeyDown(KeyCode.L))
            {
               // Buttons.Loader.LoadButtons();

                // UniversalUI.SetUIActive("VanillaClient", IsGUIActive());
            }


            if (UnityEngine.Input.GetKeyDown(KeyCode.L))
            {
                LogHandler.RePop();
                MainConfig.Load();
            }


            

            if (UnityEngine.Input.GetKeyDown(KeyCode.RightControl))
            {
                GeneralUtils.CloseGame();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.RightAlt))
            {
                GeneralUtils.Restart();
            }
        }
    }
}
