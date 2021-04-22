using CoinoneNET;
using CoinoneNET.Networking.Request;
using CoinoneNET.Networking.Request.Order;
using CoinoneNET.Networking.Response.Account;
using CoinoneNET.Networking.Response.Order;
using Soju06.Expansion;
using System;
using System.IO;
using System.Security;
using System.Threading.Tasks;

namespace CoinoneNETTest {
    class Program {
        static async Task Main(string[] _) {
            SecureString secretKey = new(), accessToken = new();
            var d = File.ReadAllLines(@"D:\Users\soju_\Downloads\sdfg.txt"); // 내 API키는 소중해
            accessToken.AppendString(d[0]);
            secretKey.AppendString(d[1]);

            var coinone = new Coinone();
            coinone.RegisterSecurityAccessToken(accessToken);
            coinone.RegisterSecuritySecretKey(secretKey);
            // 어카운트
            // 잔고 조회
            {
                var res = await coinone.Account.GetBalance();
                Console.WriteLine($"원화 {res.KRW}");
                Console.WriteLine($"비트코인 {res.BTC}");

                foreach (var item in res.Balances.FilterHoldingBalances())
                    Console.WriteLine($"{item}");
                if (res.Wallets is not null)
                    foreach (var item in res.Wallets)
                        Console.WriteLine($"{item}");
            }

            // 잔고 지갑 주소 조회
            {
                var res = await coinone.Account.GetDepositAddress();
                if (res.WalletAddresses is not null)
                    foreach (var item in res.WalletAddresses
                    .FilterHoldingWallets())
                        Console.WriteLine(item);
            }

            // 사용자 정보
            {
                var res = await coinone.Account.GetUserInformation();
                if (res.UserInformation is not null) {
                    Console.WriteLine("{0}", res.UserInformation.VirtualAccount);
                    Console.WriteLine("{0}", res.UserInformation.Bank);
                    Console.WriteLine("{0}", res.UserInformation.Mobile);
                    Console.WriteLine("Email {0}", res.UserInformation.Email);
                    Console.WriteLine("SecurityLevel {0}", res.UserInformation.SecurityLevel.ToString());
                    foreach (var item in res.UserInformation.FeeRatesI)
                        Console.Write("{0}\t", item);
                    Console.WriteLine();
                }
            }

            // 가상 계좌 조회, 
            // 사용자 정보에도 있음
            {
                var res = await coinone.Account.GetVirtualAccount();
                Console.WriteLine(res);
            }

            // 오더
            // 주문 조회
            {
                var res = await coinone.Order.MyLimitOrders("XRP");
                foreach (var item in res.Orders)
                {
                    Console.WriteLine(item);
                }
            }
            // 체결된 주문 조회
            {
                var res = await coinone.Order.MyCompleteOrders("XRP");
                foreach (var item in res.CompleteOrders)
                {
                    Console.WriteLine(item);
                }
            }
            // 매수 
            string o_id;
            {// 뭐이리 깐깐해
                var res = await coinone.Order.LimitBuy(new() { Currency = "XRP", Price = 1590, Qty = 3.1448m });
                //res.ThrowResponseError();
                o_id = res.OrderId;
                Console.WriteLine("{0}", res.ErrorMessage);
                Console.WriteLine("매수: {0}", res);
            }
            // 주문 정보 V1
            {
                var res = await coinone.Order.MyOrderInformationV1(o_id, "XRP");
                res.ThrowResponseError();
                Console.WriteLine("주문 정보: {0}", res);
            }
            // 주문 취소
            {
                var res = await coinone.Order.CancelOrder(new() { OrderID = o_id, });
                res.ThrowResponseError();
                Console.WriteLine("매수 취소: {0}", res.IsSuccess);
            }
            // 매도
            {
                var res = await coinone.Order.LimitSell(new() { Currency = "XRP", Price = 1670, Qty = 1 });
                res.ThrowResponseError();
                o_id = res.OrderId;
                Console.WriteLine("매도: {0}", res);
            }
            // 주문 정보 V2
            {
                var res = await coinone.Order.MyOrderInformationV2(o_id, "XPR");
                res.ThrowResponseError();
                Console.WriteLine("주문 정보: {0}", res);
            }
            // 주문 취소
            {
                var res = await coinone.Order.CancelOrder(new() { OrderID = o_id, });
                res.ThrowResponseError();
                Console.WriteLine("매도 취소: {0}", res.IsSuccess);
            }

            Console.ReadLine();
        }
    }
}
