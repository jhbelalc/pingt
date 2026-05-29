using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace pingt
{
    class Program
    {
        private static readonly string DefaultHost = "www.google.com";
        private static readonly int DefaultTimeout = 1000; // ms
        private static readonly int DefaultDelay = 1000;   // ms

        static async Task Main(string[] args)
        {
            string host = ParseHost(args);
            var cts = new CancellationTokenSource();

            // Handle Ctrl+C gracefully
            Console.CancelKeyPress += (sender, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            using (var pingService = new PingService(DefaultTimeout, DefaultDelay))
            {
                var stats = new PingStatistics();

                try
                {
                    ConsoleDisplay.DisplayHeader(host);
                    await ExecutePingAsync(pingService, host, stats, cts.Token);
                }
                catch (ArgumentException ex)
                {
                    ConsoleDisplay.DisplayError($"Invalid argument: {ex.Message}");
                }
                catch (InvalidOperationException ex)
                {
                    ConsoleDisplay.DisplayError(ex.Message);
                }
                catch (OperationCanceledException)
                {
                    // Expected when Ctrl+C is pressed
                }
                catch (Exception ex)
                {
                    ConsoleDisplay.DisplayError($"Unexpected error: {ex.Message}");
                }
                finally
                {
                    ConsoleDisplay.DisplayStatistics(stats);
                }
            }
        }

        private static string ParseHost(string[] args)
        {
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                return args[0];
            }

            return DefaultHost;
        }

        private static async Task ExecutePingAsync(
            PingService pingService,
            string host,
            PingStatistics stats,
            CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var pinger = new Ping();
                    var reply = await Task.Run(
                        () => pinger.Send(host, DefaultTimeout),
                        cancellationToken);

                    stats.IncrementTotal();

                    if (reply != null)
                    {
                        stats.UpdateFromReply(reply);
                        ConsoleDisplay.DisplayPingResult(reply);
                    }
                    else
                    {
                        stats.IncrementErrors();
                        ConsoleDisplay.DisplayPingResult(null!);
                    }

                    pinger.Dispose();
                }
                catch (PingException ex)
                {
                    stats.IncrementErrors();
                    ConsoleDisplay.DisplayError($"Ping failed: {ex.Message}");
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                try
                {
                    await Task.Delay(DefaultDelay, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
    }
}
