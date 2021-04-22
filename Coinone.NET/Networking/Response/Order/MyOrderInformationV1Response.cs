using Soju06;
using System;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Order {
    /// <summary>
    /// 내 오더 정보
    /// </summary>
    public class CoinoneMyOrderInformationV1Response : CoinoneResponseBase {
        public CoinoneMyOrderInformationV1Response(XElement element) : base(element) {
            OrderInfo = new(element?.Element("info"));
            if (MyOrderV1StatusTryParse(element?.Element("status")?.Value, out var r))
                Status = r;
        }

        /// <summary>
        /// 오더 정보
        /// </summary>
        public CoinoneMyOrderInfo OrderInfo { get; }

        /// <summary>
        /// 오더 상태
        /// </summary>
        public CoinoneMyOrderV1Status Status { get; }

        public static string MyOrderV1StatusToString(CoinoneMyOrderV1Status status) => status switch {
            CoinoneMyOrderV1Status.Live => "live",
            CoinoneMyOrderV1Status.Failed => "failed",
            CoinoneMyOrderV1Status.PartiallyFilled => "partially_filled",
            _ => null
        };

        public static bool MyOrderV1StatusTryParse(string s, out CoinoneMyOrderV1Status status) =>
            (status = s switch {
                "live" => CoinoneMyOrderV1Status.Live,
                "failed" => CoinoneMyOrderV1Status.Failed,
                "partially_filled" => CoinoneMyOrderV1Status.PartiallyFilled,
                _ => CoinoneMyOrderV1Status.None
            }) != CoinoneMyOrderV1Status.None;

        public override string ToString() => $"{Status} | {OrderInfo}";
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
            if (OrderTypeTryParse(TypeS = element.Element("type")?.Value, out var f))
                Type = f;
        }

        /// <summary>
        /// 코인 코드
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

        public static bool OrderTypeTryParse(string s, out CoinoneOrderTypes type) =>
            (type = s?.ToLower() switch {
                "ask" => CoinoneOrderTypes.Sell,
                "bid" => CoinoneOrderTypes.Buy,
                _ => CoinoneOrderTypes.None
            }) != CoinoneOrderTypes.None;

        public static string OrderTypeToString(CoinoneOrderTypes type) => type switch {
            CoinoneOrderTypes.Sell => "ask",
            CoinoneOrderTypes.Buy => "bid",
            _ => null
        };

        public override string ToString() => $"{Type} {Price}/{Qty} Remain Qty {RemainQty} Fee {Fee}";
    }

    /// <summary>
    /// 내 주문 상태
    /// </summary>
    public enum CoinoneMyOrderV1Status {
        /// <summary>
        /// 알수 없는
        /// </summary>
        None,
        /// <summary>
        /// 살아있는
        /// </summary>
        Live,
        /// <summary>
        /// 실패
        /// </summary>
        Failed,
        /// <summary>
        /// 일부 체결됨?
        /// </summary>
        PartiallyFilled
    }

    /// <summary>
    /// 주문 타입
    /// </summary>
    public enum CoinoneOrderTypes {
        /// <summary>
        /// 알수 없는
        /// </summary>
        None,
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
