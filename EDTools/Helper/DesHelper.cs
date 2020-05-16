using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace EDTools.Helper
{
    public class DesHelper
    {
        public static string Encrypt(string data, string key, int mode, int padding, string iv = null)
        {
            byte[] sourceBytes = Encoding.UTF8.GetBytes(data);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(key);
            des.Mode = (CipherMode)mode;
            des.Padding = (PaddingMode)padding;
            if ((des.Mode != CipherMode.ECB) && !string.IsNullOrEmpty(iv))
                des.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform transform = des.CreateEncryptor();
            return Convert.ToBase64String(transform.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length));
        }

        public static string Decrypt(string data, string key, int mode, int padding, string iv = null)
        {
            var encryptBytes = Convert.FromBase64String(data);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.UTF8.GetBytes(key);
            des.Mode = (CipherMode)mode;
            des.Padding = (PaddingMode)padding;
            if ((des.Mode != CipherMode.ECB) && !string.IsNullOrEmpty(iv))
                des.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform transform = des.CreateDecryptor();
            return Encoding.UTF8.GetString(transform.TransformFinalBlock(encryptBytes, 0, encryptBytes.Length));
        }
    }
}
