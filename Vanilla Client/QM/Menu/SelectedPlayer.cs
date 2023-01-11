using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Buttons.QM;
using Vanilla.Config;
using Vanilla.Exploits;
using Vanilla.ServerAPI;
using Vanilla.Wrappers;

namespace Vanilla.QM.Menu
{
    internal class SelectedPlayer
    {
        internal static void InitMenu(QMNestedButton Menu) 
        {
            var tp = new QMSingleButton(Menu, 1, 0, "TP User", delegate
            {
                PlayerWrapper.GetCurrentPlayer().transform.position = PlayerWrapper.GetSelectedUser().transform.position;
            }, "Tp to USer");

            var TargetUser = new QMSingleButton(Menu, 2, 0, "Target User", delegate
            {
                PlayerWrapper.GetSelectedUser();
            },"Target User");

            var AttachMenu = new QMNestedButton(Menu, 3, 0, "Siton", "Vanilla", "Vanilla Client");
            var Head = new QMSingleButton(AttachMenu, 1, 0, "Head", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 0; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Head");

            var Righthand = new QMSingleButton(AttachMenu, 2, 0, "Righthand", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 1; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Right Hand");

            var Lefthand = new QMSingleButton(AttachMenu, 3, 0, "Lefthand", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 2; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Left Hand");

            var rightshoulder = new QMSingleButton(AttachMenu, 4, 0, "rightshoulder", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 3; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Right Shoulder");

            var Leftshoulder = new QMSingleButton(AttachMenu, 1, 1, "rightshoulder", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 4; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Left Shoulder");

#if DEBUG
            var DevSelectedMenuButton = new QMNestedButton(Menu, 4, 4, "DevMenu", "Vanilla", "Vanilla Client");
            DevSelectedMenu.InitMenu(DevSelectedMenuButton);
#endif

        }
    }
}


