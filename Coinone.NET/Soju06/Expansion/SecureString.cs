using System.Runtime.InteropServices;
using System.Security;

namespace Soju06.Expansion {
    public static class ExpansionSecureString {
        public static string GetString(this SecureString s) =>
            Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(s));

        public static string GetStringAndDispose(this SecureString s) {
            using (var r = s) return Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(s));
        }

        public static void AppendString(this SecureString ss, string s) {
            for (int i = 0; i < s.Length; i++) ss.AppendChar(s[i]);
        }
    }
}
