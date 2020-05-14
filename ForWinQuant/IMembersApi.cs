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
        [Get("/user-balances")]
        Task<Restful<List<UserBalances>>> GetUserBalances(string api_id,string timestamp);
    }


}
