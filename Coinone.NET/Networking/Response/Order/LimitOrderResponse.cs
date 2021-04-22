using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Order {
    /// <summary>
    /// 주문 응답
    /// </summary>
    public class CoinoneLimitOrderResponse : CoinoneResponseBase {
        public CoinoneLimitOrderResponse(XElement element) : base(element) {
            // 아니 ㅋㅋㅋ 주문 취소할땐 order_id 더니
            // 주문 할땐 orderId ㅋㅋㅋㅋ
            // 인계 제대로 안하누
            OrderId = element.Element("orderId")?.Value;
        }

        /// <summary>
        /// 주문 ID
        /// </summary>
        public string OrderId { get; set; }
    }
}
