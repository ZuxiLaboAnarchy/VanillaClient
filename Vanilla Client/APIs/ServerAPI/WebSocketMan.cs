using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Vanilla.Config;
using Vanilla.Modules;
using Vanilla.JSON;
using Vanilla.Wrappers;
using WebSocketSharp;

namespace Vanilla.ServerAPI
{
    internal class WSBase : VanillaModule
    {
        
        protected override string ModuleName => "WSBase";
        internal override void WaitForAPIUser()
        {
            new Thread(() => { SetupSocket(); }).Start();

        }

        internal override void Stop()
        {

            Pop();
            ShutDown = true;
            wss.Close();


            Dev("WSAPI", "Popped WS Bubble & Disconnected");
        }

        internal static void KeepAlivePack()
        {
            if (wss is null)
                return;
          
            if (!wss.IsAlive && HasConn)
                return;


            var FetchModelRaw = new sendsinglemsg()
            {
              //  uid = PlayerWrapper.GetLocalAPIUser().id,

                code = "4",

               // Key = ServerHelper.GetKey(),
            };
            WSBase.sendmsg($"{Json.Encode(FetchModelRaw)}");
             
        }


        internal static void SetupSocket()
        {
            RuntimeConfig.WSAuthed = true;
            return;
            using (wss = new WebSocket("wss://hvl.gg/api/cheats/vrchat?token=" + ServerHelper.GetJWT()))
            {
                Dev("ServerAPI", "Connecting to: " + wss.Url);
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
                        //   Log("ServerAPI", $"Disconnected", ConsoleColor.Red);
                        // Log("ServerAPI", $"Attempting Reconnect in 3 Seconds", ConsoleColor.Yellow);
                    }
                    Task.Delay(3000);
                    tryrecconect();
                };
                wss.OnOpen += (sender, e) =>
                {
                    if (!HasConn)
                        Log("ServerAPI", $"Connected", ConsoleColor.Green);
                    HasConn = true;

                    var sendidtosv = new sendsinglemsg()
                    {
                        uid = null,//APIUser.CurrentUser.id,

                        code = "3",

                        Key = ServerHelper.GetKey(),


                    };
                    sendmsg(Json.Encode(sendidtosv));
                    Pop();

                };
                wss.OnMessage += Ws_OnMessage;
                wss.Log.Output = (_, __) => { };

            }
        }

        internal static void ConnectNewJWT()
        {
            if (!wss.IsAlive)
               // wss = new WebSocket("wss://hvl.gg/api/cheats/vrchat?token=" + ServerHelper.GetJWT());
            wss = new WebSocket("wss://ws.imzuxi.com/api/cheats/vrchat?token=" + ServerHelper.GetJWT());
        }





        internal static void Pop()
        {
            if (!RuntimeConfig.WSAuthed)
            { return; }

            for (int i = 0; i < toSend.Count; i++)
            {
                if (!wss.IsAlive && HasConn)
                { wss.Connect(); continue; }

                if (!toSend.TryDequeue(out var result))
                { continue; }

                if (result.code == null)
                { Log("Server API", "Failed To Send Message Message Identifyer wass Null", ConsoleColor.Red, "OnPop"); }

                var PopMessage = new sendsinglemsg()
                {
                    Custommsg = result.Message,

                    code = result.code,

                    Key = ServerHelper.GetKey(),

                    HWID = ServerHelper.GetHWID(),
                };
                sendmsg($"{Json.Encode(PopMessage)}");
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


            if (wss.IsAlive)
            { wss.Send(text); }
            else
            { tryrecconect(); }
        }

        internal static void IsConnected()
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
                Log("Server", "Server Down Possibly", ConsoleColor.Red);
                ExceptionHandler("WSMAN", error);
                wss.Connect();
            }
        }

        internal protected static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            //  Dev("ServerAPI", "Raw Data: " + e.Data.ToString());

            if (e.Data.ToString().ToLower().Contains("authed"))
            { RuntimeConfig.WSAuthed = true; LogHandler.Log("ServerAPI", "Authenticated with RealtimeNetwork", ConsoleColor.Green); }

            if (e.Data.ToString().ToLower().Contains("update packet"))
            {

              //  ServerResponceHandler.HandleWSUpdate(e.Data.ToString());
                // new Thread(() => {  }).Start();


                // LogHandler.Log("ServerAPI", "Fetched Latest Update");
            }

            if (e.Data.ToString().ToLower().Contains("invalid token"))
            {
                RuntimeConfig.WSAuthed = false;
                LogHandler.Log("ServerAPI", "Disconnected From Realtime Network Reauthing", ConsoleColor.Red);
                if (Server.SendPostRequestInternal("login") != null)
                {
                    new Thread(() => { SetupSocket(); }).Start();
                }
            }

            if (e.Data.ToString().ToLower().Contains("tokenexpirederror"))
            {
                RuntimeConfig.WSAuthed = false;
                LogHandler.Log("ServerAPI", "Disconnected From Realtime Network Reauthing", ConsoleColor.Red);
                if (Server.SendPostRequestInternal("login") != null)
                {
                    new Thread(() => { SetupSocket(); }).Start();
                }
            }









            var message = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(e.Data));

            if (message.ToString() == "authed")
            { RuntimeConfig.WSAuthed = true; LogHandler.Log("ServerAPI", "Authenticated with RealtimeNetwork", ConsoleColor.Green); }

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
            { Console.WriteLine("Unknown Issue With Server Cypher is working on it dont wory", "Server"); }

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
            if (message.ToLower().Contains("tokenexpirederror") || e.Data.ToString().ToLower().Contains("tokenexpirederror"))
            {
                RuntimeConfig.WSAuthed = false;
                LogHandler.Log("ServerAPI", "Disconnected From Realtime Network Reauthing", ConsoleColor.Red);
                if (Server.SendPostRequestInternal("login") != null)
                {
                    new Thread(() => { SetupSocket(); }).Start();
                }
            }

            if (message.ToLower().Contains("invalid token") || e.Data.ToString().ToLower().Contains("invalid token"))
            {
                RuntimeConfig.WSAuthed = false;
                LogHandler.Log("ServerAPI", "Disconnected From Realtime Network Reauthing", ConsoleColor.Red);
                if (Server.SendPostRequestInternal("login") != null)
                {
                    new Thread(() => { SetupSocket(); }).Start();
                }
            }


            if (message.Contains("Update Packet"))
            {
            //    new Thread(() => { ServerResponceHandler.HandleWSUpdate(message); }).Start();
                // LogHandler.Log("ServerAPI", "Fetched Latest Update");
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

        internal static bool HasConn = false;
        internal protected static readonly ConcurrentQueue<WSSender> toSend = new ConcurrentQueue<WSSender>();
        internal protected static WebSocket wss;
        internal protected static bool ath = false;
        internal protected static bool ShutDown = false;
        internal static string aviserchl = "";
    }
    internal class sendsinglemsg
    {
        internal string uid { get; set; }
        internal string Custommsg { get; set; }
        internal string code { get; set; }
        internal string HWID { get; set; }
        internal string Key { get; set; }
    }


    internal struct WSSender
    {
        internal string Message;
        internal string code;

    }
}
