using CoinoneNET.Networking.Request;
using Soju06;
using Soju06.Task;
using Soju06.Web.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoinoneNET.Networking {
    /// <summary>
    /// 서버 시간
    /// </summary>
    public class CoinoneTime {
        private CancellationTokenSource CancellationToken { get; }
        private CoinoneRequestLimitManager RequestLimitManager { get; }
        private RefreshObject<TimeSpan> RefreshTime { get; }

        public CoinoneTime(CoinoneRequestLimitManager manager) {
            RefreshTime = new(new TimeSpan(0, 5, 0), new(0));
            CancellationToken = new();
            RequestLimitManager = manager;
            Task.Run(async () => await Loop(CancellationToken.Token));
        }

        ~CoinoneTime() {
            CancellationToken?.Cancel();
            CancellationToken?.Dispose();
        }

        /// <summary>
        /// 서버 현재 시간
        /// </summary>
        public DateTime Now {
            get => DateTime.UtcNow + RefreshTime.Object;
        }

        private async Task Loop(CancellationToken token) {
            RefreshTime.Object = await GetTime(token);
            while (!token.IsCancellationRequested) {
                try {
                    if (RefreshTime.IsTimeout && !RequestLimitManager.IsLimitExceeded) {
                        await RequestLimitManager.CountUpAsync(token);
                        RefreshTime.Object = await GetTime(token);
                    }
                    await Task.Delay(1000, token);
                } catch {

                }
            }
        }

        private async Task<TimeSpan> GetTime(CancellationToken token) {
            try {
                return DateTime.UtcNow - await TimeUtility
                    .GetServerDateTime("https://api.coinone.co.kr/v2/", token) ?? new(0);
            } catch {
                return new(0);
            }
        }
    }
}
