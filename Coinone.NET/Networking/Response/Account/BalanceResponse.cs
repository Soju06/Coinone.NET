using Soju06.Collections;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Account {
    /// <summary>
    /// 잔액
    /// </summary>
    public class CoinoneAccountBalanceResponse : CoinoneResponseBase {
        internal CoinoneAccountBalanceResponse(XElement element) : base(element) {
            var wallets = element.Element("normalWallets");
            if (wallets is not null && wallets.HasElements) {
                Wallets = new();
                foreach (var item in wallets.Elements())
                    Wallets.Add(new(item));
                Wallets.WriteEnd();
            }
            Balances = new();
            foreach (var item in element.Elements()) {
                if (item.Name.LocalName is "result"
                    or "errorCode" or "normalWallets")
                    continue;
                if (item.Name == "krw") KRW = new(item);
                else if (item.Name == "btc") BTC = new(item);
                else Balances.Add(new(item));
            }
            Balances.WriteEnd();
        }

        /// <summary>
        /// 지갑s
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
    }

    /// <summary>
    /// 잔고
    /// </summary>
    public class CoinoneAccountBalanceBalanceInfo {
        internal CoinoneAccountBalanceBalanceInfo(XElement element) {
            if ((BalanceS = element.Element("balance")?.Value) is not null
                && decimal.TryParse(BalanceS, out var r))
                Balance = r;
            if ((AvailS = element.Element("avail")?.Value) is not null
                && decimal.TryParse(AvailS, out var w))
                Avail = w;
        }

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
    }

    /// <summary>
    /// wallets
    /// </summary>
    public class CoinoneAccountBalanceNormalWallets : LockableList<CoinoneAccountBalanceNormalWallet> {
        internal void WriteEnd() =>
            Lock();
    }

    /// <summary>
    /// BTC Normal wallet information
    /// </summary>
    public class CoinoneAccountBalanceNormalWallet {
        internal CoinoneAccountBalanceNormalWallet(XElement element) {
            Label = element.Element("label")?.Value;
            if ((BalanceString = element.Element("balance")?.Value) is not null 
                && decimal.TryParse(BalanceString, out var r))
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
        public string BalanceString { get; internal set; }
    }
}
