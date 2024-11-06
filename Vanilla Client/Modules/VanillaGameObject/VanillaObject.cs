// /*
//  *
//  * VanillaClient - VanillaObject.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using UnityEngine;
using Vanilla.Modules.Manager;

namespace Vanilla.Modules.VanillaGameObject
{
    internal class VanillaObject : VanillaModule
    {
        internal static GameObject VanillaGObject { get; private set; }
        protected override string ModuleName => "VanillaObject";


        internal override void OnUiManagerInit()
        {
            //ClassInjector.RegisterTypeInIl2Cpp<VanillaBehavior>();
            // ClassInjector.RegisterTypeInIl2Cpp<OpenVRThumbstickInputDirect>();

            Dev("VanillaObject", "Spawning GameObject");

            VanillaGObject = new GameObject("VanillaObject");
            //Add Components
            VanillaGObject.AddComponent<VanillaBehavior>();
            //VanillaGObject.AddComponent<OpenVRThumbstickInputDirect>();
            VanillaGObject.AddComponent<MonoBehaviour>();
            //Old Keyboard Data LOL
            //   VanillaGObject.AddComponent<MonoBehaviour1PublicTe_p_dTeKe_kBo_mStBuUnique>();

            //  VanillaGObject.GetComponent<MonoBehaviour1PublicTe_p_dTeKe_kBo_mStBuUnique>()


            //  VanillaGObject.name = "";
            VanillaGObject.SetActive(true);
            OnScreenUI.AfterGameObjectInit(VanillaGObject);
        }
    }
}
