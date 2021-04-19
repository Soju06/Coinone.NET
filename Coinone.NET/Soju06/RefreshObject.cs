using System;

namespace Soju06 {
    public class RefreshObject<Obj> {
        public RefreshObject(TimeSpan refreshCycle, Obj obj) {
            RefreshCycle = refreshCycle; Object = obj;
        }

        public DateTime RefreshTime { get; private set; }
        public DateTime RefreshDateTime { get; private set; }
        public TimeSpan RefreshCycle { get; set; }
        public Obj Object {
            get => _object;
            set {
                RefreshDateTime = (RefreshTime = DateTime.UtcNow) + RefreshCycle;
                _object = value;
            }
        }

        public TimeSpan RefreshRemainingTime {
            get => RefreshDateTime - DateTime.UtcNow;
        }

        public bool IsTimeout {
            get => RefreshDateTime < DateTime.UtcNow;
        }

        protected Obj _object;
    }
}
