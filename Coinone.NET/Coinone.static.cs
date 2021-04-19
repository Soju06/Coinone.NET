namespace CoinoneNET {
    /// <summary>
    /// 코인원
    /// </summary>
    public partial class Coinone {
        /// <summary>
        /// 공용 API
        /// 분당 요청 최대 횟수
        /// 상기 고지보다 많은 요청이 발생하면 10분간 차단됩니다.
        /// </summary>
        //{
        //    "result": "error",
        //    "errorCode": "4",
        //    "errorMsg": "Blocked user access."
        //}
        public const uint PublicAPIRequestLimitPerMinute = 300;
        /// <summary>
        /// 개인용 API
        /// 초당 요청 최대 횟수
        /// 상기 고지보다 많은 요청이 발생하면 10분간 차단됩니다.
        /// </summary>
        //{
        //    "result": "error",
        //    "errorCode": "4",
        //    "errorMsg": "Blocked user access."
        //}
        public const uint PrivateAPIRequestLimitPerSecond = 10;
    }
}