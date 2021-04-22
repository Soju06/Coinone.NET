using Soju06;
using System;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Order {
    /// <summary>
    /// 내 오더 정보
    /// </summary>
    public class CoinoneMyOrderInformationV2Response : CoinoneResponseBase {
        public CoinoneMyOrderInformationV2Response(XElement element) : base(element) {
            if (element is null || !element.HasElements) return;
            OrderId = element.Element("orderId")?.Value;
            BaseCurrency = element.Element("baseCurrency")?.Value;
            TargetCurrency = element.Element("targetCurrency")?.Value;
            if (decimal.TryParse(PriceS = element.Element("price")?.Value, out var e))
                Price = e;
            if (decimal.TryParse(AverageExecutedPriceS = element.Element("averageExecutedPrice")?.Value, out var a))
                AverageExecutedPrice = a;
            if (decimal.TryParse(OriginalQtyS = element.Element("originalQty")?.Value, out var w))
                OriginalQty = w;
            if (decimal.TryParse(ExecutedQtyS = element.Element("executedQty")?.Value, out var c))
                ExecutedQty = c;
            if (decimal.TryParse(CanceledQtyS = element.Element("canceledQty")?.Value, out var g))
                CanceledQty = g;
            if (decimal.TryParse(RemainQtyS = element.Element("remainQty")?.Value, out var z))
                RemainQty = z;
            if (MyOrderV2StatusTryParse(element?.Element("status")?.Value, out var h))
                Status = h;
            if (CoinoneMyOrderInfo.OrderTypeTryParse(SideS = element.Element("side")?.Value, out var f))
                Side = f;
            if (long.TryParse(OrderedAtS = element.Element("orderedAt")?.Value, out var r))
                OrderedAt = r.UnixTimestampToDateTime();
            if (long.TryParse(UpdatedAtS = element.Element("updatedAt")?.Value, out var s))
                UpdatedAt = s.UnixTimestampToDateTime();
            if (decimal.TryParse(FeeRateS = element.Element("feeRate")?.Value, out var t))
                FeeRate = t;
            if (decimal.TryParse(FeeS = element.Element("fee")?.Value, out var y))
                Fee = y;
        }

        /// <summary>
        /// 오더 상태
        /// </summary>
        public CoinoneMyOrderV2Status Status { get; }
        /// <summary>
        /// 코인 코드
        /// </summary>
        public string BaseCurrency { get; set; }
        /// <summary>
        /// 코인 코드
        /// </summary>
        public string TargetCurrency { get; set; }
        /// <summary>
        /// 주문 ID
        /// </summary>
        public string OrderId { get; }
        /// <summary>
        /// 주문 시간
        /// </summary>
        public DateTime OrderedAt { get; }
        /// <summary>
        /// 주문 시간 문자열
        /// </summary>
        public string OrderedAtS { get; }
        /// <summary>
        /// 업데이트된 시간
        /// </summary>
        public DateTime UpdatedAt { get; }
        /// <summary>
        /// 업데이트된 시간 문자열
        /// </summary>
        public string UpdatedAtS { get; }
        /// <summary>
        /// 가격
        /// </summary>
        public decimal Price { get; }
        /// <summary>
        /// 가격 문자열
        /// </summary>
        public string PriceS { get; }
        /// <summary>
        /// 평균 체결 가격
        /// </summary>
        public decimal AverageExecutedPrice { get; }
        /// <summary>
        /// 평균 체결 가격 문자열
        /// </summary>
        public string AverageExecutedPriceS { get; }
        /// <summary>
        /// 수량
        /// </summary>
        public decimal OriginalQty { get; }
        /// <summary>
        /// 수량 문자열
        /// </summary>
        public string OriginalQtyS { get; }
        /// <summary>
        /// 체결된 수량
        /// </summary>
        public decimal ExecutedQty { get; }
        /// <summary>
        /// 체결된 수량 문자열
        /// </summary>
        public string ExecutedQtyS { get; }
        /// <summary>
        /// 취소된 수량
        /// </summary>
        public decimal CanceledQty { get; }
        /// <summary>
        /// 취소된 수량 문자열
        /// </summary>
        public string CanceledQtyS { get; }
        /// <summary>
        /// 남은 수량
        /// </summary>
        public decimal RemainQty { get; }
        /// <summary>
        /// 남은 수량 문자열
        /// </summary>
        public string RemainQtyS { get; }
        /// <summary>
        /// 주문 타입
        /// </summary>
        public CoinoneOrderTypes Side { get; }
        /// <summary>
        /// 주문 타입 문자열
        /// </summary>
        public string SideS { get; }
        /// <summary>
        /// 수수료? 세금?
        /// </summary>
        public decimal FeeRate { get; }
        /// <summary>
        /// 수수료? 세금? 문자열
        /// </summary>
        public string FeeRateS { get; }
        /// <summary>
        /// 수수료? 세금?
        /// </summary>
        public decimal Fee { get; }
        /// <summary>
        /// 금액 수수료?
        /// </summary>
        public string FeeS { get; }

        public static string MyOrderV2StatusToString(CoinoneMyOrderV2Status status) => status switch {
            CoinoneMyOrderV2Status.Live => "live",
            CoinoneMyOrderV2Status.Failed => "failed",
            CoinoneMyOrderV2Status.PartiallyFilled => "partially_filled",
            CoinoneMyOrderV2Status.PartiallyCanceled => "partially_canceled",
            CoinoneMyOrderV2Status.Canceled => "canceled",
            _ => null
        };

        public static bool MyOrderV2StatusTryParse(string s, out CoinoneMyOrderV2Status status) =>
            (status = s switch {
                "live" => CoinoneMyOrderV2Status.Live,
                "failed" => CoinoneMyOrderV2Status.Failed,
                "partially_filled" => CoinoneMyOrderV2Status.PartiallyFilled,
                "partially_canceled" => CoinoneMyOrderV2Status.PartiallyCanceled,
                "canceled" => CoinoneMyOrderV2Status.Canceled,
                _ => CoinoneMyOrderV2Status.None
            }) != CoinoneMyOrderV2Status.None;

        public override string ToString() => $"{Side} {Price}/{OriginalQty} AEP {AverageExecutedPrice} Canceled Qty {CanceledQty} Remain Qty {RemainQty} Fee {Fee}";
    }

    /// <summary>
    /// 내 주문 상태
    /// </summary>
    public enum CoinoneMyOrderV2Status {
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
        PartiallyFilled,
        /// <summary>
        /// 일부 취소됨?
        /// </summary>
        PartiallyCanceled,
        /// <summary>
        /// 취소됨
        /// </summary>
        Canceled
    }
}
