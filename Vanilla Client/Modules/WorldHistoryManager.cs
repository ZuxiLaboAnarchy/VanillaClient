// /*
//  *
//  * VanillaClient - WorldHistoryManager.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

namespace Vanilla.Modules
{
    // Your Gonna need to Change the Button Api on this to the one we use
    /* internal class Worldhistory : VanillaModule
     {
         internal static GameObject worldhistorymenu { get; set; }
         internal static List<jsonmanager.worldhistory> _WorldHistory { get; set; }
         internal static UnityEngine.UI.Button[] _Buttons { get; set; }

         internal static void updatehistory(string worldname, string wolrdid)
         {

             _WorldHistory = Newtonsoft.Json.JsonConvert.DeserializeObject<List<jsonmanager.worldhistory>>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Vanilla\\Config\\WorldHistory.json"));
             if (_WorldHistory.Any())
                 _WorldHistory.RemoveAt(_WorldHistory.Count - 1);

             var newworldh = new jsonmanager.worldhistory()
             {
                 worldid = wolrdid,

                 worldname = worldname,
             };

             _WorldHistory.Insert(0, newworldh);

             var serialized = Newtonsoft.Json.JsonConvert.SerializeObject(_WorldHistory);
             File.WriteAllText((Directory.GetCurrentDirectory() + "\\Defiance\\Config\\WorldHistory.json"), serialized);

             _Buttons = worldhistorymenu.GetMenu().GetComponentsInChildren<UnityEngine.UI.Button>(true).Where(gmj => gmj.gameObject != worldhistorymenu.GetMenu().gameObject).ToArray();
             for (int i = 0; i < _Buttons.Length; i++)
             {
                 try
                 {
                     GameObject.Destroy(_Buttons[i].gameObject);
                 }
                 catch { }
             }
             _WorldHistory = Newtonsoft.Json.JsonConvert.DeserializeObject<List<jsonmanager.worldhistory>>(File.ReadAllText(Directory.GetCurrentDirectory() + "\\Vanilla\\Config\\WorldHistory.json"));
             foreach (var world in _WorldHistory)
             {
                 new NButton(worldhistorymenu.GetMenu(), world.worldname, () =>
                 {
                     try
                     {
                         if (!Networking.GoToRoom(world.worldid))
                             new PortalInternal().Method_Private_Void_String_String_PDM_0(world.worldid.Split(':')[0], world.worldid.Split(':')[1]);
                     }
                     catch (Exception e) { MelonLogger.Msg(e); }

                 }, true);
             }
         }
     }*/
}
