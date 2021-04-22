using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Order {
    /// <summary>
    /// 체결된 오더
    /// </summary>
    public class CoinoneMyCompleteOrdersResponse : CoinoneResponseBase {
        public CoinoneMyCompleteOrdersResponse(XElement element) : base(element) {
            CompleteOrders = new(element?.Element("completeOrders"));
        }

        /// <summary>
        /// 체결된 오더s
        /// </summary>
        public CoinoneLimitOrders CompleteOrders { get; }
    }
}
