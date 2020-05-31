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
    public interface IUpdateApi
    {
        [Get("/getVersions")]
        Task<Restful<List<AppVersion>>> GetNewVision();
    }
}
