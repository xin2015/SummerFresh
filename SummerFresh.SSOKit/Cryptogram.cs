using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace SummerFresh.SSOKit
{
    [Serializable]
    public class Cryptogram : MarshalByRefObject
    {
        private static TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        private static MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

        public static bool Decrypt(string encryptedString, string key, out string result)
        {
            try
            {
                var TobeDecrypted = Convert.FromBase64String(encryptedString);
                des.Mode = CipherMode.ECB;
                des.Key = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(key));
                var Decrypted = des.CreateDecryptor().TransformFinalBlock(TobeDecrypted, 0, TobeDecrypted.Length);
                des.Clear();
                result = Encoding.UTF8.GetString(Decrypted);
                return true;
            }
            catch
            {
                result = string.Empty;
                return false;
            }
        }

        public static string Encrypt(string originalString, string key)
        {
            byte[] TobeEncrypted = Encoding.UTF8.GetBytes(originalString);
            des.Mode = CipherMode.ECB;
            des.Key = hashMD5.ComputeHash(Encoding.UTF8.GetBytes(key));
            var Encrypted = des.CreateEncryptor().TransformFinalBlock(TobeEncrypted, 0, TobeEncrypted.Length);
            des.Clear();
            return Convert.ToBase64String(Encrypted);
        }
    }
}