// /*
//  *
//  * VanillaClient - AmongUsHacks.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using UnityEngine;
using UnityEngine.UI;
using Vanilla.Menu.QM.API;
using VRC.Udon;

namespace Vanilla.Menu.QM.Menu
{
    internal class AmongUsHacks
    {
        internal static void InitMenu(QMNestedButton AmonUsMenu)
        {
            var Imposter = new QMSingleButton(AmonUsMenu, 1, 0, "Show Imposter", delegate { IWantSpiderman(); },
                "Logs to Console who imposter is");

            var repair = new QMSingleButton(AmonUsMenu, 2, 0, "Repair All", delegate { RepairAll(); },
                "Repair All sabotage");

            var killall = new QMSingleButton(AmonUsMenu, 3, 0, "Sabotage All", delegate { sabotageeverything(); },
                "sabotage all stuffs");

            var ejectall = new QMSingleButton(AmonUsMenu, 4, 0, "Air Lock All", delegate { ejecteveryone(); },
                "Ejects all player");

            var closevote = new QMSingleButton(AmonUsMenu, 1, 1, "Close out Vote", delegate { closevotey(); },
                "Closes the vote menu");
        }

        internal static void IWantSpiderman()
        {
            foreach (var gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (gameObject.name.Contains("Player Entry") &&
                    gameObject.GetComponentInChildren<Text>().text != "PlayerName" &&
                    gameObject.GetComponent<Image>().color.r > 0f)
                {
                    Log("Amongus",
                        gameObject.GetComponentInChildren<Text>().text +
                        " is the imposter (U want pictures of Spiderman!!!)");
                }
            }
        }


        internal static void RepairAll()
        {
            foreach (var udonBehaviour in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>())
            {
                udonBehaviour.SendCustomNetworkEvent(0, "SyncRepairComms");
                udonBehaviour.SendCustomNetworkEvent(0, "SyncRepairReactor");
                udonBehaviour.SendCustomNetworkEvent(0, "SyncRepairOxygen");
                udonBehaviour.SendCustomNetworkEvent(0, "SyncRepairLights");
            }
        }

        internal static void sabotageeverything()
        {
            foreach (var udonBehaviour in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>())
            {
                udonBehaviour.SendCustomNetworkEvent(0, "SyncDoSabotageComms");
                udonBehaviour.SendCustomNetworkEvent(0, "SyncDoSabotageReactor");
                udonBehaviour.SendCustomNetworkEvent(0, "SyncDoSabotageOxygen");
                udonBehaviour.SendCustomNetworkEvent(0, "SyncDoSabotageLights");
            }
        }

        internal static void ejecteveryone()
        {
            foreach (var udonBehaviour in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>())
            {
                udonBehaviour.SendCustomNetworkEvent(0, "SyncVotedOut");
            }
        }

        internal static void closevotey()
        {
            foreach (var udonBehaviour in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>())
            {
                udonBehaviour.SendCustomNetworkEvent(0, "SyncCloseVoting");
            }
        }
    }
}
