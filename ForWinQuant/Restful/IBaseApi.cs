// ===============================================================================
// Project Name        :    ForWinQuant
// Project Description :   
// ===============================================================================
// Class Name          :    IBaseApi
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/15 10:34:49
// Update Time         :    2020/5/15 10:34:49
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForWinQuant
{
    public interface IBaseApi
    {
        /// <summary>
        /// 查询服务器时间
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        [Get("/server-time")]
        Task<Restful<JObject>> GetServerTime();

        /// <summary>
        /// 查询可用币种
        /// </summary>
        /// <param name="api_id"></param>
        /// <returns></returns>
        [Get("/coins")]
        Task<Restful<JObject>> GetCoins();

        /// <summary>
        /// 查询可用交易对
        /// </summary>
        /// <param name="api_id"></param>
        /// <returns></returns>
        [Get("/pairs")]
        Task<Restful<JObject>> GetPairs();


    }
}
