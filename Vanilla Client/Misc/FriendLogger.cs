// /*
//  *
//  * VanillaClient - FriendLogger.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.IO;
using System.Linq;
using Vanilla.Helpers;
using VRC.Core;

namespace Vanilla.Misc
{
    internal class FriendLogger
    {
        internal static void AutoLogFriendsToFile()
        {
            foreach (var text in APIUser.CurrentUser.friendIDs)
            {
                if (!File.ReadLines(FileHelper.GetCheatFolder() + "\\FriendList.CE").Contains(text))
                {
                    File.AppendAllText(FileHelper.GetCheatFolder() + "\\FriendList.CE", text + "\n");
                }
            }
        }
    }
}
