using Soju06.Web.Json;
using System.Security;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Request.Order {
    public class CoinoneMyOrdersRequest : CoinoneDefaultRequest {
        public CoinoneMyOrdersRequest(SecureString accessToken) : base(accessToken) {

        }

        protected override void SerializeV(XElement element) {
            element.Add(JsonUtility.CreateElement("currency", Currency, "string"));
            base.SerializeV(element);
        }

        /// <summary>
        /// 통화
        /// 기본값: BTC
        /// </summary>
        public string Currency { get; set; } = "BTC";
    }
}
