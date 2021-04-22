using Soju06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Order
{
    /// <summary>
    /// 내 오더 정보
    /// </summary>
    public class CoinoneMyOrderInformationResponse : CoinoneResponseBase {
        public CoinoneMyOrderInformationResponse(XElement element) : base(element) {
            OrderInfo = new(element?.Element("info"));
        }

        /// <summary>
        /// 오더 정보
        /// </summary>
        public CoinoneMyOrderInfo OrderInfo { get; }
    }

    /// <summary>
    /// 오더 정보
    /// </summary>
    public class CoinoneMyOrderInfo {
        internal CoinoneMyOrderInfo(XElement element) {
            if (element is null || !element.HasElements) return;
            OrderId = element.Element("orderId")?.Value;
            Currency = element.Element("currency")?.Value;
            if (long.TryParse(TimestampS = element.Element("timestamp")?.Value, out var r))
                Timestamp = r.UnixTimestampToDateTime();
            if (decimal.TryParse(PriceS = element.Element("price")?.Value, out var e))
                Price = e;
            if (decimal.TryParse(QtyS = element.Element("qty")?.Value, out var w))
                Qty = w;
            if (decimal.TryParse(FeeRateS = element.Element("feeRate")?.Value, out var t))
                FeeRate = t;
            if (decimal.TryParse(FeeS = element.Element("fee")?.Value, out var y))
                Fee = y;
            if (decimal.TryParse(RemainQtyS = element.Element("remainQty")?.Value, out var g))
                RemainQty = g;
            if (OrderTypeTryParse(TypeS = element.Element("ask")?.Value, out var f))
                Type = f;
        }

        /// <summary>
        /// 통화
        /// 기본값: BTC
        /// </summary>
        public string Currency { get; set; } = "BTC";
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
        /// <summary>
        /// 남은 수량
        /// </summary>
        public decimal RemainQty { get; }
        /// <summary>
        /// 남은 수량 문자열
        /// </summary>
        public string RemainQtyS { get; }
        /// <summary>
        /// 수수료? 세금?
        /// </summary>
        public decimal Fee { get; }
        /// <summary>
        /// 금액 수수료?
        /// </summary>
        public string FeeS { get; }

        public static bool OrderTypeTryParse(string s, out CoinoneOrderTypes type) { //이거 뭔가 문제 있음
            type = 0;
            if (s is null) return false;
            s = s.ToLower();
            if (s == "ask") type = CoinoneOrderTypes.Sell;
            else if (s == "bid") type = CoinoneOrderTypes.Buy;
            else return false;
            return true;
        }

        public static string OrderTypeToString(CoinoneOrderTypes type) => type switch {
            CoinoneOrderTypes.Sell => "ask",
            CoinoneOrderTypes.Buy => "bid",
            _ => ""
        };
    }

    /// <summary>
    /// 주문 타입
    /// </summary>
    public enum CoinoneOrderTypes {
        /// <summary>
        /// 매도
        /// </summary>
        Sell,
        /// <summary>
        /// 매수
        /// </summary>
        Buy
    }
}
