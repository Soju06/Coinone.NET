using CoinoneNET.Exception;
using Soju06;
using Soju06.Web.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CoinoneNET.Networking.Response {
    /// <summary>
    /// 응답 빌더
    /// </summary>
    public class CoinoneResponseBuilder {
        internal CoinoneResponseBuilder(CoinoneAPIVersion _) { 

        }

        /// <summary>
        /// 응답을 디코딩합니다.
        /// </summary>
        /// <typeparam name="Resopnse">응답 개체</typeparam>
        /// <param name="json">응답</param>
        /// <returns>응답 개체</returns>
        public Resopnse DecodeResponse<Resopnse>(string json) 
            where Resopnse : CoinoneResponseBase {
            var root = JsonUtility.CreateReaderDocument(json)?.Root;
            if (root is null) throw new CoinoneResponseDecodeFaliedException("root is null");
            return typeof(Resopnse).New<Resopnse>(root);
        }

        /// <summary>
        /// 응답을 비동기로 디코딩합니다.
        /// </summary>
        /// <typeparam name="Resopnse">응답 개체</typeparam>
        /// <param name="content">서버 응답</param>
        /// <returns>응답 개체</returns>
        public async Task<Resopnse> DecodeResponseAsync<Resopnse>(HttpContent content) 
            where Resopnse : CoinoneResponseBase =>
            DecodeResponse<Resopnse>(await content.ReadAsStringAsync());
    }
}
