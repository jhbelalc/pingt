using System;
using System.Net.NetworkInformation;

namespace pingt
{
    /// <summary>
    /// Tracks statistics for ping operations
    /// </summary>
    public class PingStatistics
    {
        private long _totalPings;
        private int _errors;
        private long _minRoundtripTime = long.MaxValue;
        private long _maxRoundtripTime = long.MinValue;
        private long _totalRoundtripTime;
        private int _successfulPings;

        public long TotalPings => _totalPings;
        public int Errors => _errors;
        public int SuccessfulPings => _successfulPings;
        public long MinRoundtripTime => _minRoundtripTime == long.MaxValue ? 0 : _minRoundtripTime;
        public long MaxRoundtripTime => _maxRoundtripTime == long.MinValue ? 0 : _maxRoundtripTime;
        
        public double AverageRoundtripTime => _successfulPings > 0 
            ? _totalRoundtripTime / (double)_successfulPings 
            : 0;

        public double SuccessRate => _totalPings > 0 
            ? (_successfulPings / (double)_totalPings) * 100 
            : 0;

        public void IncrementTotal() => _totalPings++;

        public void IncrementErrors() => _errors++;

        public void UpdateFromReply(PingReply reply)
        {
            if (reply.Status == IPStatus.Success)
            {
                _successfulPings++;
                _totalRoundtripTime += reply.RoundtripTime;
                _minRoundtripTime = Math.Min(_minRoundtripTime, reply.RoundtripTime);
                _maxRoundtripTime = Math.Max(_maxRoundtripTime, reply.RoundtripTime);
            }
            else
            {
                _errors++;
            }
        }

        public override string ToString()
        {
            return $"Total: {TotalPings} | Success: {SuccessfulPings} ({SuccessRate:F2}%) | " +
                   $"Errors: {Errors} | Min: {MinRoundtripTime}ms | " +
                   $"Avg: {AverageRoundtripTime:F2}ms | Max: {MaxRoundtripTime}ms";
        }
    }
}
