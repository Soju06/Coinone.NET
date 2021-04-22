using System;
using System.Security;

namespace CoinoneNET.Networking {
    public abstract class CoinoneResponseRequestManagerBase: IDisposable {
        protected bool disposedValue;
        protected CoinoneResponseRequestManagerBase parent;
        protected CoinoneResponseRequestManager manager;
        protected SecureString accessToken;
        protected SecureString secretKey;

        protected virtual CoinoneResponseRequestManager Manager { get => manager ?? parent?.Manager; }
        protected virtual SecureString AccessToken { get => accessToken ?? parent?.AccessToken; }
        protected virtual SecureString SecretKey { get => secretKey ?? parent?.SecretKey; }

        internal CoinoneResponseRequestManagerBase(CoinoneResponseRequestManager manager) {
            this.manager = manager;
        }

        /// <summary>
        /// 엑세스 토큰을 등록합니다.
        /// </summary>
        /// <param name="token">토큰</param>
        public void RegisterSecurityAccessToken(SecureString accessToken) {
            this.accessToken?.Dispose();
            this.accessToken = accessToken.Copy();
        }

        /// <summary>
        /// 비밀 키를 등록합니다.
        /// </summary>
        /// <param name="token">비밀 키</param>
        public void RegisterSecuritySecretKey(SecureString secretKey) {
            this.secretKey?.Dispose();
            this.secretKey = secretKey.Copy();
        }

        protected void SetParentManager<T>(T manager) where T: CoinoneResponseRequestManagerBase {
            manager.parent = this;
        } 

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    accessToken?.Dispose();
                    secretKey?.Dispose();
                }

                manager = null;
                accessToken = null;
                secretKey = null;
                disposedValue = true;
            }
        }

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
