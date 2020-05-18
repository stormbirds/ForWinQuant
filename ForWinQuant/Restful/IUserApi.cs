// ===============================================================================
// Project Name        :    ForWinQuant.Restful
// Project Description :   
// ===============================================================================
// Class Name          :    IUserApi
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/18 9:57:54
// Update Time         :    2020/5/18 9:57:54
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using ForWinQuant.Model;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForWinQuant.Restful
{
    public interface IUserApi
    {
        [Get("/queryEunexUsers")]
        Task<Restful<List<EunexUser>>> GetEunexUsers();

        [Post("/userLogin")]
        Task<AuthModel> Auth([Body()] JObject user);
    }
}
