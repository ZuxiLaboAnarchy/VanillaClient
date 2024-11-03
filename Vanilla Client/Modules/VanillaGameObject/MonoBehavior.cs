using UnityEngine;
using Vanilla.Modules;

namespace Vanilla.Behaviours
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class VanillaBehavior : MonoBehaviour
    {
        public VanillaBehavior(IntPtr ptr) : base(ptr)
        {
        }

        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            ModuleManager.OnApplicationFocus(hasFocus);
        }
    }
}