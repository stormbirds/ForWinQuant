using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForWinQuant
{
    public interface IMembersApi
    {

        /// <summary>
        /// 获取用户资产
        /// </summary>
        /// <param name="api_id"></param>
        /// <returns></returns>
        [Headers("Content-Type:application/json;charset=utf8")]
        [Get("/user-balances")]
        Task<Restful<List<UserBalances>>> GetUserBalances(string api_id, string secret_key);

    }

    public class UserBalances
    {
        public string symbol { set; get; }
        public float amounts { set; get; }
        public float frozen { set; get; }
    }

}
