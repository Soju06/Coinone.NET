using System;
using System.Collections.Generic;
using System.Text;

namespace Soju06 {
    public static class ILovePython {
        public static byte[] Encode(this string s, Encoding encoding = null) =>
            (encoding ?? Encoding.UTF8).GetBytes(s);
        public static string Decode(this byte[] s, Encoding encoding = null) =>
            (encoding ?? Encoding.UTF8).GetString(s);
        public static byte[] DecodeBase64(this string s) =>
            Convert.FromBase64String(s);
        public static string EncodeBase64(this string s, Encoding encoding = null) =>
            Convert.ToBase64String(s.Encode(encoding ?? Encoding.UTF8));
        public static string EncodeBase64(this byte[] s) =>
            Convert.ToBase64String(s);

        public static string Join(this IEnumerable<string> s, string sep) => string.Join(sep, s);

        public static Uri Join(this Uri uri, params Uri[] uris) {
            var r = uri;
            for (int i = 0; i < uris.Length; i++)
                r = new Uri(r, uris[i]);
            return r;
        }
        
        public static Uri Join(this Uri uri, params string[] uris) {
            var r = uri;
            for (int i = 0; i < uris.Length; i++)
                r = new Uri(r, uris[i]);
            return r;
        }

        public static Uri Uri(this string s) => new Uri(s);

        public static T[] ToTArray<T>(this IEnumerable<T> s) where T : IEnumerable<T> {
            var w = new List<T>();
            w.AddRange(s);
            return w.ToArray();
        }

        public static List<T> ToTList<T>(this IEnumerable<T> s) where T : IEnumerable<T> {
            var w = new List<T>();
            w.AddRange(s);
            return w;
        }

        public static TList ToOList<T, TList>(this IEnumerable<T> s) 
                where TList : IList<T>, new() {
            var list = new TList();
            foreach (var item in s) list.Add(item); 
            return list;
        }

        public static TList ToOList<T, TList>(this IEnumerable<T> s, TList list)
                where TList : IList<T> {
            foreach (var item in s) list.Add(item);
            return list;
        }

        public static T New<T>(this Type type) =>
            (T)Activator.CreateInstance(type);

        public static T New<T>(this Type type, params object[] obs) =>
            (T)Activator.CreateInstance(type, obs);

        public static bool IsNullOrWhiteSpace(this string s) => string.IsNullOrWhiteSpace(s);
        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

        public static long ToUnixTimestamp(this DateTime time) => ((DateTimeOffset)time).ToUnixTimeSeconds();
        public static DateTime UnixTimestampToDateTime(this long time) => new DateTime(1970, 1, 1).AddSeconds(time);
    }
}
