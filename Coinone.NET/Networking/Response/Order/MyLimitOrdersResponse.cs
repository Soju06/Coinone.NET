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

    /// <summary>
    /// 주문 정보
    /// </summary>
    public class CoinoneLimitOrderInfo {
        internal CoinoneLimitOrderInfo(XElement element) {
            if (element is null || !element.HasElements) return;
            OrderId = element.Element("orderId")?.Value;
            if (long.TryParse(TimestampS = element.Element("timestamp")?.Value, out var r))
                Timestamp = r.UnixTimestampToDateTime();
            if (long.TryParse(IndexS = element.Element("index")?.Value, out var y))
                Index = y;
            if (decimal.TryParse(PriceS = element.Element("price")?.Value, out var e))
                Price = e;
            if (decimal.TryParse(QtyS = element.Element("qty")?.Value, out var w))
                Qty = w;
            if (decimal.TryParse(FeeRateS = element.Element("feeRate")?.Value, out var t))
                FeeRate = t;
            if (CoinoneMyOrderInfo.OrderTypeTryParse(TypeS = element.Element("ask")?.Value, out var f))
                Type = f;
        }

        /// <summary>
        /// 번호
        /// </summary>
        public long Index { get; }
        /// <summary>
        /// 번호 문자열
        /// </summary>
        public string IndexS { get; }
        /// <summary>
        /// 주문 ID
        /// </summary>
        public string OrderId { get; }
        /// <summary>
        /// 주문 시간
        /// </summary>
        public DateTime Timestamp { get; }
        /// <summary>
        /// 주문 시간 문자열
        /// </summary>
        public string TimestampS { get; }
        /// <summary>
        /// 가격
        /// </summary>
        public decimal Price { get; }
        /// <summary>
        /// 가격 문자열
        /// </summary>
        public string PriceS { get; }
        /// <summary>
        /// 수량
        /// </summary>
        public decimal Qty { get; }
        /// <summary>
        /// 수량 문자열
        /// </summary>
        public string QtyS { get; }
        /// <summary>
        /// 주문 타입
        /// </summary>
        public CoinoneOrderTypes Type { get; }
        /// <summary>
        /// 주문 타입 문자열
        /// </summary>
        public string TypeS { get; }
        /// <summary>
        /// 수수료? 세금?
        /// </summary>
        public decimal FeeRate { get; }
        /// <summary>
        /// 수수료? 세금? 문자열
        /// </summary>
        public string FeeRateS { get; }

        public override string ToString() => $"{Index}. {Price} / {Qty} | {Type}, {FeeRateS}";
    }
}
