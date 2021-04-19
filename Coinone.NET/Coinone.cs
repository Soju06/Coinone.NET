using Soju06;
using System;

namespace CoinoneNET {
    public sealed partial class Coinone {
        /// <summary>
        /// API 서버 주소
        /// </summary>
        public const string APIServerAddress = "api.coinone.co.kr";
        /// <summary>
        /// API 서버 URL
        /// </summary>
        public const string APIServerURL = "https://api.coinone.co.kr/";
        /// <summary>
        /// API 서버 Uri
        /// </summary>
        public readonly static Uri APIServerURI = APIServerURL.Uri();

        /// <summary>
        /// 코인원 V2 API
        /// </summary>
        public Coinone() {

        }
    }
}
