using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanilla.Buttons.QM;
using Vanilla.Exploits;
using Vanilla.Wrappers;

namespace Vanilla.Modules
{
    internal class OnplayerMenu : VanillaModule
    {
        internal static void targetmenu(QMTabMenu tabMenu)
        { 
            GameObject selectedUserMenu = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UserActions").gameObject;
            VRC.DataModel.UserSelectionManager selectedUser = GameObject.Find("/_Application").transform.Find("UIManager/SelectedUserManager").gameObject.GetComponent<VRC.DataModel.UserSelectionManager>();
            new QMNestedButton(tabMenu, 1, 4, "selectedUserMenu", "Vanilla", "Vanilla Client");
            new QMSingleButton(selectedUserMenu, 1, 0, "Target Player", delegate
            {
                PlayerWrapper.Target.Targetuser(GameObject.Find("/_Application").transform.Find("UIManager/SelectedUserManager").gameObject.GetComponent<VRC.DataModel.UserSelectionManager>().field_Private_APIUser_1.id);
            }, "Target User");
        }

    }
}
