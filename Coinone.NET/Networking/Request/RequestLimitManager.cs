using CoinoneNET.Exception;
using Soju06;
using System;

namespace CoinoneNET.Networking.Request {
    /// <summary>
    /// 요청 한도 관리자입니다.
    /// 코인원에서 10분 차단 당하기 싫으면 내비 두세요!
    /// </summary>
    public sealed class CoinoneRequestLimitManager {
        private readonly RefreshRequestCount RequestCount;

        /// <summary>
        /// 총 요청 횟수
        /// </summary>
        public ulong TotalRequestCount { get; private set; }
        /// <summary>
        /// API 요청 한도
        /// </summary>
        public uint RequestLimit { get; }
        /// <summary>
        /// 비공개 API 여부
        /// </summary>
        public bool IsPrivateAPI { get; }

        internal CoinoneRequestLimitManager(bool isPrivateAPI) {
            IsPrivateAPI = isPrivateAPI;
            RequestCount = new(IsPrivateAPI ? new (0, 1, 0) : new(0, 0, 1));
            RequestLimit = IsPrivateAPI ? Coinone.PrivateAPIRequestLimitPerSecond : Coinone.PublicAPIRequestLimitPerMinute;
        }

        /// <summary>
        /// 한도 초기화 후 요청된 횟수
        /// </summary>
        public uint Count {
            get => RequestCount.Value;
        }

        /// <summary>
        /// 한도 초과 여부
        /// </summary>
        public bool IsLimitExceeded {
            get => Count >= RequestLimit;
        }

        private readonly object CountLock = new();

        /// <summary>
        /// 카운트 추가
        /// 한도 초과시 <c>RequestLimitExceededException</c>예외를 발생시킵니다.
        /// </summary>
        /// <exception cref="RequestLimitExceededException" />
        internal void CountUp() {
            lock (CountLock) {
                ThrowRequestLimitExceeded();
                RequestCount.CountUp();
                TotalRequestCount++;
            }
        }

        /// <summary>
        /// 한도 도달시 <c>RequestLimitExceededException</c>예외를 발생시킵니다.
        /// </summary>
        /// <exception cref="RequestLimitExceededException" />
        public void ThrowRequestLimitExceeded() {
            if (Count >= RequestLimit) throw new CoinoneRequestLimitExceededException(RequestLimit);
        }

        public class RefreshRequestCount : RefreshObject<uint> { 
            internal RefreshRequestCount(TimeSpan refreshCycle, 
                uint count = 0) : base(refreshCycle, count) {

            }

            /// <summary>
            /// 값
            /// </summary>
            public uint Value { 
                get => IsTimeout ? Object = 0 : Object;
                set => Object = value;
            }

            /// <summary>
            /// 카운트 추가
            /// </summary>
            public void CountUp() {
                _object = Value + 1;
            }
        }
    }
}
