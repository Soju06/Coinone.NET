namespace CoinoneNET.Exception {
    public class CoinoneResponseErrorExceptionException : System.Exception {
        public CoinoneResponseErrorExceptionException() { }
        public CoinoneResponseErrorExceptionException(string message) : base(message) { }
        public CoinoneResponseErrorExceptionException(string message, System.Exception inner) : base(message, inner) { }
    }
}
