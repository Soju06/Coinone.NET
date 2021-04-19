using Soju06.Expansion;
using Soju06.Web.Json;
using System.Security;
using System.Security.Cryptography;
using System;
using Soju06;
using System.Net.Http;

namespace CoinoneNET.Networking.Request {
    public class CoinoneRequestBuilder {
        /// <summary>
        /// 시간
        /// </summary>
        public uint NonceTimeLimit = 2000;

        public Uri RequestUri { get; }

        public CoinoneRequestBuilder() {
            RequestUri = Coinone.APIServerURI.Join("/v2/");
        }

        /// <summary>
        /// Json으로 요청 오브젝트를 직렬화 합니다.
        /// </summary>
        /// <typeparam name="Request">요청 오브젝트</typeparam>
        /// <param name="request">요청 오브젝트</param>
        /// <returns>Json으로 인코딩된 요청 오브젝트</returns>
        public string JsonEncodingPayload<Request>(Request request) where Request : CoinoneSerializableRequestBase {
            return JsonUtility.CreateWriterString(request.Serialize());
        }

        /// <summary>
        /// 인코딩된 페이로드를 만듭니다
        /// </summary>
        /// <typeparam name="Request">요청 오브젝트</typeparam>
        /// <param name="request">요청 오브젝트</param>
        /// <returns>인코딩된 페이로드</returns>
        public string EncodingPayload<Request>(Request request) where Request : CoinoneSerializableRequestBase {
            return JsonEncodingPayload(request).Encode().EncodeBase64();
        }

        /// <summary>
        /// 서명을 만듭니다
        /// </summary>
        /// <param name="secureKey">보안 키</param>
        /// <param name="encoded_payload">인코딩된 페이로드</param>
        /// <returns>서명</returns>
        public string GenerateSignature(SecureString secureKey, string encoded_payload) {
            using (var sha = new HMACSHA512(secureKey.GetString().Encode()))
                return BitConverter.ToString(sha.ComputeHash(encoded_payload.Encode())).Replace("-", "").ToLower();
        }

        /// <summary>
        /// 요청을 만듭니다
        /// </summary>
        /// <param name="func">요청 매소드</param>
        /// <param name="encoded_payload">인코딩된 페이로드</param>
        /// <param name="encoded_signature">인코딩된 서명</param>
        /// <returns>요청 메시지</returns>
        public HttpRequestMessage GenerateRequest(string func, string encoded_payload, string encoded_signature) {
            var r = new HttpRequestMessage {
                RequestUri = RequestUri.Join(func),
                Method = HttpMethod.Post
            };
            r.Headers.Add("X-COINONE-PAYLOAD", encoded_payload);
            r.Headers.Add("X-COINONE-SIGNATURE", encoded_signature);
            return r;
        }


        public HttpRequestMessage GenerateRequest<Request>(string func, Request request, SecureString secretKey = null) 
            where Request : CoinoneSerializableRequestBase {
            var r = new HttpRequestMessage {
                RequestUri = RequestUri.Join(func),
                Method = HttpMethod.Post
            };
            var ep = EncodingPayload(request);
            r.Headers.Add("X-COINONE-PAYLOAD", ep);
            if(secretKey is not null)
                r.Headers.Add("X-COINONE-SIGNATURE", GenerateSignature(secretKey, ep));
            return r;
        }
    }
}
