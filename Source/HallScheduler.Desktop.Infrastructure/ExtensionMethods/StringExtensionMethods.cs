namespace HallScheduler.Desktop.Infrastructure.ExtensionMethods
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class StringExtensionMethods
    {
        public static string ConvertToUnsecureString(this SecureString securePassword)
        {
            if (securePassword == null)
            {
                throw new ArgumentNullException("\"securePassword\" argument, in ConvertToUnsecureString() method, must not be NULL");
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static SecureString ConvertToSecureString(this string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            var securePassword = new SecureString();
            for (int i = 0; i < password.Length; i++)
            {
                securePassword.AppendChar(password[i]);
            }
            securePassword.MakeReadOnly();

            return securePassword;
        }
    }
}
