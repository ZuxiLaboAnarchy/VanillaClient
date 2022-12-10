using Vanilla.Modules;
using Vanilla.ServerAPI;
using UnityEngine;

namespace Vanilla.Helpers
{
    internal class WSHelper : VanillaModule
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
                nextPop = Time.realtimeSinceStartup + 15f;
                LogHandler.Dev("WSHelper", "Poping WS Bubble");
                WSBase.Pop();
               // WSBase.sendmessage("Test", "1", false);
            }

        }

    }
}
