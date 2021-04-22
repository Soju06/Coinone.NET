using Soju06.Web.Json;
using System.Security;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Request.Order {
    /// <summary>
    /// 주문 취소
    /// </summary>
    public class CoinoneCancelOrderRequest : CoinoneDefaultRequest {
        public CoinoneCancelOrderRequest(SecureString s) : base(s) {

        }

        protected override void SerializeV(XElement element) {
            element.Add(JsonUtility.CreateElement("order_id", OrderID, "string"));
            element.Add(JsonUtility.CreateElement("price", Price, "string"));
            element.Add(JsonUtility.CreateElement("qty", Qty, "number"));
            element.Add(JsonUtility.CreateElement("is_ask", IsAsk, "number"));
            element.Add(JsonUtility.CreateElement("currency", Currency, "string"));
            base.SerializeV(element);
        }

        /// <summary>
        /// 주문 ID
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 가격
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 수량
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 주문이 매도인 경우 1
        /// </summary>
        public uint IsAsk { get; set; }
        /// <summary>
        /// 통화
        /// 기본값: BTC
        /// </summary>
        public string Currency { get; set; } = "BTC";
    }
}
