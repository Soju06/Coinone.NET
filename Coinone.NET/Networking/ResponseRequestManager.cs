using CoinoneNET.Exception;
using CoinoneNET.Networking.API;
using CoinoneNET.Networking.Request;
using CoinoneNET.Networking.Response;
using Soju06.API;
using Soju06.Task;
using System.Net.Http;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace CoinoneNET.Networking {
    /// <summary>
    /// 응답 요청 매니저
    /// </summary>
    public class CoinoneResponseRequestManager : CoinoneResponseRequestManagerBase {
        internal CoinoneResponseRequestManager(CoinoneAPIVersion version) : base(null) {
            parent = this;
            ResponseBuilder = new(version);
            RequestBuilder = new(version);
            Account = new();
            Order = new();
            SetParentManager(Account);
            SetParentManager(Order);
        }

        protected override CoinoneResponseRequestManager Manager => this;
        protected override SecureString AccessToken => accessToken;
        protected override SecureString SecretKey => secretKey;

        /// <summary>
        /// 계좌
        /// </summary>
        public CoinoneAccountResponseRequestManager Account { get; }
        /// <summary>
        /// 주문
        /// </summary>
        public CoinoneOrderResponseRequestManager Order { get; }

        private readonly AsyncLock ResponseRequestLock = new();

        /// <summary>
        /// 응답 요청
        /// </summary>
        /// <typeparam name="Response">응답</typeparam>
        /// <typeparam name="Request">요청 개체</typeparam>
        /// <param name="func">매소드</param>
        /// <param name="request">요청</param>
        /// <param name="secretKey">보안 키</param>
        /// <param name="token">엑세스 토큰</param>
        /// <returns></returns>
        public async Task<Response> ResponseRequest<Response, Request>(string func, Request request, SecureString secretKey = null, CancellationToken? token = null)
            where Response : CoinoneResponseBase where Request : CoinoneRequestBase {
            using (await ResponseRequestLock.LockAsync()) {
                token?.ThrowIfCancellationRequested();
                try {
                    var client = APIClient.GetClient();
                    using (var req = RequestBuilder.CreateRequest(func, request, secretKey)) {
                        HttpResponseMessage res = token.HasValue ?
                            await client.SendAsync(req, token.Value) : await client.SendAsync(req);
                        return await ResponseBuilder.DecodeResponseAsync<Response>(res.Content);
                    }
                } catch (System.Exception ex) {
                    throw new CoinoneNetworkResponseRequestException("failed to request a response", ex);
                }
            }
        }

        /// <summary>
        /// 요청 빌더
        /// </summary>
        public CoinoneRequestBuilder RequestBuilder { get; private set; }

        /// <summary>
        /// 응답 빌더
        /// </summary>
        public CoinoneResponseBuilder ResponseBuilder { get; private set; }
    }
}
