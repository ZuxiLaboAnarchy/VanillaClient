using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using VRC.SDKBase;

namespace Vanilla.QM.Menu
{
    internal class movement
    {
        internal static bool jetPackJump;
        internal static void MovementMenu(QMTabMenu tabMenu)
        {

            var movementMenu = new QMNestedButton(tabMenu, 2, 3, "Movement Settings", "Vanilla", "Vanilla Client");
            var rocketJumpButton = new QMToggleButton(movementMenu, 1, 0, "Rocket Shoes", delegate
            {
               jetPackJump = true;
               Packhandler();

            }, delegate
            {
                jetPackJump = false;
            }, "Jump Fly");



        }









        // To lazy to add a manager rn so here lmao

        internal static void Packhandler()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (Math.Abs(VRCInputManager.Method_Public_Static_VRCInput_String_0("Jump").prop_Single_0 - 1f) < 1f)
            {
                Vector3 velocity = localPlayer.GetVelocity();
                velocity.y = localPlayer.GetJumpImpulse();
                localPlayer.SetVelocity(velocity);
            }
        }


    }

}