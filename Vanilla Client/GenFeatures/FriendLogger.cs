using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;
using Vanilla.Utils;
namespace Vanilla.Modules
{
    internal class FriendLogger
    {




        internal static void AutoLogFriendsToFile()
        {
            if (!File.Exists(FileHelper.GetCheatFolder() +  "\\FriendList.CE"))
            {
                File.Create(FileHelper.GetCheatFolder() + "\\FriendList.CE");

                
            }
           
            foreach (string text in APIUser.CurrentUser.friendIDs)
            {
                if (!File.ReadLines(FileHelper.GetCheatFolder() + "\\FriendList.CE").Contains(text))
                {
                    File.AppendAllText(FileHelper.GetCheatFolder() + "\\FriendList.CE", text + "\n");
                }
            }
          
        }
    }
}
