using Soju06.Expansion;
using Soju06.Web.Json;
using System.Security;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Request {
    /// <summary>
    /// 기본 요청
    /// </summary>
    public class CoinoneDefaultRequest : CoinoneSerializableRequestBase {
        public CoinoneDefaultRequest(SecureString accessToken) {
            AccessToken = accessToken.Copy();
        }

        protected sealed override void SerializeObject(ref XElement element) {
            SerializeV(element);
            element.Add(JsonUtility.CreateElement("access_token", AccessToken.GetStringAndDispose(), "string"));
        }

        protected virtual void SerializeV(XElement element) {

        }

        /// <summary>
        /// 보안 문자열 엑세스 토큰이 만료되었다면.
        /// </summary>
        public bool IsExpiration { get => AccessToken == null; }

        /// <summary>
        /// .NET Framework에서만 보안 문자열이 적용됩니다.
        /// </summary>
        private SecureString AccessToken { get; }
    }
}
