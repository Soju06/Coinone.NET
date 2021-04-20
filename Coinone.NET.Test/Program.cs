﻿using CoinoneNET;
using CoinoneNET.Networking.Request;
using CoinoneNET.Networking.Response.Account;
using Soju06;
using Soju06.API;
using Soju06.Expansion;
using Soju06.Web.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace CoinoneNETTest {
    class Program {
        static void Main(string[] _) {
            SecureString secretKey = new(), accessToken = new();
            var d = File.ReadAllLines(@"D:\Users\soju_\Downloads\sdfg.txt"); // 내 API키는 소중해
            accessToken.AppendString(d[0]);
            secretKey.AppendString(d[1]);
            Task.Run(async () => {
                var coinone = new Coinone();
                // 잔고 조회
                {
                    var res = await coinone.ResponseRequest<CoinoneAccountBalanceResponse,
                    CoinoneDefaultRequest>("account/balance/", new(accessToken), secretKey);
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
                    var res = await coinone.ResponseRequest<CoinoneDepositAddressResponse,
                    CoinoneDefaultRequest>("account/deposit_address/", new(accessToken), secretKey);
                    if(res.WalletAddresses is not null)
                        foreach (var item in res.WalletAddresses
                        .FilterHoldingWallets())
                            Console.WriteLine(item);
                }

                // 사용자 정보, 이제야 3개 만들었는데.... 그만하고싶다....
                {
                    var res = await coinone.ResponseRequest<CoinoneUserInformationResponse,
                    CoinoneDefaultRequest>("account/user_info/", new(accessToken), secretKey);
                    if (res.UserInformation is not null) {
                        Console.WriteLine("{0}", res.UserInformation.VirtualAccount);
                        Console.WriteLine("{0}", res.UserInformation.Bank);
                        Console.WriteLine("{0}", res.UserInformation.Mobile);
                        Console.WriteLine("Email {0}", res.UserInformation.Email);
                        Console.WriteLine("SecurityLevel {0}", res.UserInformation.SecurityLevel.ToString());
                        foreach (var item in res.UserInformation.FeeRatesI)
                            Console.WriteLine("{0}", item);
                    }
                }
            }).Wait();
            Console.ReadLine();
        }
    }
}
