using MelonLoader;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Exploits;
using Vanilla.QM.QMAPI;
using Vanilla.Wrappers;


namespace Vanilla.Modules
{
    internal class playermenu
    {
        internal static MenuPanelButton teleportButton;
        internal static void onplayermenu()
        {
            var teleportButton = new MenuPanelButton("Teleport", delegate
            {
                PlayerInformation playerInformationByName3 = PlayerWrapper.GetPlayerInformationByName(GeneralWrappers.GetPageUserInfo().field_Private_APIUser_0.displayName);
                if (playerInformationByName3 != null)
                {
                    PlayerWrapper.GetCurrentPlayer().transform.position = playerInformationByName3.vrcPlayer.transform.position;
                }
                else
                {
                    MelonLogger.Msg("Error", "User is not in instance");
                }
            }, interactable: true, "Buttons/RightSideButtons/RightUpperButtonColumn/PlaylistsButton", "Buttons/RightSideButtons/RightUpperButtonColumn");
        }
    }
}
