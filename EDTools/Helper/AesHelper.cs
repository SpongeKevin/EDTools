using System;
using System.Security.Cryptography;
using System.Text;

namespace EDTools.Helper
{
    public class AesHelper
    {
        public static string Encrypt(string data, string key, int mode, int padding, string iv = null)
        {
            byte[] sourceBytes = Encoding.UTF8.GetBytes(data);

            RijndaelManaged aes = new RijndaelManaged();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.Mode = (CipherMode)mode;
            aes.Padding = (PaddingMode)padding;
            if ((aes.Mode != CipherMode.ECB) && !string.IsNullOrEmpty(iv))
                aes.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform transform = aes.CreateEncryptor();
            return Convert.ToBase64String(transform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length));
        }

        public static string Decrypt(string data, string key, int mode, int padding, string iv = null)
        {
            var encryptBytes = Convert.FromBase64String(data);

            RijndaelManaged aes = new RijndaelManaged();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.Mode = (CipherMode)mode;
            aes.Padding = (PaddingMode)padding;
            if ((aes.Mode != CipherMode.ECB) && !string.IsNullOrEmpty(iv))
                aes.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform transform = aes.CreateDecryptor();
            return Encoding.UTF8.GetString(transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));
        }
    }
}
