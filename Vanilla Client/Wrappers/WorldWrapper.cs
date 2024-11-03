// /*
//  *
//  * VanillaClient - WorldWrapper.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Components;
using VRC.SDKBase;

namespace Vanilla.Wrappers
{
    internal class WorldWrapper
    {
        private static int _countedPickups;

        private static int _countedPens;


        private static VRC_Pickup[] GetAllPickups { get; set; }

        private static VRC_MirrorReflection[] _mirrorReflectionArray;

        private static VRCMirrorReflection[] _sdk3MirrorReflectionArray;

        private static List<VRC_Pickup> _penComponentList;

        private static readonly List<string> PenComponentNameList = new() { "pen", "marker", "grip" };

        internal static void GoToRoom(string ID)
        {
            Networking.GoToRoom(ID);
        }


        private static IEnumerator FindComponentsOnSceneLoad()
        {
            _countedPickups = 0;
            _countedPens = 0;

            _penComponentList = new List<VRC_Pickup>();
            //_videoPlayerArray = Resources.FindObjectsOfTypeAll<VideoPlayer>().ToArray();
            _mirrorReflectionArray = Resources.FindObjectsOfTypeAll<VRC_MirrorReflection>().ToArray();
            _sdk3MirrorReflectionArray = Resources.FindObjectsOfTypeAll<VRCMirrorReflection>().ToArray();
            var getAllPickups = GetAllPickups;
            foreach (var pickup in getAllPickups)
            {
                foreach (var item in PenComponentNameList.Where((string name) =>
                             pickup.name.ToLower().Contains(name) &&
                             !pickup.transform.parent.name.ToLower().Contains("eraser")))
                {
                    _ = item;
                    _penComponentList.Add(pickup);
                }

                _countedPens++;
            }

            var getAllPickups2 = GetAllPickups;
            foreach (var pickup2 in getAllPickups2)
            {
                if (pickup2 != null)
                {
                    _countedPickups++;
                }
            }

            yield break;
        }

        public static void EnableDisablePickups(bool state)
        {
            if (!GeneralWrappers.IsInWorld())
            {
                return;
            }

            var getAllPickups = GetAllPickups;
            if (state)
            {
                var array = getAllPickups;
                foreach (var vRC_Pickup in array)
                {
                    if (vRC_Pickup != null)
                    {
                        vRC_Pickup.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                if (state)
                {
                    return;
                }

                var array2 = getAllPickups;
                foreach (var vRC_Pickup2 in array2)
                {
                    if (vRC_Pickup2 != null)
                    {
                        vRC_Pickup2.gameObject.SetActive(true);
                    }
                }
            }
        }

        public static void EnableDisablePens(bool state)
        {
            if (!GeneralWrappers.IsInWorld())
            {
                return;
            }

            if (state)
            {
                foreach (var penComponent in _penComponentList)
                {
                    if (penComponent != null)
                    {
                        penComponent.gameObject.SetActive(false);
                    }
                }
            }

            foreach (var penComponent2 in _penComponentList)
            {
                if (penComponent2 != null)
                {
                    penComponent2.gameObject.SetActive(true);
                }
            }
        }
    }
}
