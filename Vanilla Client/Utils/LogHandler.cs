using MelonLoader;

using System.Collections.Concurrent;
using System.Reflection;
using UnityEngine;
using Vanilla.Config;
using Vanilla.Modules;
using WebSocketSharp;
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

        private static readonly MelonLogger.Instance loggerInstance = new MelonLogger.Instance("AbandonWare");
        private static readonly MelonLogger.Instance CypherEngineLogger = new MelonLogger.Instance("CypherEngine");
        internal static void LogToHud(string message) =>  OnScreenUI.AddString(string.Format("<color=#ff0000> {0} </color>", message.ToString()));
        internal static void CypherEngineLog(string Identify, object message, ConsoleColor color = ConsoleColor.White, string caller = null) =>   CypherEngineLogger.Msg(color, "[" + Identify + "] " + message);
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
            if (message is null) return; 
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

        internal static void Error(string identify, object message, ConsoleColor color = ConsoleColor.Red, string caller = null)
        {
            if (message is null) return;
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

        internal static void Dev(string identify, object message, ConsoleColor color = ConsoleColor.Magenta, string caller = null, bool logToHud = true)
        {
            if (DevMode)
            {
                WrittenToConsole.Enqueue(new ConsoleLog
                {
                    identifier = identify,
                    text = message,
                    textColor = color,
                    callerName = caller,
                    LogToHud = logToHud,

                });
                Pop();
            }
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
                            loggerInstance.Msg(color.ClosestConsoleColor(), "[" + result.identifier + "]: " + result.text.StripHtml());
                        else
                        { loggerInstance.Msg(color.ClosestConsoleColor(), "[" + result.identifier + "] [" + result.callerName + "] " + result.text.StripHtml()); }
                    }
                    else
                    {
                        if (result.callerName == null)
                            loggerInstance.Msg(result.textColor, "[" + result.identifier + "]: " + result.text.StripHtml()  );
                        else
                        { loggerInstance.Msg(result.textColor, "[" + result.identifier + "] [" + result.callerName + "] " + result.text.StripHtml()); }
                    }
                }
                else
                {
                    if (result.callerName == null)
                        loggerInstance.Msg(result.textColor, "[" + result.identifier + "]: " + result.text.StripHtml());
                    else
                    { loggerInstance.Msg(result.textColor, "[" + result.identifier + "] [" + result.callerName + "] " + result.text.StripHtml()); }
                }

                if (result.LogToHud)
                {
                    if (string.IsNullOrEmpty(result.text.ToString())) return; 
                      OnScreenUI.AddString($"[{ result.identifier }] <color={result.textColor.ToHex()}> { result.text } </color>");
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

         
      

        internal static void WriteNewLog()
        {

            /* foreach(var key in WrittenToConsole.Keys)
             {
                 loggerInstance.Msg("[{0}]: {1} ", key, WrittenToConsole[key]);
             }
             */

        }


        internal static void ExceptionHandler(string Identify, Exception message, object Info = null, bool IsCritError = false)
        {
            if (IsCritError)
            {
             loggerInstance.Error("ALERT ZUXI THIS IS A CRITICAL ERROR!");   
            }
            loggerInstance.Error($"[{Identify}]: An Oopsie Woopsie occurred  \n\n{Info}\n\n" +
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
