using System;

namespace CoinoneNET.Exception {
    [Serializable]
    public class CoinoneNetworkResponseRequestException : System.Exception {
        public CoinoneNetworkResponseRequestException() { }
        public CoinoneNetworkResponseRequestException(string message) : base(message) { }
        public CoinoneNetworkResponseRequestException(string message, System.Exception inner) : base(message, inner) { }
    }
}
