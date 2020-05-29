// ===============================================================================
// Project Name        :    ForWinQuant
// Project Description :   
// ===============================================================================
// Class Name          :    IMarketApi
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/15 15:46:36
// Update Time         :    2020/5/15 15:46:36
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
    public interface IMarketApi
    {
        /// <summary>
        /// 获取最新深度明细
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="pair_symbol">这里的pair_symbol可以从查询可用交易的symbol获取</param>
        /// <returns>
        /// {
        ///    "code": 0,
        ///    "data": {
        ///        "asks": [
        ///            [
        ///                0.07179999,
        ///                0.35899995
        ///            ]
        ///        ],
        ///        "bids": [
        ///            [
        ///                0.06400001,
        ///                3.742438
        ///            ]
        ///        ]
        ///    }
        ///}
        /// </returns>
        [Get("/depth/{pair_symbol}")]
        Task<Restful<JObject>> GetDepthDetails(string api_id, string secret_key,string pair_symbol);

        /// <summary>
        /// 获取最新成交明细
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="pair_symbol"></param>
        /// <returns></returns>
        [Get("/{pair_symbol}")]
        Task<Restful<JObject>> GetPairSymbol(string api_id, string secret_key,string pair_symbol);
    }
}
