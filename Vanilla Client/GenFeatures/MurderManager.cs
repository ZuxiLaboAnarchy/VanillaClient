using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Vanilla.Modules
{
    internal class MurderManager
    {
        internal static bool murdergoldweapon = false;
        internal static bool murdergodmod = false;
        internal static bool everyonegoldgun = false;
        internal static bool amongusgodmod = false;
        internal static bool continuesfire = false;
        internal static bool everyonecontinuesfire = false;
    }
    class MurderMisc
    {
        public static bool Check()
        {
            string WORLDID = "";
            return RoomManager.Method_Public_Static_String_0().Contains(WORLDID);
        }
        public static void MurderMod(string udonevent)
        {
            bool CheckWorldID = Check();
            if (CheckWorldID)
            {
                foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    bool GameLogic = gameObject.name.Contains("Game Logic");
                    if (GameLogic)
                    {
                        gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, udonevent);
                    }
                }
            }
        }
        public static void MurderGive(string ObjectName)
        {
            bool CheckWorldID = Check();
            if (CheckWorldID)
            {
                foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    bool GameLogic = gameObject.name.Contains(ObjectName);
                    if (GameLogic)
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
                        gameObject.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
                    }
                }
            }
        }
    }
}
