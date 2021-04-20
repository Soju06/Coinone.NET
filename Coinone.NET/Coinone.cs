using CoinoneNET.Networking;

namespace CoinoneNET {
    /// <summary>
    /// 코인원 V2 API
    /// </summary>
    public sealed partial class Coinone : CoinoneNetwork {
        /// <summary>
        /// 코인원 V2 API
        /// </summary>
        public Coinone(CoinoneAPIVersion version = CoinoneAPIVersion.V2) 
            : base(version) {

        }
    }
}
