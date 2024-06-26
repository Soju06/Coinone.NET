﻿using Soju06;
using System;

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
        public readonly static Uri APIV2ServerURI = APIServerURL.Uri().Join("/v2/");
        public readonly static Uri APIV1ServerURI = APIServerURL.Uri().Join("/v1/");

        /// <summary>
        /// API 서버 셀렉터
        /// </summary>
        /// <param name="version">버전</param>
        /// <returns></returns>
        public static Uri GetApiServerURI(CoinoneAPIVersion version) => version switch {
            CoinoneAPIVersion.V1 => APIV1ServerURI,
            CoinoneAPIVersion.V2 => APIV2ServerURI,
            _ => null
        };
    }

    /// <summary>
    /// 코인원 버전
    /// </summary>
    public enum CoinoneAPIVersion {
        /// <summary>
        /// 응애 리걸 구지?
        /// </summary>
        V1,
        /// <summary>
        /// G O O D
        /// </summary>
        V2
    }
}