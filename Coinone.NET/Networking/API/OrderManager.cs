using CoinoneNET.Networking.Request.Order;
using CoinoneNET.Networking.Response;
using CoinoneNET.Networking.Response.Order;
using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace CoinoneNET.Networking.API {
    /// <summary>
    /// 주문
    /// </summary>
    public class CoinoneOrderResponseRequestManager : CoinoneResponseRequestManagerBase {
        internal CoinoneOrderResponseRequestManager() : base(null) {

        }

        /// <summary>
        /// 주문 취소
        /// </summary>
        /// <param name="request">요청 옵션</param>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>기본 응답</returns>
        public async Task<CoinoneDefaultResponse> CancelOrder(CoinoneCancelOrderRequest request,
            SecureString accessToken = null, CancellationToken? token = null) {
            if (request is null) throw new ArgumentNullException("request");
            if (request?.AccessToken is null) request.AccessToken = (accessToken ?? AccessToken)?.Copy();
            return await Manager.ResponseRequest<CoinoneDefaultResponse, CoinoneCancelOrderRequest>("order/cancel/", request, secretKey ?? SecretKey, token);
        }

        /// <summary>
        /// 매수
        /// </summary>
        /// <param name="request">요청 옵션</param>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>주문 결과</returns>
        public async Task<CoinoneLimitOrderResponse> LimitBuy(CoinoneLimitOrderRequest request,
            SecureString accessToken = null, CancellationToken? token = null) {
            if (request is null) throw new ArgumentNullException("request");
            if (request?.AccessToken is null) request.AccessToken = (accessToken ?? AccessToken)?.Copy();
            return await Manager.ResponseRequest<CoinoneLimitOrderResponse, CoinoneLimitOrderRequest>("order/limit_buy/", request, secretKey ?? SecretKey, token);
        }

        /// <summary>
        /// 매도
        /// </summary>
        /// <param name="request">요청 옵션</param>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>주문 결과</returns>
        public async Task<CoinoneLimitOrderResponse> LimitSell(CoinoneLimitOrderRequest request,
            SecureString accessToken = null, CancellationToken? token = null) {
            if (request is null) throw new ArgumentNullException("request");
            if (request?.AccessToken is null) request.AccessToken = (accessToken ?? AccessToken)?.Copy();
            return await Manager.ResponseRequest<CoinoneLimitOrderResponse, CoinoneLimitOrderRequest>("order/limit_sell/", request, secretKey ?? SecretKey, token);
        }

        /// <summary>
        /// 내 주문s
        /// </summary>
        /// <param name="currency">코인 코드</param>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>주문s</returns>
        public async Task<CoinoneMyLimitOrdersResponse> MyLimitOrders(string currency = "BTC",
            SecureString accessToken = null, CancellationToken? token = null) {
            return await Manager.ResponseRequest<CoinoneMyLimitOrdersResponse, CoinoneMyOrdersRequest>("order/limit_sell/",
                new(accessToken ?? AccessToken) { Currency = currency }, secretKey ?? SecretKey, token);
        }

        /// <summary>
        /// 내 체결된 주문s
        /// </summary>
        /// <param name="currency">코인 코드</param>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>체결된 주문s</returns>
        public async Task<CoinoneMyCompleteOrdersResponse> MyCompleteOrders(string currency = "BTC",
            SecureString accessToken = null, CancellationToken? token = null) {
            return await Manager.ResponseRequest<CoinoneMyCompleteOrdersResponse, CoinoneMyOrdersRequest>("order/complete_orders/",
                new(accessToken ?? AccessToken) { Currency = currency }, secretKey ?? SecretKey, token);
        }

        /// <summary>
        /// 내 주문 정보 v1
        /// </summary>
        /// <param name="currency">코인 코드</param>
        /// <param name="orderID">주문 ID</param>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>주문 정보</returns>
        public async Task<CoinoneMyOrderInformationV1Response> MyOrderInformationV1(string orderID, string currency = "BTC",
            SecureString accessToken = null, CancellationToken? token = null) {
            return await Manager.ResponseRequest<CoinoneMyOrderInformationV1Response, CoinoneMyOrdersInformationRequest>("order/order_info/",
                new(accessToken ?? AccessToken, orderID) { Currency = currency }, secretKey ?? SecretKey, token);
        }

        /// <summary>
        /// 내 주문 정보 v2
        /// </summary>
        /// <param name="currency">코인 코드</param>
        /// <param name="orderID">주문 ID</param>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>주문 정보</returns>
        public async Task<CoinoneMyOrderInformationV2Response> MyOrderInformationV2(string orderID, string currency = "BTC",
            SecureString accessToken = null, CancellationToken? token = null) {
            return await Manager.ResponseRequest<CoinoneMyOrderInformationV2Response, CoinoneMyOrdersInformationRequest>("order/query_order/",
                new(accessToken ?? AccessToken, orderID) { Currency = currency }, secretKey ?? SecretKey, token);
        }
    }
}
