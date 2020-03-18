using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Win.Helpers.Util
{
    public class Encrypt
    {
        static string hash = "ReklamKafe";

        public static string Md5(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(@"Şifrelenecek veri yok");
            else
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(text);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        return Convert.ToBase64String(results, 0, results.Length).Replace('+', '_').Replace('/', '-');
                    }
                }
            }
        }
        public static string Md5Decrypt(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(@"Çözülecek veri yok");
            else
            {
                byte[] data = Convert.FromBase64String(text.Replace('_', '+').Replace('-', '/')); // decrypt the incrypted text
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        return UTF8Encoding.UTF8.GetString(results).Replace("RKCONFIRM2019", "");
                    }
                }
            }
        }

        public static string CreateRandomPassword(int length = 8)
        {
            try
            {
                // Create a string of characters, numbers, special characters that allowed in the password  
                string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#*?_-";
                Random random = new Random();
                // Select one random character at a time from the string  
                // and create an array of chars  
                char[] chars = new char[length];
                for (int i = 0; i < length; i++)
                    chars[i] = validChars[random.Next(0, validChars.Length)];
                return new string(chars);
            }
            catch (Exception)
            {
                return DateTime.Now.Ticks.ToString();
            }
        }
    }
}
