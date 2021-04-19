using CoinoneNET.Networking.Request;
using Soju06;
using Soju06.API;
using Soju06.Expansion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CoinoneNETTest {
    class Program {
        static void Main(string[] args) {
            CoinoneRequestBuilder builder = new();
            SecureString secretKey = new(), accessToken = new();
            var d = File.ReadAllLines(@"D:\Users\soju_\Downloads\sdfg.txt"); // 내 API키는 소중해
            accessToken.AppendString(d[0]);
            secretKey.AppendString(d[1]);
            var e = builder.EncodingPayload(new CoinoneDefaultRequest(accessToken));
            var r = builder.GenerateRequest("account/balance/", e, builder.GenerateSignature(secretKey, e));
            Task.Run(async () => {
                var f = await APIClient.GetClient().SendAsync(r);
                Console.WriteLine(await f.Content.ReadAsStringAsync());
            }).Wait();
            Console.Read();
        }
    }
}
