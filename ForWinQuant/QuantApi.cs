using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace ForWinQuant
{
    public class QuantApi
    {
        const string BaseLoginUrl = "http://192.168.1.104:8000";
        const string BaseEunexUrl = "https://api6c9jg3nfw1s1js.eunex.co";
        readonly IRestClient _client;
        string token = ConfigurationManager.AppSettings.Get("token");

        public QuantApi()
        {
            if (!String.IsNullOrEmpty(token)) { 
                _client = new RestClient(BaseEunexUrl); 
            }
        }

        public Dictionary<string, object> login(string name,string psword)
        {
            var client = new RestClient(BaseLoginUrl);
            var request = new RestRequest("userLogin", DataFormat.Json);
            request.AddJsonBody(new { username = name, password = psword });
            var response = client.Post(request);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
        }
    }
}
