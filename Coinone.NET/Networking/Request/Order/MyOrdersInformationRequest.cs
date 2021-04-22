using Soju06.Web.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Request.Order {
    public class CoinoneMyOrdersInformationRequest : CoinoneDefaultRequest {
        public CoinoneMyOrdersInformationRequest(SecureString accessToken) : base(accessToken) {

        }

        public CoinoneMyOrdersInformationRequest(SecureString accessToken, string orderID) : base(accessToken) {
            OrderID = orderID;
        }

        protected override void SerializeV(XElement element) {
            element.Add(JsonUtility.CreateElement("currency", Currency, "string"));
            element.Add(JsonUtility.CreateElement("order_id", OrderID, "string"));
            base.SerializeV(element);
        }

        /// <summary>
        /// 코인 코드
        /// 기본값: BTC
        /// </summary>
        public string Currency { get; set; } = "BTC";

        /// <summary>
        /// 주문 ID
        /// </summary>
        public string OrderID { get; set; }
    }
}
