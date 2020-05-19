// ===============================================================================
// Project Name        :    ForWinQuant
// Project Description :   
// ===============================================================================
// Class Name          :    Utils
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/14 17:33:11
// Update Time         :    2020/5/14 17:33:11
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ForWinQuant
{
    public static class Utils
    {
        /// <summary>  
        /// Unix时间戳转为C#格式时间  
        /// </summary>  
        /// <param name="timeStamp">Unix时间戳格式,例如1482115779</param>  
        /// <returns>C#格式时间</returns>  
        public static DateTimeOffset GetTime(string timeStamp)
        {
            long lTime = long.Parse(timeStamp);
            return DateTimeOffset.FromUnixTimeMilliseconds(lTime);
        }

        public static DateTimeOffset ToDateTime(long timeStamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(timeStamp);
        }

        /// <summary>
        /// 获取当前时间戳(UNIX秒)
        /// </summary>
        /// <returns></returns>
        public static long GetTimeStampSeconds()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        }

        public static long GetTimeStampMilliseconds()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }

        public static String Sha1Sign(String content, Encoding encode = null)
        {
            if (encode == null)
                encode = Encoding.Default;
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                String result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "").ToLower();
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
