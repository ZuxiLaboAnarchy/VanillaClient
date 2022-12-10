using Vanilla.Modules;
using Vanilla.ServerAPI;
using UnityEngine;
using Vanilla.Config;

namespace Vanilla.Helpers
{
    internal class ConfigHelper : VanillaModule
    {

        private float nextPop = 0f;

        public override void Start()
        {
            MainConfig.Load();
        }


        public override void Update()
        {
            if (Time.realtimeSinceStartup >= nextPop)
            {
                nextPop = Time.realtimeSinceStartup + 15f;
                MainConfig.Save();
            }

        }

    }
}
