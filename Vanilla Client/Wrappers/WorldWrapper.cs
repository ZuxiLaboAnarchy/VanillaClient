using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;
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

        private static readonly List<string> PenComponentNameList = new List<string> { "pen", "marker", "grip" };

        private static IEnumerator FindComponentsOnSceneLoad()
        {
            _countedPickups = 0;
            _countedPens = 0;
           
            _penComponentList = new List<VRC_Pickup>();
            //_videoPlayerArray = Resources.FindObjectsOfTypeAll<VideoPlayer>().ToArray();
            _mirrorReflectionArray = Resources.FindObjectsOfTypeAll<VRC_MirrorReflection>().ToArray();
            _sdk3MirrorReflectionArray = Resources.FindObjectsOfTypeAll<VRCMirrorReflection>().ToArray();
            VRC_Pickup[] getAllPickups = GetAllPickups;
            foreach (VRC_Pickup pickup in getAllPickups)
            {
                foreach (string item in PenComponentNameList.Where((string name) => pickup.name.ToLower().Contains(name) && !pickup.transform.parent.name.ToLower().Contains("eraser")))
                {
                    _ = item;
                    _penComponentList.Add(pickup);
                }
                _countedPens++;
            }
            VRC_Pickup[] getAllPickups2 = GetAllPickups;
            foreach (VRC_Pickup pickup2 in getAllPickups2)
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
            VRC_Pickup[] getAllPickups = GetAllPickups;
            if (state)
            {
                VRC_Pickup[] array = getAllPickups;
                foreach (VRC_Pickup vRC_Pickup in array)
                {
                    if (vRC_Pickup != null)
                    {
                        vRC_Pickup.gameObject.SetActive(value: false);
                    }
                }
                
            }
            else
            {
                if (state)
                {
                    return;
                }
                VRC_Pickup[] array2 = getAllPickups;
                foreach (VRC_Pickup vRC_Pickup2 in array2)
                {
                    if (vRC_Pickup2 != null)
                    {
                        vRC_Pickup2.gameObject.SetActive(value: true);
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
                foreach (VRC_Pickup penComponent in _penComponentList)
                {
                    if (penComponent != null)
                    {
                        penComponent.gameObject.SetActive(value: false);
                    }
                }
                
            }
            foreach (VRC_Pickup penComponent2 in _penComponentList)
            {
                if (penComponent2 != null)
                {
                    penComponent2.gameObject.SetActive(value: true);
                }
            }
            
        }
    }
}
