using Soju06.Web.Json;
using System.Security;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Request.Order {
    /// <summary>
    /// 한도 구매? 제한 구매?
    /// </summary>
    public class CoinoneLimitOrderRequest : CoinoneDefaultRequest {
        public CoinoneLimitOrderRequest() {

        }

        public CoinoneLimitOrderRequest(SecureString s) : base(s) {

        }

        protected override void SerializeV(XElement element) {
            element.Add(JsonUtility.CreateElement("price", Price, "string"));
            element.Add(JsonUtility.CreateElement("qty", Qty, "number"));
            element.Add(JsonUtility.CreateElement("currency", Currency, "string"));
            element.Add(JsonUtility.CreateElement("is_post_only", IsPostOnly.ToString().ToLower(), "boolean"));
            base.SerializeV(element);
        }

        /// <summary>
        /// 가격
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 수량
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 코인 코드
        /// 기본값: BTC
        /// </summary>
        public string Currency { get; set; } = "BTC";

        /// <summary>
        /// 이게 먼소리여..
        /// 기본값: false
        /// </summary>
        public bool IsPostOnly = false;
    }
}
