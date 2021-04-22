using System.Xml.Linq;

namespace CoinoneNET.Networking.Response {
    /// <summary>
    /// 기본 요청
    /// </summary>
    public class CoinoneDefaultResponse : CoinoneResponseBase {
        public CoinoneDefaultResponse(XElement element) : base(element) { 
            
        }
    }
}
