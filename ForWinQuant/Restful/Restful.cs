using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForWinQuant
{
    public class Restful<T>
    {
        public T data { get; set; }
        public int code { get; set; }
        public string msg { get; set; }
    }

    public class AuthModel
    {
        public string access_token { get; set; }
        public int code { get; set; }
        public string msg { get; set; }
    }

    public static class HttpStatuscode
    {
        public static int Success = 0;
    }
}
