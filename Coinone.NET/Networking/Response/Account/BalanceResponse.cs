using Soju06;
using Soju06.Collections;
using Soju06.Expansion;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Account {
    /// <summary>
    /// 잔액
    /// </summary>
    public class CoinoneAccountBalanceResponse : CoinoneResponseBase {
        public CoinoneAccountBalanceResponse(XElement element) : base(element) {
            Wallets = new(element.Element("normalWallets"));
            Balances = new();
            if(element.HasElements)
                foreach (var item in element.Elements()) {
                    if (item.Name.LocalName is "result"
                        or "errorCode" or "normalWallets")
                        continue;
                    if (item.Name == "krw") KRW = new(item);
                    else if (item.Name == "btc") BTC = new(item);
                    else Balances.Add(new(item));
                }
            Balances.WriteEnd();
            Balances.Filter((CoinoneAccountBalanceBalanceInfo d) => false);
        }

        /// <summary>
        /// 지갑s
        /// 이 속성은 null이 될수 있습니다
        /// </summary>
        public CoinoneAccountBalanceNormalWallets Wallets { get; private set; }

        /// <summary>
        /// 잔고s 
        /// 원화 잔고, 비트코인 잔고는 해당되지 않습니다.
        /// </summary>
        public CoinoneAccountBalanceBalances Balances { get; private set; }

        /// <summary>
        /// 원화 잔고
        /// </summary>
        public CoinoneAccountBalanceBalanceInfo KRW { get; private set; }

        /// <summary>
        /// 비트코인 잔고
        /// </summary>
        public CoinoneAccountBalanceBalanceInfo BTC { get; private set; }
    }

    /// <summary>
    /// 잔고s
    /// </summary>
    public class CoinoneAccountBalanceBalances : LockableList<CoinoneAccountBalanceBalanceInfo> {
        internal void WriteEnd() =>
            Lock();

        /// <summary>
        /// 보유 잔고만 필터링 합니다.
        /// balance.Avail > 0 && balance.Balance > 0
        /// </summary>
        public CoinoneAccountBalanceBalances FilterHoldingBalances() {
            var r = this.Filter((s) => s.Avail > 0 && s.Balance > 0)
                .ToOList(new CoinoneAccountBalanceBalances());
            r.Lock(); return r;
        }
    }

    /// <summary>
    /// 잔고
    /// </summary>
    public class CoinoneAccountBalanceBalanceInfo {
        internal CoinoneAccountBalanceBalanceInfo(XElement element) {
            CoinCode = element.Name.LocalName;
            if ((BalanceS = element.Element("balance")?.Value) is not null
                && decimal.TryParse(BalanceS, out var r))
                Balance = r;
            if ((AvailS = element.Element("avail")?.Value) is not null
                && decimal.TryParse(AvailS, out var w))
                Avail = w;
        }

        /// <summary>
        /// 코인명
        /// </summary>
        public string CoinCode { get; set; }

        /// <summary>
        /// 손익? 이익?
        /// </summary>
        public decimal Avail { get; set; }

        /// <summary>
        /// 손익? 이익? 문자열
        /// </summary>
        public string AvailS { get; set; }

        /// <summary>
        /// 잔고? 보류 수량?
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 잔고? 보류 수량? 문자열
        /// </summary>
        public string BalanceS { get; set; }

        public override string ToString() => $"{CoinCode} {Balance} {Avail}";
    }

    /// <summary>
    /// wallets
    /// </summary>
    public class CoinoneAccountBalanceNormalWallets : LockableList<CoinoneAccountBalanceNormalWallet> {
        internal CoinoneAccountBalanceNormalWallets(XElement element) {
            if (element is not null && element.HasElements)
                foreach (var item in element.Elements())
                    Add(new(item));
            Lock();
        }
    }

    /// <summary>
    /// BTC Normal wallet information
    /// </summary>
    public class CoinoneAccountBalanceNormalWallet {
        internal CoinoneAccountBalanceNormalWallet(XElement element) {
            Label = element.Element("label")?.Value;
            if ((BalanceS = element.Element("balance")?.Value) is not null 
                && decimal.TryParse(BalanceS, out var r))
                Balance = r;
        }
        
        /// <summary>
        /// 지갑 이름
        /// </summary>
        public string Label { get; internal set; }

        /// <summary>
        /// 지갑 잔액
        /// </summary>
        public decimal Balance { get; internal set; }

        /// <summary>
        /// 지갑 잔액 문자열
        /// </summary>
        public string BalanceS { get; internal set; }

        public override string ToString() => $"{Label} {Balance}";
    }
}
