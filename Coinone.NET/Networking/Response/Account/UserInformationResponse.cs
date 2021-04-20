using Soju06.Collections;
using System;
using System.Xml.Linq;

namespace CoinoneNET.Networking.Response.Account
{
    public class CoinoneUserInformationResponse : CoinoneResponseBase {
        public CoinoneUserInformationResponse(XElement element) : base(element) {
            if (element is null) return;
            UserInformation = new(element.Element("userInfo"));
        }

        /// <summary>
        /// 사용자 정보
        /// </summary>
        public CoinoneUserInformation UserInformation { get; private set; }
    }

    /// <summary>
    /// 사용자 정보
    /// </summary>
    public class CoinoneUserInformation {
        internal CoinoneUserInformation(XElement element) {
            if (element is null) return;
            VirtualAccount = new(element.Element("virtualAccountInfo"));
            Mobile = new(element.Element("mobileInfo"));
            Bank = new(element.Element("bankInfo"));
            Email = new(element.Element("emailInfo"));
            FeeRatesI = new(element.Element("feeRate"));
            SecurityLevelS = element.Element("securityLevel")?.Value;
            if (SecurityLevelS is not null && 
                Enum.TryParse(SecurityLevelS, out CoinoneSecurityLevel r))
                    SecurityLevel = r;
        }

        /// <summary>
        /// 가상 계좌 정보
        /// </summary>
        public VirtualAccountInfo VirtualAccount { get; private set; }
        /// <summary>
        /// 모바일 정보
        /// </summary>
        public MobileInfo Mobile { get; private set; }
        /// <summary>
        /// 은행 정보
        /// </summary>
        public BankInfo Bank { get; private set; }
        /// <summary>
        /// 이메일 정보
        /// </summary>
        public EmailInfo Email { get; private set; }
        /// <summary>
        /// 보안 레벨
        /// </summary>
        public CoinoneSecurityLevel SecurityLevel { get; private set; }
        /// <summary>
        /// 보안 레벨 문자열
        /// </summary>
        public string SecurityLevelS { get; private set; }
        /// <summary>
        /// 수수료 정보
        /// </summary>
        public FeeRates FeeRatesI { get; private set; }

        /// <summary>
        /// 가상 계좌 정보
        /// </summary>
        public class VirtualAccountInfo {
            internal VirtualAccountInfo(XElement element) {
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

        /// <summary>
        /// 이뭔 혼종이여..
        /// 전화번호 정보?
        /// 모바일 지점 정보?
        /// </summary>
        public class MobileInfo {
            internal MobileInfo(XElement element) {
                if (element is null) return;
                UserName = element.Element("userName")?.Value;
                PhoneNumber = element.Element("phoneNumber")?.Value;
                PhoneCorp = element.Element("phoneCorp")?.Value;
                if(bool.TryParse(IsAuthenticatedS = element.Element("isAuthenticated")?
                    .Value, out var r))
                    IsAuthenticated = r;
            }

            /// <summary>
            /// 이름
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 전화 번호
            /// </summary>
            public string PhoneNumber { get; set; }
            /// <summary>
            /// PhoneCorp
            /// </summary>
            public string PhoneCorp { get; set; }
            /// <summary>
            /// 인증됨
            /// </summary>
            public bool IsAuthenticated { get; set; }
            /// <summary>
            /// 인증됨
            /// </summary>
            public string IsAuthenticatedS { get; set; }

            public override string ToString() => $"{UserName} {PhoneCorp} {PhoneNumber}";
        }

        /// <summary>
        /// 은행 정보
        /// 재발 개체 분산화 시키지마... 만들기 귀찮자나
        /// </summary>
        public class BankInfo {
            internal BankInfo(XElement element) {
                if (element is null) return;
                Depositor = element.Element("depositor")?.Value;
                BankCode = element.Element("bankCode")?.Value;
                AccountNumber = element.Element("accountNumber")?.Value;
                if (bool.TryParse(IsAuthenticatedS = element.Element("isAuthenticated")?
                    .Value, out var r))
                    IsAuthenticated = r;
            }

            /// <summary>
            /// 예금주
            /// </summary>
            public string Depositor { get; set; }
            /// <summary>
            /// 은행 코드
            /// </summary>
            public string BankCode { get; set; }
            /// <summary>
            /// 계좌 번호
            /// </summary>
            public string AccountNumber { get; set; }
            /// <summary>
            /// 인증됨
            /// </summary>
            public bool IsAuthenticated { get; set; }
            /// <summary>
            /// 인증됨
            /// </summary>
            public string IsAuthenticatedS { get; set; }

            public override string ToString() => $"{Depositor} {AccountNumber} {BankCode}";
        }

        /// <summary>
        /// 은행 정보
        /// 재발 개체 분산화 시키지마... 만들기 귀찮자나
        /// </summary>
        public class EmailInfo {
            internal EmailInfo(XElement element) {
                if (element is null) return;
                Email = element.Element("email")?.Value;
                if (bool.TryParse(IsAuthenticatedS = element.Element("isAuthenticated")?
                    .Value, out var r))
                    IsAuthenticated = r;
            }

            /// <summary>
            /// email
            /// </summary>
            public string Email { get; set; }
            /// <summary>
            /// 인증됨
            /// </summary>
            public bool IsAuthenticated { get; set; }
            /// <summary>
            /// 인증됨
            /// </summary>
            public string IsAuthenticatedS { get; set; }

            public override string ToString() => $"{Email}";
        }

        /// <summary>
        /// 수수료s
        /// </summary>
        public class FeeRates : LockableList<FeeRate> {
            internal FeeRates(XElement element) {
                if(element is not null && element.HasElements)
                    foreach (var item in element.Elements())
                        Add(new(item));
                Lock();
            }
        }

        public class FeeRate {
            internal FeeRate(XElement element) {
                if (element is null) return;
                CoinCode = element.Name.LocalName;
                Maker = element.Element("maker")?.Value;
                Taker = element.Element("taker")?.Value;
            }

            /// <summary>
            /// 코인명
            /// </summary>
            public string CoinCode { get; set; }
            /// <summary>
            /// 오더북 체결 수수료
            /// </summary>
            public string Maker { get; set; }
            /// <summary>
            /// 즉시 체결 수수료
            /// </summary>
            public string Taker { get; set; }

            public override string ToString() => $"{CoinCode} Maker {Maker} Taker {Taker}";
        }
    }

    /// <summary>
    /// 어카운트 보안 레벨
    /// 추정입니다. 정확하지 않을수 있음
    /// </summary>
    public enum CoinoneSecurityLevel {
        None = 0,
        PhoneAuthenticated = 1,
        Authenticated = 2,
        BankAuthenticated = 3,
        EmailAuthenticated = 4,
    }
}
