using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soju06.Task {
    public sealed class AsyncLock {
        private readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<IDisposable> LockAsync(CancellationToken? token = null) {
            if (token.HasValue) await _semaphore.WaitAsync(token.Value);
            else await _semaphore.WaitAsync();
            return new Handler(_semaphore);
        }

        private sealed class Handler : IDisposable {
            private readonly SemaphoreSlim _semaphore;
            private bool _disposed = false;

            public Handler(SemaphoreSlim semaphore) {
                _semaphore = semaphore;
            }

            public void Dispose() {
                if (!_disposed) {
                    _semaphore.Release();
                    _disposed = true;
                }
            }
        }
    }
}
