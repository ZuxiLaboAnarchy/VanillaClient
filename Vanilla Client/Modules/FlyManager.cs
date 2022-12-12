using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;
using static Vanilla.Main;

namespace Vanilla.Modules
{
    internal class FlyManager : VanillaModule
    {
        public override void Update()
        {
            private Transform camera() => GameObject.Find("Camera (eye)").transform;
            
            if (!Nig.flytoggle) return;

            if (VRC.Player.prop_Player_0 == null) return;

            float flyspeed = Input.GetKey(KeyCode.LeftShift) ? Time.deltaTime * 50 : Time.deltaTime * 25;
            if (VRC.Player.prop_Player_0.field_Private_VRCPlayerApi_0.IsUserInVR())
            {
                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < 0f)
                    VRC.Player.prop_Player_0.transform.position += camera().up * flyspeed;
                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0f)
                    VRC.Player.prop_Player_0.transform.position -= camera().up * flyspeed;

                if (Input.GetAxis("Vertical") != 0f)
                    VRC.Player.prop_Player_0.transform.position += camera().forward * (flyspeed * Input.GetAxis("Vertical"));

                if (Input.GetAxis("Horizontal") != 0f)
                    VRC.Player.prop_Player_0.transform.position += camera().transform.right * (flyspeed * Input.GetAxis("Horizontal"));
            }
            else
            {
                if (Input.GetKey(KeyCode.W))
                    VRC.Player.prop_Player_0.transform.position += camera().forward * flyspeed;

                if (Input.GetKey(KeyCode.S))
                    VRC.Player.prop_Player_0.transform.position -= camera().forward * flyspeed;

                if (Input.GetKey(KeyCode.A))
                    VRC.Player.prop_Player_0.transform.position -= camera().right * (flyspeed / 2);

                if (Input.GetKey(KeyCode.D))
                    VRC.Player.prop_Player_0.transform.position += camera().right * (flyspeed / 2);

                if (Input.GetKey(KeyCode.LeftControl))
                    VRC.Player.prop_Player_0.transform.position -= camera().up * (flyspeed / 2);

                if (Input.GetKey(KeyCode.Space))
                    VRC.Player.prop_Player_0.transform.position += camera().up * (flyspeed / 2);
            }
        }
    }
}
