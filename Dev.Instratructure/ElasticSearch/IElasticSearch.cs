using Dev.Instratructure.ElasticSearch.Object;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Instratructure.ElasticSearch
{
    public interface IElasticSearch
    {
        IElasticClient GetClient();
        void CreateIndex(PageView model);
        void CreateIndex(List<PageView> list);
        PageView Get(string id);
        SearchResult<PageView> Search(string query, int page = 1, int pageSize = 10);
        List<ElasticResult> Autocomplete(string query);
    }
}
