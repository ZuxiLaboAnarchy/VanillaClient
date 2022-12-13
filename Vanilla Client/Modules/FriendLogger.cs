using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.Core;

namespace Vanilla.Modules
{
    internal class FriendLogger : VanillaModule
    {
        internal static IEnumerator AutoLogFriendsToFile()
        {
            if (!File.Exists("Vanilla/FriendList.txt"))
            {
                File.Create("Vanilla/FriendList.txt");

                yield break;
            }
            yield return new WaitForSeconds(1f);
            foreach (string text in APIUser.CurrentUser.friendIDs)
            {
                if (!File.ReadLines("Vanilla/FriendList.txt").Contains(text))
                {
                    File.AppendAllText("Vanilla/FriendList.txt", text + "\n");
                }
            }
            yield break;
        }
    }
}
