using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Wrappers;
using VRC.DataModel;
using VRC.SDKBase;

namespace Vanilla.QM.Menu
{
    internal class Movement
    {
        internal static bool jetPackJump;
        internal static void InitMenu(QMNestedButton movementMenu)
        {

         

            var rocketJumpButton = new QMToggleButton(movementMenu, 1, 0, "Rocket Shoes", delegate
            {
                jetPackJump = true;
                Packhandler();

            }, delegate
            {
                jetPackJump = false;
            }, "Jump Fly");

            var Flipper = new QMToggleButton(movementMenu, 2, 0, "Flip Head", delegate
            {
                HeedflipHHandler(flip: false);
            }, delegate
            {
                HeedflipHHandler(flip: true);
            }, "Flips Head");

            var spin = new QMToggleButton(movementMenu, 3, 0, "Spin God", delegate
            {
                SpingodHandler(spinGod: false);
            }, delegate
            {
                SpingodHandler(spinGod: true);
            }, "Makes you a Spin God");



        }









        // To lazy to add a manager rn so here lmao

        internal static void Packhandler()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (Math.Abs(VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_0 - 1f) < 1f)
            {
                //curent V
                Vector3 velocity = localPlayer.GetVelocity();
                float sex = velocity.y;
                velocity.y = sex + 5;
                localPlayer.SetVelocity(velocity);
            }
        }


        private static NeckRange _nexkRange;
        internal static void HeedflipHHandler(bool flip)
        {
            VRC.Player player = PlayerWrapper.LocalPlayer();
            if (flip)
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.rotation = new Quaternion(0, 0f, 0f, 0f);
            }
            else
            {
                VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position += new Vector3(0f, 1.5f, 0f);
            }
        }

        private static Quaternion _revertRotation;
        private static Vector3 _revertVector;
        public static void SpingodHandler(bool spinGod)
        {
            if (spinGod)
            {
                PlayerWrapper.SendToLocation(_revertVector, _revertRotation);
            }
            else
            {
                _revertRotation = PlayerWrapper.GetPlayerRotation();
                _revertVector = PlayerWrapper.GetPlayerPosition();
                Transform transform = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform;
                transform.rotation = new Quaternion(90f, 0f, 0f, 0f);
                Transform transform2 = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform;
                transform.position = transform2.position + new Vector3(0f, 2f, 0f);
            }
        }


    }

}