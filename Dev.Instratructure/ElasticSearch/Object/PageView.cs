using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Instratructure.ElasticSearch.Object
{
    public class PageView
    {
        public string PageName { get; set; }
        public string PageDesc { get; set; }

        public CompletionField Suggest { get; set; }
    }
}
