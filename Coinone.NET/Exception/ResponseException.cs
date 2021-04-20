using System;

namespace CoinoneNET.Exception {
    /// <summary>
    /// 서버 응답이 200이 아닙니다
    /// </summary>
    [Serializable]
    public class CoinoneResponseErrorExceptionException : System.Exception {
        public CoinoneResponseErrorExceptionException() { }
        public CoinoneResponseErrorExceptionException(string message) : base(message) { }
        public CoinoneResponseErrorExceptionException(string message, System.Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// 응답을 역직렬화 하는데 실패했습니다.
    /// </summary>
    [Serializable]
    public class CoinoneResponseDecodeFaliedException : System.Exception {
        public CoinoneResponseDecodeFaliedException() { }
        public CoinoneResponseDecodeFaliedException(string message) : base(message) { }
        public CoinoneResponseDecodeFaliedException(string message, System.Exception inner) : base(message, inner) { }
    }
}
