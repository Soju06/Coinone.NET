using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Account {
    /// <summary>
    /// 가상 계좌 정보
    /// </summary>
    public class CoinoneVirtualAccountResponse : CoinoneResponseBase {
        public CoinoneVirtualAccountResponse(XElement element) : base(element) {
            if (element is null) return;
            Depositor = element.Element("depositor")?.Value;
            AccountNumber = element.Element("accountNumber")?.Value;
            BankName = element.Element("bankName")?.Value;
        }

        /// <summary>
        /// 예금주
        /// </summary>
        public string Depositor { get; set; }
        /// <summary>
        /// 계좌 번호
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// 은행 이름
        /// </summary>
        public string BankName { get; set; }

        public override string ToString() => $"{Depositor} {AccountNumber} {BankName}";
    }
}
