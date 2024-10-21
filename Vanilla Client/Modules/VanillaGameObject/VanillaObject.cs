using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerRuntimeLib;
using UnityEngine;
using Vanilla.Behaviours;
using VRC.SDK3.Components;

namespace Vanilla.Modules
{
    internal class VanillaObject : VanillaModule
    {
        internal static GameObject VanillaGObject { get; private set; }
        protected override string ModuleName => "VanillaObject";

        internal override void WaitForPlayer()
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
