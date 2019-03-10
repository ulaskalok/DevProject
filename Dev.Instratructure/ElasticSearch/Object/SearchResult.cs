using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Instratructure.ElasticSearch.Object
{
    public class SearchResult<T>
    {
        public int Total { get; set; }

        public int Page { get; set; }

        public IEnumerable<T> Results { get; set; }

        public long ElapsedMilliseconds { get; set; }
        
    }
}
