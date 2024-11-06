// /*
//  *
//  * VanillaClient - OnScreenUI.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Vanilla.Modules.Manager;

namespace Vanilla.Modules
{
    internal class OnScreenUI : VanillaModule
    {
        protected override string ModuleName => "OnScreenUI";

        private static GameObject _uiLayerObject = null;
        private static TextMeshProUGUI _uiLogger = null;
        private static TextMeshProUGUI _uiStatus = null;
        private static readonly List<string> stringList = new();
        private const int MaxCapacity = 30; // 23 if using game object


        internal static void AfterGameObjectInit(GameObject gobject)
        {
            #region Onscreen Logger Initlizer

            // Clear String List to make sure it can successfully init
            AddString("");


            // Create and Instantiate our object that contains the onscreen UI & Parent it to our base object
            _uiLayerObject =
                UnityEngine.Object.Instantiate(AssetLoader.LoadGameObject("ZuxiCanvas"), gobject.transform, true);


            // Set it active bc unity
            _uiLayerObject.SetActive(true);
            // Set its name bc its yes while not important the user won't see it it's just a nice touch also so other devs know it's ours and can latch into it
            // @note back when this was private this required a proxy DLL that allowed for attaching however the method is public now
            // so feel free to check if loaded or load this and use it
            _uiLayerObject.name = "ZuxiOnScreenLogger";

            // Find our objects and get there text mesh pro components so we don't have to get it when updating it since IT SHOULD NEVER CHANGE
            _uiLogger = _uiLayerObject.transform.Find("ZuxiLoggerUI").gameObject.GetComponent<TextMeshProUGUI>();
            _uiStatus = _uiLayerObject.transform.Find("ZuxiStatsUI").gameObject.GetComponent<TextMeshProUGUI>();
            _uiLayerObject.transform.Find("ZuxiStatsUI").gameObject.SetActive(false);
            Dev("UILayer", "OnScreen Logger Created!");
            AddString("HelloWorld!");

            #endregion
        }

        internal override void Debug()
        {
            Console.WriteLine("[UI DEBUG]: \n" + BuildUIList() + "[END.]");
        }

        internal override void OnGUI()
        {
            #region Status

            // Hack job to make status always Shown and not have to use assetbundle alot more fps intensive thogh

            // USER INFO
            /*  UnityEngine.GUI.Label(new Rect(3f, 0f, 160f, 90f),
                  $"<color=cyan>UID: </color><color=cyan>0\n</color>");
              UnityEngine.GUI.Label(new Rect(3f, 12f, 160f, 90f),
                  $"<color=cyan>User: </color><color=cyan>ANARCHY\n</color>");
              */
            // General Stats
            GUI.Label(new Rect(3f, 1000f, 160f, 90f),
                $"<color=cyan>Day: </color><color=cyan>{DateTime.Now.DayOfWeek}\n</color>");
            GUI.Label(new Rect(3f, 1012f, 160f, 90f),
                $"<color=cyan>Date: </color><color=cyan>{DateTime.Now.ToString("MM/dd/yyyy")}\n</color>");
            GUI.Label(new Rect(3f, 1024f, 160f, 90f),
                $"<color=cyan>Time: </color><color=cyan>{DateTime.Now.ToString("h:m:s tt")}\n</color>");
            GUI.Label(new Rect(3f, 1036f, 160f, 90f),
                $"<color=cyan>FPS: </color><color=cyan>{(int)(1.0f / Time.smoothDeltaTime)}\n</color>");

            #endregion
        }

        #region OnScreenLogger Info Container Utils

        private static void OnGUIUpdateRequested()
        {
            if (_uiLogger != null)
            {
                _uiLogger.SetText(BuildUIList());
            }
        }

        internal static void AddString(string newString)
        {
            if (string.IsNullOrEmpty(newString))
            {
                OnGUIUpdateRequested();
                return;
            }

            stringList.Add(newString + "<color=#00ffff> | " + FormatTimeWithTimeZone(DateTime.Now) + " </color>");

            // Check if the list size exceeds the maximum capacity
            if (stringList.Count > MaxCapacity)
            // Remove the oldest string (the one at index 0)
            {
                stringList.RemoveAt(0);
            }

            OnGUIUpdateRequested();
        }


        internal static List<string> GetStringList()
        {
            return stringList;
        }

        private static string BuildUIList()
        {
            return string.Join("\n", stringList);
        }

        private static string FormatTimeWithTimeZone(DateTime dateTime)
        {
            // Get the user's local time zone
            var userTimeZone = TimeZoneInfo.Local;

            // Convert the provided DateTime to the user's time zone
            var localTime = TimeZoneInfo.ConvertTime(dateTime, userTimeZone);

            // Format the time in the desired format (e.g., "04:11:49.9 AM/PM")
            var formattedTime = localTime.ToString("hh:mm:ss.f tt");

            return formattedTime;
        }

        #endregion
    }
}
