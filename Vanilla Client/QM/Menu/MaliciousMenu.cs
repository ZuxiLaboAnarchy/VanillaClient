using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Wrappers;
using VRC.SDKBase;

namespace Vanilla.QM.Menu
{
    internal class MaliciousMenu
    {
        internal static void InitMenu(QMNestedButton Menu)
        {
            var Itemlagg = new QMToggleButton(Menu, 1, 0, "Itemlag", delegate
            {
#pragma warning disable CS0219 // Variable is assigned but its value is never used
                bool lagger = true;
#pragma warning restore CS0219 // Variable is assigned but its value is never used
                MelonCoroutines.Start(ItemLaggers());
                MelonCoroutines.Start(ItemLagger2());
                RuntimeConfig.ItemLagger = true;

            }, delegate
            {

                MelonCoroutines.Stop(ItemLaggers());
                MelonCoroutines.Stop(ItemLagger2());
                RuntimeConfig.ItemLagger = false;

            }, "Item Lag");

            var HideSelfButton = new QMToggleButton(Menu, 2,0, "Hide Self", delegate
            {
                PlayerWrapper.HideSelf(true);
            }, delegate
            {
                PlayerWrapper.HideSelf(false);
            }, "Hide Self");


        }

        static IEnumerator ItemLaggers()
        {
            while (RuntimeConfig.ItemLagger)
            {
                foreach (VRC_Pickup item in UnityEngine.Object.FindObjectsOfType<VRC_Pickup>())
                {
                    Networking.LocalPlayer.TakeOwnership(item.gameObject);
                    item.transform.position = GeneralWrappers.LocalPlayer().transform.position + new Vector3(0f, 0.15f, 0f);
                }
                yield return new WaitForSeconds(0.3f);
            }
        }

       static  IEnumerator ItemLagger2()
        {
            while (RuntimeConfig.ItemLagger)
            {
                foreach (VRC_Pickup item in UnityEngine.Object.FindObjectsOfType<VRC_Pickup>())
                {
                    Networking.LocalPlayer.TakeOwnership(item.gameObject);
                    item.transform.position = GeneralWrappers.LocalPlayer().transform.position + new Vector3(0f, 9.998E+07f, 0f);
                }
                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
