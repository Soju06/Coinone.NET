﻿using System;
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
    }
}