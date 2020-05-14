using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForWinQuant
{
    class QuantAuth : IAuthenticator
    {
        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (!request.Parameters.Any(p => "Authorization".Equals(p.Name, StringComparison.OrdinalIgnoreCase)))
            {
                request.AddHeader("Authorization", ConfigurationManager.AppSettings.Get("token"));
            }
        }
    }
}
