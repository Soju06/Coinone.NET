using CoinoneNET.Networking.Request;
using CoinoneNET.Networking.Response.Account;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace CoinoneNET.Networking.API {
    /// <summary>
    /// 계좌
    /// </summary>
    public class CoinoneAccountResponseRequestManager: CoinoneResponseRequestManagerBase {
        internal CoinoneAccountResponseRequestManager(): base(null) { 
            
        }

        /// <summary>
        /// 잔고
        /// </summary>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>잔고</returns>
        public async Task<CoinoneAccountBalanceResponse> GetBalance(
            SecureString accessToken = null, CancellationToken? token = null) =>
                await Manager.ResponseRequest<CoinoneAccountBalanceResponse,
                    CoinoneDefaultRequest>("account/balance/", new(accessToken ?? AccessToken), secretKey ?? SecretKey, token);

        /// <summary>
        /// 입금? 예금? 주소
        /// </summary>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>입금? 예금? 주소</returns>
        public async Task<CoinoneDepositAddressResponse> GetDepositAddress(
            SecureString accessToken = null, CancellationToken? token = null) =>
                await Manager.ResponseRequest<CoinoneDepositAddressResponse,
                    CoinoneDefaultRequest>("account/deposit_address/", new(accessToken ?? AccessToken), secretKey ?? SecretKey, token);

        /// <summary>
        /// 사용자 정보
        /// </summary>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>사용자 정보</returns>
        public async Task<CoinoneUserInformationResponse> GetUserInformation(
            SecureString accessToken = null, CancellationToken? token = null) =>
                await Manager.ResponseRequest<CoinoneUserInformationResponse,
                    CoinoneDefaultRequest>("account/user_info/", new(accessToken ?? AccessToken), secretKey ?? SecretKey, token);

        /// <summary>
        /// 가상 계좌
        /// </summary>
        /// <param name="accessToken">엑세스 토큰 null일시 상속된 토큰을 사용함</param>
        /// <param name="token">Task Cancel Token</param>
        /// <returns>가상 계좌</returns>
        public async Task<CoinoneVirtualAccountResponse> GetVirtualAccount(
            SecureString accessToken = null, CancellationToken? token = null) =>
                await Manager.ResponseRequest<CoinoneVirtualAccountResponse,
                    CoinoneDefaultRequest>("account/virtual_account/", new(accessToken ?? AccessToken), secretKey ?? SecretKey, token);
    }
}
