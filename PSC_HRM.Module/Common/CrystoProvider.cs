using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PSC_HRM.Module
{
    public static class CrystoProvider
    {
        /// <summary>
        /// Encrypt password using SHA 512 algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPassword(string password)
        {
            if (!String.IsNullOrEmpty(password))
            {
                string result;
                SHA512Managed sha512 = new SHA512Managed();
                byte[] data = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                result = Convert.ToBase64String(data);
                return result;
            }
            return password;
        }

        /// <summary>
        /// Mã hóa 1 chuuỗi, có thể giải mã ngược lại
        /// </summary>
        /// <param name="toEncrypt">Chuỗi cần mã hóa</param>
        /// <param name="key">từ khóa dùng để mã hóa</param>
        /// <param name="useHashing">sử dụng MD5 để tạo key từ khóa</param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            try
            {
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// Giải mã ngược lại chuỗi đã mã hóa
        /// </summary>
        /// <param name="toDecrypt">Chuỗi mã hóa</param>
        /// <param name="key">từ khóa đã dùng để mã hóa</param>
        /// <param name="useHashing">sử dụng MD5 để tạo key từ khóa</param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            try
            {
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch
            {
                return "";
            }
        }

    }
}
