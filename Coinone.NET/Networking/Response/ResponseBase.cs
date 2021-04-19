using System;
using System.Xml.Linq;
using CoinoneNET.Exception;

namespace CoinoneNET.Networking.Response {
    public abstract class CoinoneResponseBase {
        internal CoinoneResponseBase(XElement element) {
            ErrorMessage = element.Element("errorMsg")?.Value;
            ErrorCodeS = element.Element("errorCode")?.Value;
            ResultS = element.Element("result")?.Value;
        }

        /// <summary>
        /// 결과 메시지
        /// </summary>
        public CoinoneResponseResults Result { get; internal set; } = CoinoneResponseResults.None;
        /// <summary>
        /// 오류 코드
        /// </summary>
        public CoinoneErrorCodes ErrorCode { get; internal set; } = CoinoneErrorCodes.Done;

        public string ErrorMessage { get; internal set; }

        internal string ResultS {
            get => Result.ToString();
            set {
                if (Enum.TryParse(value, out CoinoneResponseResults r))
                    Result = r;
            } 
        }

        internal string ErrorCodeS {
            get => ErrorCode.ToString();
            set {
                if (Enum.TryParse(value, out CoinoneErrorCodes r))
                    ErrorCode = r;
            } 
        }

        /// <summary>
        /// 만약, 오류가 발생하였을 경후 <c></c>
        /// </summary>
        public void ThrowResponseError() {
            if (Result != CoinoneResponseResults.Success)
                throw new CoinoneResponseErrorExceptionException($"{ErrorCode} / {ErrorMessage}");
        }
    }

    /// <summary>
    /// 응답 결과
    /// </summary>
    public enum CoinoneResponseResults {
        /// <summary>
        /// 일반적인 경후 발생하지 않음
        /// </summary>
        None,
        /// <summary>
        /// 오류
        /// </summary>
        Error,
        /// <summary>
        /// 성공
        /// </summary>
        Success
    }
}
