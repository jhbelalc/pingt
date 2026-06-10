using System;
using System.Net.NetworkInformation;

namespace pingt
{
    /// <summary>
    /// Handles all console output and formatting
    /// </summary>
    public static class ConsoleDisplay
    {
        public static void DisplayHeader(string host)
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly()
                      .GetName().Version?.ToString(3) ?? "unknown";
            Console.WriteLine(new string('=', 80));
            Console.WriteLine($"pingt v{version} - {DateTime.Now:yyyy-MM-dd HH:mm:ss} - Pinging {host}");
            Console.WriteLine("Press CTRL+C to exit");
            Console.WriteLine(new string('=', 80));
        }

        public static void DisplayPingResult(PingReply reply, long pingNumber)
        {
            if (reply == null)
            {
                WriteError($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - No response from host [{pingNumber}]");
                return;
            }

            if (reply.Status == IPStatus.Success)
            {
                WriteSuccess($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Reply from {reply.Address}: " +
                            $"bytes=32 time={reply.RoundtripTime}ms TTL={reply.Options?.Ttl ?? 0} [{pingNumber}]");
            }
            else
            {
                WriteError($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Reply from {reply.Address}: " +
                          $"Status={reply.Status} [{pingNumber}]");
            }
        }

        public static void DisplayError(string message, long pingNumber = 0)
        {
            var counter = pingNumber > 0 ? $" [{pingNumber}]" : string.Empty;
            WriteError($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERROR: {message}{counter}");
        }

        public static void DisplayStatistics(PingStatistics stats, DateTime startTime, DateTime endTime)
        {
            TimeSpan elapsed = endTime - startTime;

            Console.WriteLine();
            Console.WriteLine(new string('=', 80));
            Console.WriteLine($"{endTime:yyyy-MM-dd HH:mm:ss} - Session ended");
            Console.WriteLine($"Start: {startTime:yyyy-MM-dd HH:mm:ss}  |  " +
                            $"End: {endTime:yyyy-MM-dd HH:mm:ss}  |  " +
                            $"Duration: {(int)elapsed.TotalHours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}");
            Console.WriteLine(new string('=', 80));
            WriteSuccess($"Ping Statistics: {stats}");
            Console.WriteLine(new string('=', 80));
        }

        private static void WriteSuccess(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }

        private static void WriteError(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }

        private static void WriteWarning(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = originalColor;
        }
    }
}
