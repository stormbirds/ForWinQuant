using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForWinQuant.Model
{
    public class AppVersion
    {
        public long id { get; set; }
        public string version_code { get; set; }
        public string release_note { get; set; }
        public string url { get; set; }
        public DateTimeOffset created_time { get; set; }
    }
}
