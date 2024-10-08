using MelonLoader;

using System.Collections.Concurrent;
using System.Reflection;
using UnityEngine;
using Vanilla.Modules;
using static Il2CppSystem.TypeIdentifiers;

namespace Vanilla.Utils
{
    [Obfuscation(Feature = "-flow")]
    [Obfuscation(Feature = "-strenc")]
    [Obfuscation(Feature = "-virtualization")]
    [Obfuscation(Feature = "-rename")]
    internal class LogHandler
    {
#if DEBUG
        internal protected readonly static bool DevMode = true;
#else
        internal protected readonly static bool DevMode = false;
#endif

        // static Dictionary<string, object> WrittenToConsole = new Dictionary<string, object>();

        private static readonly ConcurrentQueue<ConsoleLog> WrittenToConsole = new ConcurrentQueue<ConsoleLog>();

        private static readonly MelonLogger.Instance loggerInstance = new MelonLogger.Instance("VanillaClient");
        private static readonly MelonLogger.Instance CypherEngineLogger = new MelonLogger.Instance("CypherEngine");
        private static HUDNotifyGlobal HUDInstance = null; //new();



        internal static void SetupHud()
        {

            GameObject HUDObject = GameObject.Find("HUD_UI 2(Clone)/VR Canvas/Container/Center/F2/User Event Carousel");
            HUDInstance = HUDObject.GetComponent<HUDNotifyGlobal>();


            // HUDInstance.Method_Public_Void_String_Sprite_0(message, null);//ImageUtils.CreateSprite(AssetLoader.LoadTexture("VanillaClientLogo")));




        }

        internal static void LogToHud(string message)
        {
            // Sprite sprite= ImageUtils.CreateSprite(AssetLoader.LoadTexture("VanillaClientLogo");
         //   HUDInstance.Method_Public_Void_String_Sprite_0(message, ImageUtils.CreateSprite(AssetLoader.LoadTexture("VanillaClientLogo")));

            // HUDInstance.Method_Public_Void_String_Sprite_0(message, null);//ImageUtils.CreateSprite(AssetLoader.LoadTexture("VanillaClientLogo")));

           
                OnScreenUI.AddString(
                    string.Format("<color=#ff0000> {0} </color>", message.ToString()));


        }

        internal static void CypherEngineLog(string Identify, object message, ConsoleColor color = ConsoleColor.White, string caller = null)
        {
            CypherEngineLogger.Msg(color, "[" + Identify + "] " + message);

        }


        internal static void HudLog(string Identify, object message, ConsoleColor ConsoleColor = ConsoleColor.White, string caller = null)
        {





            WrittenToConsole.Enqueue(new ConsoleLog
            {
                identifier = Identify,
                text = message,
                textColor = ConsoleColor,
                callerName = caller,
                LogToHud = true,

            });
            Pop();

        }

        internal static void Log(string identify, object message, ConsoleColor color = ConsoleColor.White, string caller = null)
        {
            WrittenToConsole.Enqueue(new ConsoleLog
            {
                identifier = identify,
                text = message,
                textColor = color,
                callerName = caller,
                LogToHud = true,

            });
            Pop();
        }
        internal static int CurCue;

        internal static void Pop()
        {
            for (int i = 0; i < WrittenToConsole.Count; i++)
            {



                if (!WrittenToConsole.TryDequeue(out var result))
                { continue; }



                int num = result.text.ToString().LastIndexOf("<color=");
                if (num != -1)
                {
                    int num2 = result.text.ToString().IndexOf('>', num);
                    string htmlString = result.text.ToString().Substring(num + 7, num2 - (num + 7));
                    if (ColorUtility.TryParseHtmlString(htmlString, out var color))
                    {
                        //  Log(Identify, message, ClosestConsoleColor((byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f)));

                        if (result.callerName == null)
                            loggerInstance.Msg(ClosestConsoleColor((byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f)), "[" + result.identifier + "]: " + result.text);
                        else
                        { loggerInstance.Msg(ClosestConsoleColor((byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f)), "[" + result.identifier + "] [" + result.callerName + "] " + result.text); }


                    }
                    else
                    {
                        if (result.callerName == null)
                            loggerInstance.Msg(result.textColor, "[" + result.identifier + "]: " + result.text);
                        else
                        { loggerInstance.Msg(result.textColor, "[" + result.identifier + "] [" + result.callerName + "] " + result.text); }
                    }
                }
                else
                {
                    if (result.callerName == null)
                        loggerInstance.Msg(result.textColor, "[" + result.identifier + "]: " + result.text);
                    else
                    { loggerInstance.Msg(result.textColor, "[" + result.identifier + "] [" + result.callerName + "] " + result.text); }
                }





                if (result.LogToHud)
                {
                      OnScreenUI.AddString("[" + result.identifier + "] <color=#00aeff>" + result.text + "</color>");
                   // LogToHud("[" + result.identifier + "] " + result.text.ToString());
                }

            }


        }


        internal static void RePop()
        {
            for (int i = 0; i < WrittenToConsole.Count; i++)
            {
                if (!WrittenToConsole.TryPeek(out var result))
                {
                    continue;
                }

                if (result.callerName == null)
                    loggerInstance.Msg(result.textColor, "[" + result.identifier + "]: " + result.text);
                else
                { loggerInstance.Msg(result.textColor, "[" + result.identifier + "] [" + result.callerName + "] " + result.text); }
                CurCue++;
            }


        }


        internal static void Dev(string Identify, object message, ConsoleColor color = ConsoleColor.DarkMagenta)
        {
            if (DevMode)
            {
                loggerInstance.Msg(color, $"[{Identify}]: {message} ");
            }
        }

        internal static void WriteNewLog()
        {

            /* foreach(var key in WrittenToConsole.Keys)
             {
                 loggerInstance.Msg("[{0}]: {1} ", key, WrittenToConsole[key]);
             }
             */

        }


        internal static void ExceptionHandler(string Identify, Exception message, object Info = null)
        {
            loggerInstance.Error($"[{Identify}]: An Exception Occoured \n\n{Info}\n\n" +

                "----------{Where?}----------\n\n" +
                $"{message.TargetSite}\n\n" +
                "----------{Message}----------\n\n" +
                $"{message.Message}\n\n" +
                "----------{ExceptionData}----------\n\n" +
                $"{message.Data}\n\n" +
                "----------{StackTrace}----------\n\n" +
                $"{message.StackTrace}\n\n" +
                 "----------{InnerException}----------\n\n" +
                $"{message.InnerException}\n\n" +
                "---------{EndOfException}----------\n\n" +
                $"");
        }


        internal static ConsoleColor ClosestConsoleColor(byte r, byte g, byte b)
        {
            ConsoleColor result = ConsoleColor.White;
            double num = (int)r;
            double num2 = (int)g;
            double num3 = (int)b;
            double num4 = double.MaxValue;
            foreach (ConsoleColor value in Enum.GetValues(typeof(ConsoleColor)))
            {
                string name = Enum.GetName(typeof(ConsoleColor), value);
                System.Drawing.Color color = System.Drawing.Color.FromName((name == "DarkYellow") ? "Orange" : name);
                double num5 = Math.Pow((double)(int)color.R - num, 2.0) + Math.Pow((double)(int)color.G - num2, 2.0) + Math.Pow((double)(int)color.B - num3, 2.0);
                if (num5 == 0.0)
                {
                    return value;
                }
                if (num5 < num4)
                {
                    num4 = num5;
                    result = value;
                }
            }
            return result;
        }

    }

    internal struct ConsoleLog
    {
        internal string identifier;

        internal object text;

        internal ConsoleColor textColor;

        internal string callerName;

        internal bool LogToHud;

    }
}
