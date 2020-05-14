// ===============================================================================
// Project Name        :    ForWinQuant
// Project Description :   
// ===============================================================================
// Class Name          :    HttpRestfulService
// Class Version       :    v1.0.0.0
// Class Description   :   
// Author              :    baojun
// Create Time         :    2020/5/14 16:54:26
// Update Time         :    2020/5/14 16:54:26
// ===============================================================================
// Copyright © BAOJUN5040 2020 . All rights reserved.
// ===============================================================================

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ForWinQuant
{
    public static class HttpRestfulService
    {
        static readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        const string BaseEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/public";
        const string MarketEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/market";
        const string MembersEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/member";
        const string OrdersEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co/v2/orders";
        const string API_KEY = "KqtRQAwWjlqEnS8v";
        const string API_SECRET = "07OojLSkmAGdiPwm";

        static readonly TimeSpan RequestTimeout = TimeSpan.FromDays(1);

        public static T ForMembersApi<T>()
        {
            return RestService.For<T>(
              new HttpClient(new MembersHttpClientHandler())
              {
                  BaseAddress = new Uri(MembersEunexUrl),
                  Timeout = RequestTimeout
              },
              new RefitSettings()
              {
                  ContentSerializer = new NewtonsoftJsonContentSerializer(jsonSettings)
              }) ;
        }

        public static async Task<HttpResponseMessage> HandleHttpIO(HttpRequestMessage request, HttpResponseMessage response)
        {
            Console.Write("request url: " + request.RequestUri.ToString());
            var res = await response.Content.ReadAsStringAsync();
            Console.Write("response data: " + res);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.Write("响应错误 {}", JsonConvert.SerializeObject(response.Content));
            }
            return response;
        }

        public static string getSign(string query)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            query = query.Substring(1, query.Length - 1);
            string[] kv = query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in kv)
            {
                string[] kv2 = item.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                dic.Add(kv2[0], kv2[1]);
            }

            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(dic);
            StringBuilder basestring = new StringBuilder();
            for (int i = 0; i < sortedParams.Count; i++)
            {
                if (i < sortedParams.Count - 1)
                    basestring.Append(sortedParams.ToList()[i].Key).Append("=").Append(sortedParams.ToList()[i].Value).Append("&");
                else
                    basestring.Append(sortedParams.ToList()[i].Key).Append("=").Append(sortedParams.ToList()[i].Value);
            }
            string result = basestring.ToString();
            return result;
        }
        
    }


    class MembersHttpClientHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return null;
            }
            Dictionary<string, object> pairs = new Dictionary<string, object>();


            request.RequestUri = new Uri(request.RequestUri.ToString() + "&sign=");
            var response = await base.SendAsync(request, cancellationToken);
            var message = await HttpRestfulService.HandleHttpIO(request, response);
            return message;
        }
    }
}
