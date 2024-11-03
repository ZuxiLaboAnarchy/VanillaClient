using UnityEngine;
using Vanilla.Config;

namespace Vanilla.Modules
{
    internal class FlyManager : VanillaModule
    {
        protected override string ModuleName => "FlyManager";

        internal override void WorldUnload(int level)
        {
            RuntimeConfig.ShouldFly = false;
        }

        internal static void ToggleFly()
        {
            if (!RuntimeConfig.ShouldFly)
            {
                Log("Fly", "Fly Enabled");
                RuntimeConfig.ShouldFly = true;
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = false;
            }
            else
            {
                Log("Fly", "Fly Disabled");
                RuntimeConfig.ShouldFly = false;
                VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
            }
        }

        private Transform camera()
        {
            return GameObject.Find("Camera (eye)").transform;
        }

        internal override void Update()
        {
            if (!RuntimeConfig.ShouldFly)
            {
                return;
            }

            if (VRC.Player.prop_Player_0 == null)
            {
                return;
            }

            var flyspeed = Input.GetKey(KeyCode.LeftShift) ? Time.deltaTime * 50 : Time.deltaTime * 25;
            if (VRC.Player.prop_Player_0.field_Private_VRCPlayerApi_0.IsUserInVR())
            {
                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0f)
                {
                    VRC.Player.prop_Player_0.transform.position += camera().up * flyspeed;
                }

                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0f)
                {
                    VRC.Player.prop_Player_0.transform.position -= camera().up * flyspeed;
                }

                if (Input.GetAxis("Vertical") != 0f)
                {
                    VRC.Player.prop_Player_0.transform.position +=
                        camera().forward * (flyspeed * Input.GetAxis("Vertical"));
                }

                if (Input.GetAxis("Horizontal") != 0f)
                {
                    VRC.Player.prop_Player_0.transform.position +=
                        camera().transform.right * (flyspeed * Input.GetAxis("Horizontal"));
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    VRC.Player.prop_Player_0.transform.position += camera().forward * flyspeed;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    VRC.Player.prop_Player_0.transform.position -= camera().forward * flyspeed;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    VRC.Player.prop_Player_0.transform.position -= camera().right * (flyspeed / 2);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    VRC.Player.prop_Player_0.transform.position += camera().right * (flyspeed / 2);
                }

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    VRC.Player.prop_Player_0.transform.position -= camera().up * (flyspeed / 2);
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    VRC.Player.prop_Player_0.transform.position += camera().up * (flyspeed / 2);
                }
            }
        }
    }
}