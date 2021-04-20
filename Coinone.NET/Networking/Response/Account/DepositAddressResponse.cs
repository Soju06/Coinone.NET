using Soju06;
using Soju06.Collections;
using Soju06.Expansion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Account {
    /// <summary>
    /// 입금? 예금? 주소
    /// </summary>
    public class CoinoneDepositAddressResponse : CoinoneResponseBase {
        public CoinoneDepositAddressResponse(XElement element) : base(element) {
            WalletAddresses = new(element.Element("walletAddress"));
        }

        /// <summary>
        /// 지갑 주소s
        /// </summary>
        public CoinoneWalletAddresses WalletAddresses { get; private set; }
    }

    /// <summary>
    /// 지갑 주소s
    /// </summary>
    public class CoinoneWalletAddresses : LockableList<CoinoneWalletAddressInfo> { 
        internal CoinoneWalletAddresses() { 
            
        }

        internal CoinoneWalletAddresses(XElement element) {
            if(element is not null && element.HasElements)
                foreach (var item in element.Elements())
                    Add(new(item));
            Lock();
        }

        /// <summary>
        /// 보유 잔고만 필터링 합니다.
        /// lan(walletAddress.CoinCode) > 0 && lan(walletAddress.Address > 0)
        /// </summary>
        public CoinoneWalletAddresses FilterHoldingWallets() {
            var r = this.Filter((s) => !string.IsNullOrWhiteSpace(s.CoinCode)
            && !string.IsNullOrWhiteSpace(s.Address))
                .ToOList(new CoinoneWalletAddresses());
            r.Lock(); return r;
        }
    }

    /// <summary>
    /// 지갑 주소 정보
    /// </summary>
    public class CoinoneWalletAddressInfo {
        internal CoinoneWalletAddressInfo(XElement element) {
            CoinCode = element.Name.LocalName;
            Address = element.Value;
        }

        /// <summary>
        /// 코인명
        /// </summary>
        public string CoinCode { get; set; }

        /// <summary>
        /// 지갑 주소
        /// </summary>
        public string Address { get; set; }

        public override string ToString() => $"{CoinCode} {Address}";
    }
}
