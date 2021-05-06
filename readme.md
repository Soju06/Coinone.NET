# Coinone.NET
.NET Framework용 코인원 API

## 사용방법

* using

  ```csharp
  using CoinoneNET;
  ```

* 이니셜라이즈

  ```csharp
  var coinone = new Coinone();
  coinone.RegisterSecurityAccessToken(accessToken); // 코인원에서 발급받은 AccessToken
  coinone.RegisterSecuritySecretKey(secretKey); // 코인원에서 발급받은 SecretKey
  ```

- 잔고 조회

  ```csharp
  var res = await coinone.Account.GetBalance();
  Console.WriteLine($"원화 {res.KRW}");
  Console.WriteLine($"비트코인 {res.BTC}");
  
  foreach (var item in res.Balances.FilterHoldingBalances())
      Console.WriteLine($"{item.CoinCode} {item.Balance} {item.Avail}");
  if (res.Wallets is not null)
      foreach (var item in res.Wallets)
  		Console.WriteLine($"{item.Label} {item.Balance}");
  ```

- 잔고 주소 조회

  ```csharp
  var res = await coinone.Account.GetDepositAddress();
  if (res.WalletAddresses is not null)
      foreach (var item in res.WalletAddresses.FilterHoldingWallets())
          Console.WriteLine(item); // 모든 Response는 ToString을 지원합니다.
  ```

- 사용자 정보 조회

  ```csharp
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
  ```
  **TODO: Add account, order, and transaction examples**

* SecureString 사용

  ```csharp
  coinone.RegisterSecurityAccessToken(accessToken);
  coinone.RegisterSecuritySecretKey(secretKey);
  ```

  키나 토큰을 등록할때 SecureString를 사용하게 되는데, unsafe char가 필요하지만

  Soju06.Expansion을 using하면 

  ```csharp
  using Soju06.Expansion;
  //
  SecureString secretKey = new(), accessToken = new();
  accessToken.AppendString("");
  secretKey.AppendString("");
  
  var coinone = new Coinone();
  coinone.RegisterSecurityAccessToken(accessToken);
  coinone.RegisterSecuritySecretKey(secretKey);
  ```

  간단하게 구현할 수 있습니다.

## 경고

SecureString class는 .NET Framework에서만 암호화됩니다.
.NET 기반 런타임에서 보안문자열을 사용하려면.. 그냥 직접 수정하세요 ㅋ