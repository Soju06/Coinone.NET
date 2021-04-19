namespace CoinoneNET.Networking {
    /// <summary>
    /// Coinone 오류 코드
    /// </summary>
    public enum CoinoneErrorCodes {
        /// <summary>
        /// 오류가 발생하지 않음
        /// </summary>
        Done = 0,
        /// <summary>
        /// Blocked user access
        /// 차단 된 사용자 액세스
        /// </summary>
        BlockedUserAccess = 4,
        /// <summary>
        /// Request Token Parameter is needed
        /// 요청 토큰 매개 변수가 필요합니다.
        /// </summary>
        RequestTokenParameterIsNeeded = 8,
        /// <summary>
        /// Access token is missing
        /// 액세스 토큰이 없습니다.
        /// </summary>
        AccessTokenIsMissing = 11,
        /// <summary>
        /// Invalid access token
        /// 잘못된 액세스 토큰
        /// </summary>
        InvalidAccessToken = 12,
        /// <summary>
        /// Invalid API permission
        /// 잘못된 API 권한
        /// </summary>
        InvalidAPIPermission = 40,
        /// <summary>
        /// Invalid API
        /// 잘못된 API
        /// </summary>
        InvalidAPI = 51,
        /// <summary>
        /// Two Factor Auth Fail
        /// 2 단계 인증 실패
        /// </summary>
        TwoFactorAuthFail = 53,
        /// <summary>
        /// Invalid format
        /// 잘못된 형식
        /// </summary>
        InvalidFormat = 101,
        /// <summary>
        /// Lack of Balance
        /// 균형 부족
        /// </summary>
        LackOfBalance = 103,
        /// <summary>
        /// Order id is not exist
        /// 주문 ID가 없습니다.
        /// </summary>
        OrderIdIsNotExist = 104,
        /// <summary>
        /// Price is not correct
        /// 가격이 정확하지 않습니다
        /// </summary>
        PriceIsNotCorrect = 105,
        /// <summary>
        /// Parameter error
        /// 매개 변수 오류
        /// </summary>
        ParameterError = 107,
        /// <summary>
        /// Unknown cryptocurrency.
        /// 알 수없는 암호 화폐.
        /// </summary>
        UnknownCryptocurrency = 108,
        /// <summary>
        /// Quantity is too low
        /// 수량이 너무 적습니다.
        /// </summary>
        QuantityIsTooLow = 113,
        /// <summary>
        /// This is not a valid your order amount.
        /// 유효한 주문 금액이 아닙니다.
        /// </summary>
        ThisIsNotAValidYourOrderAmount = 114,
        /// <summary>
        /// The sum of the holding quantity and the quantity of active orders has exceeded the maximum quantity.
        /// 보류 수량과 활성 주문 수량의 합계가 최대 수량을 초과했습니다.
        /// </summary>
        TheSumOfTheHoldingQuantityAndTheQuantityOfActiveOrdersHasExceededTheMaximumQuantity = 115,
        /// <summary>
        /// Already Traded.
        /// 이미 거래되었습니다.
        /// </summary>
        AlreadyTraded = 116,
        /// <summary>
        /// Already Canceled.
        /// 이미 취소되었습니다.
        /// </summary>
        AlreadyCanceled = 117,
        /// <summary>
        /// Already Submitted.
        /// 이미 제출되었습니다.
        /// </summary>
        AlreadySubmitted = 118,
        /// <summary>
        /// V2 API payload is missing
        /// V2 API 페이로드가 없습니다.
        /// </summary>
        V2APIPayloadIsMissing = 120,
        /// <summary>
        /// V2 API signature is missing
        /// V2 API 서명이 없습니다.
        /// </summary>
        V2APISignatureIsMissing = 121,
        /// <summary>
        /// V2 API nonce is missing
        /// V2 API 임시 값이 없습니다.
        /// </summary>
        V2APINonceIsMissing = 122,
        /// <summary>
        /// V2 API signature is not correct
        /// V2 API 서명이 올바르지 않습니다.
        /// </summary>
        V2APISignatureIsNotCorrect = 123,
        /// <summary>
        /// V2 API Nonce value must be a positive integer
        /// V2 API Nonce 값은 양의 정수 여야합니다.
        /// </summary>
        V2APINonceValueMustBeAPositiveInteger = 130,
        /// <summary>
        /// V2 API Nonce is must be bigger then last nonce
        /// V2 API Nonce는 마지막 임시 값보다 커야합니다.
        /// </summary>
        V2APINonceIsMustBeBiggerThenLastNonce = 131,
        /// <summary>
        /// It's V1 API. V2 Access token is not acceptable
        /// V1 API입니다. V2 액세스 토큰은 허용되지 않습니다.
        /// </summary>
        ItsV1APIV2AccessTokenIsNotAcceptable = 150,
        /// <summary>
        /// It's V2 API. V1 Access token is not acceptable
        /// V2 API입니다. V1 액세스 토큰은 허용되지 않습니다.
        /// </summary>
        ItsV2APIV1AccessTokenIsNotAcceptable = 151,
        /// <summary>
        /// Invalid address
        /// 잘못된 주소
        /// </summary>
        InvalidAddress = 152,
        /// <summary>
        /// The address is detected by FDS. Please contact our customer center.
        /// 주소는 FDS에 의해 감지됩니다. 고객 센터로 문의 해주세요.
        /// </summary>
        TheAddressIsDetectedByFDSPleaseContactOurCustomerCenter = 153,
        /// <summary>
        /// The address is not registered as an API withdrawal address.
        /// 해당 주소는 API 출금 주소로 등록되지 않았습니다.
        /// </summary>
        TheAddressIsNotRegisteredAsAnAPIWithdrawalAddress = 154,
        /// <summary>
        /// Withdrawal/Deposit id is invalid.
        /// 출금 / 입금 ID가 유효하지 않습니다.
        /// </summary>
        WithdrawalDepositIdIsInvalid = 160,
        /// <summary>
        /// Withdrawal quantity is not correct.
        /// 출금 수량이 정확하지 않습니다.
        /// </summary>
        WithdrawalQuantityIsNotCorrect = 202,
        /// <summary>
        /// Invalid 2FA .
        /// 2FA가 잘못되었습니다.
        /// </summary>
        Invalid2FA = 777,
        /// <summary>
        /// Server error
        /// 서버 오류
        /// </summary>
        ServerError = 405,
        /// <summary>
        /// API Deprecated
        /// API 지원 중단
        /// </summary>
        APIDeprecated = 1201,
        /// <summary>
        /// User not found
        /// 사용자를 찾을 수 없습니다.
        /// </summary>
        UserNotFound = 1206,
        /// <summary>
        /// Withdrawal of the virtual asset has been suspended. Please check our notice.
        /// 가상 자산 출금이 일시 중지되었습니다. 공지 사항을 확인 해주세요.
        /// </summary>
        WithdrawalOfTheVirtualAssetHasBeenSuspendedPleaseCheckOurNotice = 3001,
        /// <summary>
        /// Withdrawal is rejected
        /// 출금이 거부되었습니다.
        /// </summary>
        WithdrawalIsRejected = 3002,
        /// <summary>
        /// Exceed daily withdrawal limit
        /// 일일 출금 한도 초과
        /// </summary>
        ExceedDailyWithdrawalLimit = 3003,
        /// <summary>
        /// Failed by 24-hour withdrawal delay policy
        /// 24 시간 출금 지연 정책에 의해 실패
        /// </summary>
        FailedBy24hourWithdrawalDelayPolicy = 3004,
        /// <summary>
        /// Try again after complete phone verification.
        /// 전화 인증 완료 후 다시 시도하세요.
        /// </summary>
        TryAgainAfterCompletePhoneVerification = 3005,
        /// <summary>
        /// Withdrawal is restricted for 72 hours after the first KRW deposit.
        /// 출금은 최초 원화 입금 후 72 시간 동안 제한됩니다.
        /// </summary>
        WithdrawalIsRestrictedFor72HoursAfterTheFirstKRWDeposit = 3006,
        /// <summary>
        /// An error in the balance. Please contact CS center.
        /// 잔액에 오류가 있습니다. CS 센터로 문의 바랍니다.
        /// </summary>
        AnErrorInTheBalancePleaseContactCSCenter = 3007,
        /// <summary>
        /// Waiting to clear 72-hours withdrawal restriction
        /// 72 시간 출금 제한 해제 대기 중
        /// </summary>
        WaitingToClear72hoursWithdrawalRestriction = 3008,
        /// <summary>
        /// Account was detected through FDS system monitoring.
        /// FDS 시스템 모니터링을 통해 계정이 감지되었습니다.
        /// </summary>
        AccountWasDetectedThroughFDSSystemMonitoring = 3009,
        /// <summary>
        /// Account is locked
        /// 계정이 잠겨 있습니다.
        /// </summary>
        AccountIsLocked = 3010,
        /// <summary>
        /// Withdrawal is restricted by master account.
        /// 인출은 마스터 계정에 의해 제한됩니다.
        /// </summary>
        WithdrawalIsRestrictedByMasterAccount = 3011,
    }
}