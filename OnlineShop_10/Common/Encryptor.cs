using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OnlineShop_10.Common
{
    public static class Encryptor
    {
        public static string MD5Hash(string text)
        {
            // Chuyển đổi chuỗi thành mảng byte
            byte[] inputBytes = Encoding.UTF8.GetBytes(text);

            // Sử dụng MD5 để tạo hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi mảng byte thành chuỗi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}