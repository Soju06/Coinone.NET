using Soju06.Collections;
using Soju06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Order {
    /// <summary>
    /// 내 주문s 정보
    /// </summary>
    public class CoinoneMyLimitOrdersResponse : CoinoneResponseBase {
        public CoinoneMyLimitOrdersResponse(XElement element) : base(element) {
            Orders = new(element?.Element("limitOrders"));
        }

        /// <summary>
        /// 주문s
        /// </summary>
        public CoinoneLimitOrders Orders { get; }
    }

    /// <summary>
    /// 주문 리스트
    /// </summary>
    public class CoinoneLimitOrders : LockableList<CoinoneLimitOrderInfo> {
        internal CoinoneLimitOrders(XElement element) {
            if(element is not null && element.HasElements)
                foreach (var item in element.Elements())
                    Add(new(item));
            Lock();
        }
    }

    public class CoinoneLimitOrderInfo: CoinoneOrderInfo {
        internal CoinoneLimitOrderInfo(XElement element) : base(element) {
            if (element is null || !element.HasElements) return;
            if (long.TryParse(IndexS = element.Element("index")?.Value, out var y))
                Index = y;
        }

        /// <summary>
        /// 번호
        /// </summary>
        public long Index { get; }
        /// <summary>
        /// 번호 문자열
        /// </summary>
        public string IndexS { get; }

        public override string ToString() => $"{Index}. {Price} / {Qty} | {Type}, {FeeRateS}";
    }
}
