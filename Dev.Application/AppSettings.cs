using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Application
{
    public class AppSettings
    {
        public string RedisCache { get; set; }

        public int RedisPort { get; set; }

        public string IndexName { get; set; }
        public string elastisearchUrl { get; set; }

    }
}
