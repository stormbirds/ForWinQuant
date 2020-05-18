// ===============================================================================
// Project Name        :    ForWinQuant.Model
// Project Description :   
// ===============================================================================
// Class Name          :    EunexUser
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/18 12:08:24
// Update Time         :    2020/5/18 12:08:24
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForWinQuant.Model
{
    public class EunexUser
    {
        public long id { set; get; }
        /// <summary>
        /// 欧联账户名
        /// </summary>
        public string eunex_name { get; set; }
        /// <summary>
        /// API_KEY
        /// </summary>
        public string api_key { get; set; }
        public string api_secret { get; set; }
        /// <summary>
        /// 平台账号id
        /// </summary>
        public long user_id { get; set; }
        /// <summary>
        /// 授权过期时间
        /// </summary>
        public DateTime auth_date { get; set; }
        /// <summary>
        /// 工作状态
        /// </summary>
        public int is_working { get; set; }
        /// <summary>
        /// 当日刷单量
        /// </summary>
        public float day_total { get; set; }
    }
}
