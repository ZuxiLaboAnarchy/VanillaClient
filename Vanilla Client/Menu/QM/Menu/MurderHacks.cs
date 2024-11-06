// /*
//  *
//  * VanillaClient - MurderHacks.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vanilla.Menu.QM.API;
using Vanilla.Wrappers;
using VRC.SDK3.Components;
using VRC.SDKBase;
using VRC.Udon;

namespace Vanilla.Menu.QM.Menu
{
    internal class MurderHacks
    {
        internal static void InitMenu(QMNestedButton Muder4)
        {
            var getLuger = new QMSingleButton(Muder4, 1, 0, "Get Luger", delegate { Luger(); }, "Gets Luger Gun");

            var OtherFire = new QMSingleButton(Muder4, 2, 0, "Rapid Fire",
                delegate
                {
                    GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>()
                        .SendCustomEvent("Fire");
                }, "Continue Fire");


            var Blind = new QMSingleButton(Muder4, 3, 0, "Blind",
                delegate
                {
                    GameObject.Find("Game Logic").GetComponent<UdonBehaviour>()
                        .SendCustomNetworkEvent(0, "OnLocalPlayerFlashbanged");
                }, "Blinds users in game");


            var force = new QMSingleButton(Muder4, 4, 0, "Force Steal", delegate
            {
                var ItemArray1 = Resources.FindObjectsOfTypeAll<VRC_Pickup>().ToArray<VRC_Pickup>();
                foreach (var Items1 in ItemArray1)
                {
                    bool Check1 = Items1.gameObject;
                    if (Check1)
                    {
                        Items1.DisallowTheft = false;
                    }
                }

                var ItemArray2 = Resources.FindObjectsOfTypeAll<VRCPickup>().ToArray<VRCPickup>();
                foreach (var Items3 in ItemArray2)
                {
                    bool Check2 = Items3.gameObject;
                    if (Check2)
                    {
                        Items3.DisallowTheft = false;
                    }
                }
            }, "Steal Pickup");

            var unlockdoor = new QMSingleButton(Muder4, 1, 1, "Unlock Door", delegate { unlock(); }, "Unlock Door");

            var open = new QMSingleButton(Muder4, 2, 1, "Open Doors", delegate { opendoor(); }, "Open doors");

            var snake = new QMSingleButton(Muder4, 3, 1, "Snake",
                delegate
                {
                    GameObject.Find("Game Logic/Snakes/SnakeDispenser").GetComponent<UdonBehaviour>()
                        .SendCustomNetworkEvent(0, "DispenseSnake");
                }, "Relases the snake");

            var shotguns = new QMSingleButton(Muder4, 4, 1, "Get ShotGun", delegate { shotgun(); }, "Give me ShotGun");
        }

        internal static void shotgun()
        {
            VRC_Pickup[] array8 = Resources.FindObjectsOfTypeAll<VRC_Pickup>();
            for (var num2 = 0; num2 < array8.Length; num2++)
            {
                if (array8[num2].gameObject.name.ToLower().Contains("shotgun"))
                {
                    Networking.SetOwner(PlayerWrapper.GetCurrentPlayer().prop_VRCPlayerApi_0, array8[num2].gameObject);
                    array8[num2].transform.position =
                        PlayerWrapper.GetCurrentPlayer().gameObject.transform.position + Vector3.up;
                    array8[num2].pickupable = true;
                    array8[num2].DisallowTheft = false;
                    break;
                }
            }
        }

        internal static void Luger()
        {
            VRC_Pickup[] array7 = Resources.FindObjectsOfTypeAll<VRC_Pickup>();
            for (var num = 0; num < array7.Length; num++)
            {
                if (array7[num].gameObject.name.ToLower().Contains("luger"))
                {
                    Networking.SetOwner(PlayerWrapper.GetCurrentPlayer().prop_VRCPlayerApi_0, array7[num].gameObject);
                    array7[num].transform.position =
                        PlayerWrapper.GetCurrentPlayer().gameObject.transform.position + Vector3.up;
                    array7[num].pickupable = true;
                    array7[num].DisallowTheft = false;
                    break;
                }
            }
        }

        internal static void unlock()
        {
            var list = new List<Transform>
            {
                GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact shove")
            };
            foreach (var transform in list)
            {
                transform.GetComponent<UdonBehaviour>().Interact();
                transform.GetComponent<UdonBehaviour>().Interact();
                transform.GetComponent<UdonBehaviour>().Interact();
                transform.GetComponent<UdonBehaviour>().Interact();
            }
        }

        internal static void opendoor()
        {
            var list = new List<Transform>
            {
                GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact open")
            };
            foreach (var transform in list)
            {
                transform.GetComponent<UdonBehaviour>().Interact();
            }
        }
    }
}
