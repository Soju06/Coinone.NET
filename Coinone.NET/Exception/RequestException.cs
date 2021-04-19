using System;

namespace CoinoneNET.Exception {
    /// <summary>
    /// 요청 한도 초과
    /// </summary>
    [Serializable]
    public class CoinoneRequestLimitExceededException : System.Exception {
        public const string ExceptionMessage = "Requests per allotted time exceeded.";
        public CoinoneRequestLimitExceededException(uint limit) : base($"{ExceptionMessage}, {limit}") { 
            
        }
    }

    /// <summary>
    /// 엑세스 토큰(보안 문자열)이 만료되었습니다.
    /// 민감한 요청을 재탕하지 마세요! ㅋ
    /// </summary>
    [Serializable]
    public class CoinoneDefaultRequestAccessTokenExpirationException : System.Exception {
        public CoinoneDefaultRequestAccessTokenExpirationException() : 
            base("The access token (security string) has expired.\nDon't re-use sensitive requests!") { }
    }
}
