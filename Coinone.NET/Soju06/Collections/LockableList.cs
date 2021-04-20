using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Soju06.Collections {
    public class LockableList<T> : IList<T> {
        public LockableList() { }
        public LockableList(IList<T> list) =>
            List = list.ToList();
        public LockableList(params T[] list) =>
            List = list.ToList();

        private List<T> List { get; set; } = new();

        public T this[int index] { get => List[index]; set => List[index] = value; }

        public int Count => List.Count;

        public bool IsLocked { get; private set; }

        public bool IsReadOnly => IsLocked;

        public void Add(T item) {
            CheckLocked();
            List.Add(item);
        }

        public void Clear() {
            CheckLocked();
            List.Clear();
        }

        public bool Contains(T item) => List.Contains(item);

        public void CopyTo(T[] array, int arrayIndex) =>
            List.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator() =>
            List.GetEnumerator();

        public int IndexOf(T item) => List.IndexOf(item);

        public void Insert(int index, T item) {
            CheckLocked();
            List.Insert(index, item);
        }

        public bool Remove(T item) {
            CheckLocked();
            return List.Remove(item);
        }

        public void RemoveAt(int index) {
            CheckLocked();
            List.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            List.GetEnumerator(); 

        private void CheckLocked() {
            if (IsLocked) throw new ListLockedException("this list is locked");
        }

        protected void Lock() =>
            IsLocked = true;

        protected void Unlock() =>
            IsLocked = false;

        public static implicit operator LockableList<T>(List<T> s) =>
            new LockableList<T>(s);
        public static implicit operator LockableList<T>(T[] s) =>
            new LockableList<T>(s);
        public static implicit operator List<T>(LockableList<T> s) =>
            s?.ToList();
        public static implicit operator T[](LockableList<T> s) =>
            s?.ToArray();
    }

    [Serializable]
    public class ListLockedException : Exception {
        public ListLockedException() { }
        public ListLockedException(string message) : base(message) { }
    }
}
