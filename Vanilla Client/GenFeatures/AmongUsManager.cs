using System.Linq;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Vanilla.Modules
{
    internal class AmongUsManager
    {
        public static bool Check()
        {
            string WORLDID = "";
            return RoomManager.Method_Public_Static_String_0().Contains(WORLDID);
        }
        public static void AmongUsMod(string udonevent)
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
        public static void AmongUsGive(string ObjectName)
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


