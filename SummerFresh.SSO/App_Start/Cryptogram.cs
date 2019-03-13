using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace SummerFresh.SSO
{
    [Serializable]
    public class Cryptogram : MarshalByRefObject
    {
        private static TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        public static byte[] EACIV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly byte[] pIV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly byte[] pKEY = new byte[] { 
            0xda, 0xef, 0xe3, 0x16, 0x1f, 0x35, 120, 0xe0, 0xdf, 0xdf, 0xab, 210, 140, 0x9e, 0x2f, 0x56, 
            0x7a, 0x27, 0xee, 0x5f, 0x2f, 0x8a, 0x2c, 0x9b
         };

        static Cryptogram()
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
        }

        public static string base64_3DES_Encrypt(string sourceStr, string Key)
        {
            string s = "";
            string str2 = "";
            try
            {
                byte[] buffer3;
                byte[] kEY = HexStringToByteArray(Key);
                s = sourceStr;
                byte[] tobeEncrypted = FromBase64String(s);
                if (Encrypt(kEY, pIV, tobeEncrypted, out buffer3))
                {
                    str2 = ToBase64String(buffer3);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static string base64_3DES_SHA1_Decrypt(string sourceStr, string Key)
        {
            string str = "";
            try
            {
                byte[] buffer3;
                byte[] kEY = HexStringToByteArray(Key);
                byte[] tobeDecrypted = FromBase64String(sourceStr);
                if (Decrypt(kEY, pIV, tobeDecrypted, out buffer3))
                {
                    str = ToBase64String(buffer3);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        public static string base64_3DES_SHA1_Encrypt(string sourceStr, string Key)
        {
            string str2 = "";
            try
            {
                byte[] buffer3;
                byte[] kEY = HexStringToByteArray(Key);
                byte[] tobeEncrypted = FromBase64String(ComputeHashString(sourceStr));
                if (Encrypt(kEY, pIV, tobeEncrypted, out buffer3))
                {
                    str2 = ToBase64String(buffer3);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static string base64_SHA1_Encrypt(string sourceStr)
        {
            return ComputeHashString(sourceStr);
        }

        public static string ByteArrayToHexString(byte[] buf)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buf.Length; i++)
            {
                builder.Append((buf[i].ToString("X").Length == 2) ? buf[i].ToString("X") : ("0" + buf[i].ToString("X")));
            }
            return builder.ToString();
        }

        private static byte chr2hex(string chr)
        {
            switch (chr)
            {
                case "0":
                    return 0;

                case "1":
                    return 1;

                case "2":
                    return 2;

                case "3":
                    return 3;

                case "4":
                    return 4;

                case "5":
                    return 5;

                case "6":
                    return 6;

                case "7":
                    return 7;

                case "8":
                    return 8;

                case "9":
                    return 9;

                case "A":
                    return 10;

                case "B":
                    return 11;

                case "C":
                    return 12;

                case "D":
                    return 13;

                case "E":
                    return 14;

                case "F":
                    return 15;
            }
            return 0;
        }

        public static byte[] ComputeHash(byte[] buf)
        {
            return ((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(buf);
        }

        public static string ComputeHashString(string s)
        {
            return ToBase64String(ComputeHash(ConvertStringToByteArray(s)));
        }

        public static string ConvertByteArrayToString(byte[] buf)
        {
            return Encoding.GetEncoding("utf-8").GetString(buf);
        }

        public static byte[] ConvertStringToByteArray(string s)
        {
            return Encoding.GetEncoding("utf-8").GetBytes(s);
        }

        public static bool Decrypt(byte[] KEY, byte[] IV, byte[] TobeDecrypted, out byte[] Decrypted)
        {
            string message = "";
            Decrypted = null;
            try
            {
                int num;
                byte[] rgbIV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (num = 0; num < 8; num++)
                {
                    rgbIV[num] = IV[num];
                }
                byte[] rgbKey = new byte[] { 
                    0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 
                    0, 1, 2, 3, 4, 5, 6, 7
                 };
                for (num = 0; num < 0x18; num++)
                {
                    rgbKey[num] = KEY[num];
                }
                Decrypted = des.CreateDecryptor(rgbKey, rgbIV).TransformFinalBlock(TobeDecrypted, 0, TobeDecrypted.Length);
                des.Clear();
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
            return true;
        }

        public static bool Encrypt(byte[] KEY, byte[] IV, byte[] TobeEncrypted, out byte[] Encrypted)
        {
            string message = "";
            Encrypted = null;
            if ((KEY == null) || (IV == null))
            {
                return false;
            }
            try
            {
                int num;
                byte[] rgbIV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (num = 0; num < 8; num++)
                {
                    rgbIV[num] = IV[num];
                }
                byte[] rgbKey = new byte[] { 
                    0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 
                    0, 1, 2, 3, 4, 5, 6, 7
                 };
                for (num = 0; num < 0x18; num++)
                {
                    rgbKey[num] = KEY[num];
                }
                Encrypted = des.CreateEncryptor(rgbKey, rgbIV).TransformFinalBlock(TobeEncrypted, 0, TobeEncrypted.Length);
                des.Clear();
                return true;
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
        }

        public static byte[] FromBase64String(string s)
        {
            return Convert.FromBase64String(s);
        }

        public static string Generate3des(string Source, string Key)
        {
            string str2 = "";
            try
            {
                byte[] buffer4;
                byte[] iV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
                byte[] kEY = HexStringToByteArray(Key);
                byte[] tobeEncrypted = FromBase64String(ComputeHashString(Source));
                if (Encrypt(kEY, iV, tobeEncrypted, out buffer4))
                {
                    str2 = ToBase64String(buffer4);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public static byte[] GenerateIV()
        {
            byte[] iV = null;
            try
            {
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.GenerateIV();
                iV = provider.IV;
            }
            catch (Exception)
            {
                return iV;
            }
            return iV;
        }

        public static byte[] GenerateKey()
        {
            byte[] key = null;
            try
            {
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.GenerateKey();
                key = provider.Key;
            }
            catch (Exception)
            {
            }
            return key;
        }

        public static byte[] GenerateKey(string KEY)
        {
            byte[] buffer = null;
            try
            {
                buffer = new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(KEY));
            }
            catch (Exception)
            {
            }
            return buffer;
        }

        public static string GetGenerateKey()
        {
            return ByteArrayToHexString(GenerateKey());
        }

        public static byte[] HexStringToByteArray(string s)
        {
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)((chr2hex(s.Substring(i * 2, 1)) * 0x10) + chr2hex(s.Substring((i * 2) + 1, 1)));
            }
            return buffer;
        }

        public static string ToBase64String(byte[] buf)
        {
            return Convert.ToBase64String(buf);
        }
    }
}