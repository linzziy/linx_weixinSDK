using System;
using System.Security.Cryptography;

namespace Linx.Weixin.Framework
{
    public class SecurityHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyStr"></param>
        /// <returns></returns>
        public static string SHA1(string keyStr)
        {
            //FormsAuthentication.HashPasswordForStoringInConfigFile(strkey, "SHA1");

            byte[] data = new byte[keyStr.Length * 8];
            byte[] result;

            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            result = sha.ComputeHash(data);

            return System.Text.Encoding.UTF8.GetString(result);
        }
    }
}