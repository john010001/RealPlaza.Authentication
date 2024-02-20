using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealPlaza.Authentication.Application.Security
{
    public class Encryptor
    {
        private sealed class ConvertTo
        {
            public static byte[] ConvertStringToByteArray(string s) =>
                new UnicodeEncoding().GetBytes(s);
        }

        public static string Bytes2Hex(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.AppendFormat("{0:x2}", bytes[i]);
            }
            return builder.ToString();
        }

        public static string DecryptSha1(string txtText)
        {
            StringBuilder builder = new StringBuilder();
            byte[] buffer = SHA1.Create().ComputeHash(new ASCIIEncoding().GetBytes(txtText));
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.AppendFormat("{0:x2}", buffer[i]);
            }
            return builder.ToString();
        }

        public static string DecryptTripleDES(string base64Text, string strKey, byte[] IV)
        {
            if ((base64Text.Trim() == string.Empty) || (strKey.Trim() == string.Empty))
            {
                return string.Empty;
            }
            ICryptoTransform transform = new TripleDESCryptoServiceProvider
            {
                Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(strKey)),
                Mode = CipherMode.CBC,
                IV = IV
            }.CreateDecryptor();
            byte[] inputBuffer = Convert.FromBase64String(base64Text);
            return Encoding.ASCII.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
        }

        public static string EncryptHashValue(string PlainText)
        {
            byte[] buffer = ConvertTo.ConvertStringToByteArray(PlainText);
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(buffer));
        }

        public static string EncryptTripleDES(string Plaintext, string strKey, byte[] IV)
        {
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider
            {
                Key = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(strKey)),
                Mode = CipherMode.CBC,
                IV = IV
            };
            byte[] bytes = Encoding.ASCII.GetBytes(Plaintext);
            return Convert.ToBase64String(provider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
        }

        public static byte[] Hex2Bytes(string strHex) =>
            (from x in Enumerable.Range(0, strHex.Length)
             where (x % 2) == 0
             select Convert.ToByte(strHex.Substring(x, 2), 0x10)).ToArray();

        public static byte[] RandomBytes(int LEN_BYTES)
        {
            byte[] data = new byte[LEN_BYTES];
            new RNGCryptoServiceProvider().GetNonZeroBytes(data);
            return data;
        }

        public static string RandomString(int LEN_SALT)
        {
            StringBuilder builder = new StringBuilder();
            byte[] data = new byte[LEN_SALT];
            new RNGCryptoServiceProvider().GetNonZeroBytes(data);
            for (int i = 0; i < data.Length; i++)
            {
                builder.AppendFormat("{0:x2}", data[i]);
            }
            return builder.ToString();
        }

        public static string RandomEntero(int LEN_SALT)
        {
            Random generator = new Random();
            String builder = generator.Next(0, 1000000).ToString("D6");
            return builder;
        }


        public static string Sha1(string txtText)
        {
            StringBuilder builder = new StringBuilder();
            byte[] buffer = SHA1.Create().ComputeHash(new ASCIIEncoding().GetBytes(txtText));
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.AppendFormat("{0:x2}", buffer[i]);
            }
            return builder.ToString();
        }
    }
}
