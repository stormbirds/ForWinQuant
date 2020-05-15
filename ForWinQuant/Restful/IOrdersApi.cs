// ===============================================================================
// Project Name        :    ForWinQuant
// Project Description :   
// ===============================================================================
// Class Name          :    IOrdersApi
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/15 15:47:29
// Update Time         :    2020/5/15 15:47:29
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
    public interface IOrdersApi
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="postOrder"></param>
        /// <returns></returns>
        [Post("/{type}")]
        Task<Restful<JObject>> createOrder(string api_id, string type, [Body]PostOrder postOrder);

        /// <summary>
        /// 查询我的订单列表
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="pairSymbol">可用交易对</param>
        /// <param name="states">状态</param>
        /// <param name="pageNum">从第几⻚</param>
        /// <param name="pageSize">分⻚</param>
        /// <returns></returns>
        [Get("")]
        Task<Restful<JObject>> getOrder(string api_id, string pairSymbol, int states, int pageNum, int pageSize);

        /// <summary>
        /// 查询指定订单
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="order_id"></param>
        /// <returns></returns>
        [Get("/{order_id}")]
        Task<Restful<Order>> getOrder(string api_id, string order_id);

        /// <summary>
        /// 申请撤销订单
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="order_id"></param>
        /// <returns></returns>
        [Post("/{order_id}/cancellation")]
        Task<Restful<JObject>> cancelOrder(string api_id, string order_id);

        /// <summary>
        /// 查询指定订单的成交记录
        /// </summary>
        /// <param name="api_id"></param>
        /// <param name="order_id"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Get("/{order_id}/trade-histories")]
        Task<Restful<JObject>> getOrderTrade(string api_id, string order_id, int pageNum, int pageSize);
    }

    public class PostOrder
    {
        public string pairSymbol { set; get; }
        public decimal price { set; get; }
        public int quantity { set; get; }
    }

    public class Order
    {
        public long id { set; get; }
        public string orderId { set; get; }
        public string orderKey { set; get; }
        public long memberId { set; get; }
        public string memberKey { set; get; }
        public string usr { set; get; }
        public string clientId { set; get; }
        public int orderType { set; get; }
        public string pairCode { set; get; }
        public string tradeCoinKey { set; get; }
        public string priceCoinKey { set; get; }
        public int priceType { set; get; }
        public decimal price { set; get; }
        public int quantity { set; get; }
        public int lastQuantity { set; get; }
        public int orderStatus { set; get; }
        public bool matched { set; get; }
        public int createdAt { set; get; }
        public int lastModifiedAt { set; get; }
        public long version { set; get; }
    }
}
