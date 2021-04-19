using CoinoneNET.Networking.Request;
using System;

namespace CoinoneNET.Networking
{
    /// <summary>
    /// 코인원 네트워크
    /// </summary>
    public class CoinoneNetwork {
        /// <summary>
        /// 코인원 현재 시간
        /// 5분마다 자동 시차 조율됩니다.
        /// </summary>
        public static DateTime CoinoneNowDateTime { get => (_coinoneTime ??= new(RequestLimitManager)).Now; }

        /// <summary>
        /// 요청 한도 관리자
        /// </summary>
        public static CoinoneRequestLimitManager RequestLimitManager { get => _requestLimitManager ??= new(true); }

        private static CoinoneRequestLimitManager _requestLimitManager;
        private static CoinoneTime _coinoneTime;
    }
}
