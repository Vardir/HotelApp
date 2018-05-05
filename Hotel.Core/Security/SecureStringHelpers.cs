using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Hotel.Core.Security
{
    public static class SecureStringHelpers
    {
        public static string Unsecure(this SecureString self)
        {
            if (self == null) return string.Empty;
            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(self);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
        public static SecureString Secure(this SecureString self, string value)
        {
            if (value == null)
                return self;
            for (int i = 0; i < value.Length; i++)
                self.AppendChar(value[i]);
            return self;
        }
    }
}