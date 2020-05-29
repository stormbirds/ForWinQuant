// ===============================================================================
// Project Name        :    ForWinQuant.Helper
// Project Description :   
// ===============================================================================
// Class Name          :    EunexHelper
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/19 10:16:10
// Update Time         :    2020/5/19 10:16:10
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using ForWinQuant.Restful;
using Newtonsoft.Json.Linq;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace ForWinQuant.Helper
{
    public class EunexHelper
    {
        public static float countOrdersByDay(List<Order> orders, DateTimeOffset date)
        {
            float count = 0;
            foreach(Order order in orders)
            {
                DateTimeOffset orderTime = Utils.ToDateTime(order.createdAt).AddHours(-9);
                TimeSpan sp = orderTime.Subtract(date);
                if (order.orderType == 0 && sp.Days == 0)
                {
                    count += order.quantity;
                }
            }
            return count;
        }

        public static Task<AuthModel> Auth(string username,string password)
        {
            var api = HttpRestfulService.ForBaseApi<IUserApi>();
            var user = new JObject { new JProperty("username", username) };
            user.Add("password", password);
            return api.Auth(user);
        }

        #region 定义基础服务接口部分
        public static Task<Restful<JObject>> GetServerTime(string api_id, string secret_key)
        {
            var api = HttpRestfulService.ForBaseApi<IBaseApi>();
            return api.GetServerTime(api_id, secret_key);
        }

        public static Task<Restful<JObject>> GetCoins(string api_id, string secret_key)
        {
            var api = HttpRestfulService.ForBaseApi<IBaseApi>();
            return api.GetCoins(api_id, secret_key);
        }

        public static Task<Restful<JObject>> GetPairs(string api_id, string secret_key)
        {
            var api = HttpRestfulService.ForBaseApi<IBaseApi>();
            return api.GetPairs(api_id, secret_key);
        }
        #endregion

        #region 定义行情接口
        public static Task<Restful<JObject>> GetDepthDetails(string api_id, string secret_key,string pair_symbol)
        {
            var api = HttpRestfulService.ForBaseApi<IMarketApi>();
            return api.GetDepthDetails(api_id, secret_key, pair_symbol);
        }

        public static Task<Restful<JObject>> GetPairSymbol(string api_id, string secret_key,string pair_symbol) {
            var api = HttpRestfulService.ForBaseApi<IMarketApi>();
            return api.GetPairSymbol(api_id, secret_key, pair_symbol);
        }
        #endregion

        #region 定义用户信息接口
        public static Task<Restful<List<UserBalances>>> GetUserBalances(string api_id, string secret_key)
        {
            var api = HttpRestfulService.ForBaseApi<IMembersApi>();
            return api.GetUserBalances(api_id, secret_key);
        }
        #endregion

        #region 定义订单接口
        public static Task<Restful<JObject>> CreateOrder(string api_id, string secret_key,string type, [Body]PostOrder postOrder)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.CreateOrder(api_id, secret_key, type, postOrder);
        }

        public static Task<Restful<OrderContent>> GetOrder(string api_id, string secret_key,string pairSymbol, int states, int pageNum, int pageSize)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.GetOrder(api_id, secret_key, pairSymbol, states,pageNum,pageSize);
        }

        public static Task<Restful<Order>> GetOrder(string api_id, string secret_key,string order_id)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.GetOrder(api_id, secret_key, order_id);
        }

        public static Task<Restful<JObject>> CancelOrder(string api_id, string secret_key,string order_id)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.CancelOrder(api_id, secret_key, order_id);
        }

        public static Task<Restful<JObject>> GetOrderTrade(string api_id, string secret_key,string order_id, int pageNum, int pageSize)
        {
            var api = HttpRestfulService.ForBaseApi<IOrdersApi>();
            return api.GetOrderTrade(api_id, secret_key, order_id,pageNum,pageSize);
        }
        #endregion

    }
}
