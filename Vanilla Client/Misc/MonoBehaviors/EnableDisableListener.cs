using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace Vanilla.Misc.MonoBehaviors
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class EnableDisableListener : MonoBehaviour
    {
        public EnableDisableListener(IntPtr obj) : base(obj) { }

        [method: HideFromIl2Cpp]
        public event Action OnDisabled;
        [method: HideFromIl2Cpp]
        public event Action OnEnabled;

        private void OnDisable() => OnDisabled?.Invoke();
        private void OnEnable() => OnEnabled?.Invoke();
    }
}
 