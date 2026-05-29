using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace pingt
{
    /// <summary>
    /// Service responsible for executing ping operations
    /// </summary>
    public class PingService : IDisposable
    {
        private readonly int _timeoutMs;
        private readonly int _delayBetweenPingsMs;
        private Ping _pinger;

        public PingService(int timeoutMs = 1000, int delayBetweenPingsMs = 1000)
        {
            _timeoutMs = timeoutMs;
            _delayBetweenPingsMs = delayBetweenPingsMs;
            _pinger = new Ping();
        }

        /// <summary>
        /// Continuously ping a host until cancellation is requested
        /// </summary>
        public async Task PingHostAsync(string host, PingStatistics stats, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new ArgumentException("Host cannot be null or empty", nameof(host));
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var reply = await Task.Run(() => _pinger.Send(host, _timeoutMs), cancellationToken);
                    stats.IncrementTotal();

                    if (reply == null)
                    {
                        stats.IncrementErrors();
                        continue;
                    }

                    stats.UpdateFromReply(reply);
                }
                catch (PingException ex)
                {
                    stats.IncrementErrors();
                    throw new InvalidOperationException($"Failed to ping host '{host}'", ex);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                await Task.Delay(_delayBetweenPingsMs, cancellationToken);
            }
        }

        public void Dispose()
        {
            _pinger?.Dispose();
        }
    }
}
