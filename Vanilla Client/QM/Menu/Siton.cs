using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Buttons.QM;
using Vanilla.QM.Menu;
using Vanilla.Exploits;
using Vanilla.Modules;

namespace Vanilla.QM.Menu
{/*
    internal class Siton : VanillaModule
    {
        internal static void InitMenu(QMTabMenu tabMenu)
        {
            var AttachMenu = new QMNestedButton(tabMenu, 3, 1, "Siton", "Vanilla", "Vanilla Client");
            var Head = new QMSingleButton(AttachMenu, 1, 1, "Head", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 0; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Head");
            
            var Righthand = new QMSingleButton(AttachMenu, 1, 1, "Righthand", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 1; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Right Hand");
            
            var Lefthand = new QMSingleButton(AttachMenu, 1, 1, "Lefthand", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 2; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Left Hand");
            
            var rightshoulder = new QMSingleButton(AttachMenu, 1, 1, "rightshoulder", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 3; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Right Shoulder");

            var Leftshoulder = new QMSingleButton(AttachMenu, 1, 1, "rightshoulder", delegate
            {
                PlayerAttach._IsSiting = !PlayerAttach._IsSiting;
                PlayerAttach._Part = 4; MelonLoader.MelonCoroutines.Start(PlayerAttach._Sitonparts());
            }, "Sit On Left Shoulder");

        }
    }*/
}
