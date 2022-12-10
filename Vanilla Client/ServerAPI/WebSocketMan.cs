using VanillaClient.Modules;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading.Tasks;
using WebSocketSharp;

namespace VanillaClient.ServerAPI
{
    internal class WSBase : VanillaModule
    {

        public override void Start()
        {
            Runsocket();
        }

        public override void Stop()
        {

            Pop();
            ShutDown = true;
            wss.Close();
            Dev("WSAPI", "Popped WS Bubble & Disconnected");
        }



        public static void Runsocket()
        {
            using (wss = new WebSocket("wss://hvl.gg"))
            {
                wss.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
                Task.Delay(60000);
                Dev("ServerAPI", $"Connecting");

                wss.Connect();

                wss.OnClose += (sender, e) =>
                {
                    if (ShutDown)
                    { Dev("WSBase", "ShutDown Connection"); }



                    if (HasConn && HasConn)
                    {
                        Log("ServerAPI", $"Disconnected", ConsoleColor.Red);
                        Log("ServerAPI", $"Attempting Reconnect in 3 Seconds", ConsoleColor.Yellow);
                    }
                    Task.Delay(3000);
                    tryrecconect();
                };
                wss.OnOpen += (sender, e) =>
                {
                    Log("ServerAPI", $"Connected", ConsoleColor.Green);


                    var sendidtosv = new sendsinglemsg()
                    {
                        Custommsg = "VanillaClientWorldBossLogin",

                        code = "1",

                        Key = ServerHelper.GetKey(),

                        HWID = ServerHelper.GetHWID(),
                    };
                    sendmsg($"{JsonConvert.SerializeObject(sendidtosv)}");
                    Pop();
                    HasConn = true;
                };
                wss.OnMessage += Ws_OnMessage;
                wss.Log.Output = (_, __) => { };

            }
        }


        public static void Pop()
        {


            for (int i = 0; i < toSend.Count; i++)
            {
                if (!wss.IsAlive && HasConn)
                { wss.Connect(); continue; }

                if (!toSend.TryDequeue(out var result))
                { continue; }

                if (result.code == null)
                { Log("Server API", "Failed To Send Message", ConsoleColor.Red, "OnClientReconnect"); }

                var PopMessage = new sendsinglemsg()
                {
                    Custommsg = result.Message,

                    code = result.code,

                    Key = ServerHelper.GetKey(),

                    HWID = ServerHelper.GetHWID(),
                };




                sendmsg($"{JsonConvert.SerializeObject(PopMessage)}");
            }
        }

        internal protected static void sendmessage(string Message, string Code, bool pop = true)
        {
            // Dev("WSBase", $"Added Message {Message} with code {Code}");


            toSend.Enqueue(new WSSender
            {
                Message = Message,
                code = Code
            });



            if (pop)
                Pop();

        }

        internal protected static void sendmsg(string text)
        {

            if (wss.IsAlive && HasConn)
            { wss.Send(text); }
            else
            { tryrecconect(); }
        }

        public static void IsConnected()
        {

            if (!wss.IsAlive && HasConn)
                wss.Connect();
            Pop();
        }
        internal protected static void tryrecconect()
        {
            try
            {
                Task.Delay(50000);
                if (!wss.IsAlive)
                    wss.Connect();
            }
            catch (Exception error)
            {
                Console.WriteLine("Server Down Possibly", "Server");
                Console.WriteLine($"Cloud not connect : {error}", "Server");
                wss.Connect();
            }
        }

        internal protected static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            var message = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(e.Data));

            if (message.Contains("AvatarName") && message.Contains("Authorid") && message.Contains("Asseturl"))
            { }

            if (message.ToString() == "JUSTSAYHI")
            {
                ath = true;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome");
            }

            if (message.ToString() == "UserNotAuthInvalidHWID")
            { Console.WriteLine("Invalid HWID", "Auth"); }

            if (message.ToString() == "UserNotAuthUnknownWhy")
            { Console.WriteLine("Weird Issue with Auth", "Auth"); }

            if (message.Contains("UserNotAuth"))
            { Process.GetCurrentProcess().Kill(); }

            if (message.ToString() == "Server Error 1")
            { Console.WriteLine("Unknown Issue With Server HyperV is working on it dont wory", "Server"); }

            if (message.ToString() == "Server Error 2")
            {
                Console.WriteLine("Server Mantinace", "Server");
                Console.WriteLine("please wait for server to come back online Server required Features will not work", "Server");
            }
            if (message.ToString() == "Close Connection")
            {
                Console.WriteLine("Server Mantinace", "Server");
                Console.WriteLine("please wait for server to come back online Server required Features will not work", "Server");
                // System.Diagnostics.Process.GetCurrentProcess().Kill();
            }


            /*
            if (message.Contains("GlobalMSG"))
            {
                string result = message.Remove(0, 10);
                if (Settings.Config.GlobalMessage != result)
                {
                    Console.WriteLine($"[\u001b[36;1mGalaxyClient\u001b[0m] [Global Message]: {result}");
                    Settings.Config.ServerNotify = true;
                    Settings.Config.GlobalMessage = result;
                }
            }
            */

        }

        protected static bool HasConn = false;
        internal protected static readonly ConcurrentQueue<WSSender> toSend = new ConcurrentQueue<WSSender>();
        internal protected static WebSocket wss;
        internal protected static bool ath = false;
        internal protected static bool ShutDown = false;
        internal static string aviserchl = "";
    }
    internal class sendsinglemsg
    {
        public string Custommsg { get; set; }
        public string code { get; set; }
        public string HWID { get; set; }
        public string Key { get; set; }
    }


    internal struct WSSender
    {
        internal string Message;
        internal string code;

    }
}
