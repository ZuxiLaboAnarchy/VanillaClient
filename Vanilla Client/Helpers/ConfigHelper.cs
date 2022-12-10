using VanillaClient.Modules;
using VanillaClient.ServerAPI;
using UnityEngine;

namespace VanillaClient.Helpers
{
    internal class ConfigHelper : VanillaModule
    {

        private float nextPop = 0f;




        /*private static System.Timers.Timer WSHelperTimer;
           public override void Start()
           {
               new Thread(() => { ThreadStart(); }).Start();
           }


           public static void ThreadStart()
           {
               WSHelperTimer = new System.Timers.Timer(10000);
               WSHelperTimer.Elapsed += CheckConnection;
               WSHelperTimer.AutoReset = true;
               WSHelperTimer.Enabled = true;
               Dev("ServerAPI", "Started Heart Beat");
           }


           private static void CheckConnection(Object source, ElapsedEventArgs e)
           {
               WSBase.IsConnected();



       

           }*/

        public override void Start()
        {
            WSBase.Pop();
        }


        public override void Update()
        {
            if (Time.realtimeSinceStartup >= nextPop)
            {
              MainConfig.Save();
            }

        }

    }
}
