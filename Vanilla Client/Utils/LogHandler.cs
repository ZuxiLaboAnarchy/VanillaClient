using MelonLoader;
using System.Collections.Concurrent;

namespace Vanilla.Utils
{
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

        internal static void CypherEngineLog(string Identify, object message, ConsoleColor color = ConsoleColor.White, string caller = null)
        {
            CypherEngineLogger.Msg(color, "[" + Identify + "] "  + message);
            
        }
        internal static void Log(string Identify, object message, ConsoleColor color = ConsoleColor.White, string caller = null)
        {
            WrittenToConsole.Enqueue(new ConsoleLog
            {
                identifier = Identify,
                text = message,
                textColor = color,
                callerName = caller,

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

                if (result.callerName == null)
                    loggerInstance.Msg(result.textColor, "[" + result.identifier + "]: " + result.text);
                else
                { loggerInstance.Msg(result.textColor, "[" + result.identifier + "] [" + result.callerName + "] " + result.text); }

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




    }

    internal struct ConsoleLog
    {
        internal string identifier;

        internal object text;

        internal ConsoleColor textColor;

        internal string callerName;

    }
}
