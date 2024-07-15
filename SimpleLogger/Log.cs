namespace SimpleLogger
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error,
    }

    public static class Log
    {
        const string dateFormatter = "yyyy/MM/dd HH:mm:ss";
        const string infoMark = "INFO";
        const string warnMark = "WARN";
        const string erroMark = "ERROR";

        public static void LogMessage(string? message, LogLevel level = LogLevel.Info, DateTime? dateTime = null)
        {
            Console.Write($"[{(dateTime ?? DateTime.Now).ToString(dateFormatter)}] ");
            var (markColor, mark) = level switch
            {
                LogLevel.Info => (ConsoleColor.DarkGreen, infoMark),
                LogLevel.Warning => (ConsoleColor.DarkYellow, warnMark),
                LogLevel.Error => (ConsoleColor.Red, erroMark),
                _ => (ConsoleColor.White, infoMark),
            };
            var tempBackColor = Console.BackgroundColor;
            var tempForeColor = Console.ForegroundColor;

            Console.BackgroundColor = markColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write($"[{mark}]");
            Console.BackgroundColor = tempBackColor;
            Console.ForegroundColor = tempForeColor;
            Console.WriteLine($" {message}");
        }

        public static void LogInfo(string? message, DateTime? dateTime = null)
        {
            LogMessage(message, LogLevel.Info, dateTime);
        }

        public static void LogWarning(string? message, DateTime? dateTime = null)
        {
            LogMessage(message, LogLevel.Warning, dateTime);
        }

        public static void LogError(string? message, DateTime? dateTime = null)
        {
            LogMessage(message, LogLevel.Error, dateTime);
        }
    }
}
