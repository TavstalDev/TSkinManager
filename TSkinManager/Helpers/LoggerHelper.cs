﻿using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Tavstal.TSkinManager.Helpers
{
    internal static class LoggerHelper
    {
        private static string Name = Assembly.GetExecutingAssembly().GetName().Name;

        public static void Log(object message, ConsoleColor color = ConsoleColor.Green, string prefix = "[INFO] >")
        {

            string text = prefix + " " + message.ToString();
            try
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                Console.ForegroundColor = color;
                using (StreamWriter streamWriter = File.AppendText(Path.Combine(Rocket.Core.Environment.LogsDirectory, Rocket.Core.Environment.LogFile)))
                {
                    streamWriter.WriteLine(string.Concat("[", DateTime.Now, "] ", string.Format("[{0}] {1}", Name, text)));
                    streamWriter.Close();
                }
                Console.WriteLine(text);
                Console.ForegroundColor = oldColor;
            }
            catch
            {
                Rocket.Core.Logging.Logger.Log(text);
            }
        }

        public static void LogWarning(object message, ConsoleColor color = ConsoleColor.Yellow, string prefix = "[WARNING] >")
        {
            Log(message, color, prefix);
        }

        public static void LogException(object message, ConsoleColor color = ConsoleColor.DarkYellow, string prefix = "[EXCEPTION] >")
        {
            Log(message, color, prefix);
        }

        public static void LogError(object message, ConsoleColor color = ConsoleColor.Red, string prefix = "[ERROR] >")
        {
            Log(message, color, prefix);
        }

        public static void LogLateInit()
        {
            ConsoleColor oldFGColor = Console.ForegroundColor;
            ConsoleColor oldBGColor = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            string text = $"######## {Name} LATE INIT ########";
            try
            {
                using (StreamWriter streamWriter = File.AppendText(Path.Combine(Rocket.Core.Environment.LogsDirectory, Rocket.Core.Environment.LogFile)))
                {
                    streamWriter.WriteLine(string.Concat("[", DateTime.Now, "] ", string.Format("[{0}] {1}", Name, text)));
                    streamWriter.Close();
                }
                Console.WriteLine(text);
            }
            catch
            {
                Rocket.Core.Logging.Logger.Log(text);
            }
            Console.ForegroundColor = oldFGColor;
            Console.BackgroundColor = oldBGColor;
        }

        public static void LogCommand(object message, ConsoleColor color = ConsoleColor.Blue, string prefix = "[Command] >")
        {
            string msg = message.ToString().Replace("((", "{").Replace("))", "}").Replace("[TShop]", "");
            int amount = msg.Split('{').Length;
            for (int i = 0; i < amount; i++)
            {
                Regex regex = new Regex(string.Format("{0}(.*?){1}", Regex.Escape("{"), Regex.Escape("}")), RegexOptions.RightToLeft);
                msg = regex.Replace(msg, "{" + "}");
            }

            Log(msg.Replace("{", "").Replace("}", ""), color, prefix);
        }
    }
}
