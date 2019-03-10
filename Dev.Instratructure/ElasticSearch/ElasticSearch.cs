using Dev.Application;
using Dev.Instratructure.ElasticSearch.Object;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dev.Instratructure.ElasticSearch
{
    public class ElasticSearch : IElasticSearch
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IElasticClient _client;
        public ElasticSearch(IOptions<AppSettings> settings)
        {
            _settings = settings;
            _client = GetClient();
        }
        public IElasticClient GetClient()
        {
            var node = new Uri(_settings.Value.elastisearchUrl);
            var settings = new ConnectionSettings(node);
            //settings.DisableDirectStreaming(true);
            settings.DefaultIndex(_settings.Value.IndexName);
            return new ElasticClient(settings);
        }
        public void CreateIndex(PageView model)
        {
            if (!_client.IndexExists(_settings.Value.IndexName).Exists)
            {
                var indexDescriptor = new CreateIndexDescriptor(_settings.Value.IndexName)
                    .Mappings(ms => ms
                        .Map<PageView>(m => m.AutoMap().Properties(ps => ps
                                     .Completion(c => c
                                         .Name(p => p.Suggest)))));

                var result = _client.CreateIndex(_settings.Value.IndexName, i => indexDescriptor);
            }
            else
            {
                //_client.DeleteIndex(_settings.Value.IndexName);
            }
            _client.Index<PageView>(model, idx => idx.Index(_settings.Value.IndexName));
        }

        public void CreateIndex(List<PageView> list)
        {
            _client.IndexMany(list, _settings.Value.IndexName);
        }

        public PageView Get(string id)
        {
            var result = _client.Get<PageView>(new DocumentPath<PageView>(id));
            return result.Source;
        }

        public SearchResult<PageView> Search(string query, int page = 1, int pageSize = 10)
        {
            var result = _client.Search<PageView>(x => x.Query(q => q
                                                         .MultiMatch(mp => mp
                                                             .Query(query)
                                                                 .Fields(f => f
                                                                     .Fields(f1 => f1.PageName, f2 => f2.PageDesc))))
                                                     .From(page - 1)
                                                     .Size(pageSize));

            return new SearchResult<PageView>
            {
                Total = (int)result.Total,
                Page = page,
                Results = result.Documents,
                ElapsedMilliseconds = result.Took,
            };
        }

        public List<ElasticResult> Autocomplete(string keyword)
        {
            var response =  _client.Search<PageView>(s => s
       .Index(_settings.Value.IndexName)
        .Suggest(su => su
            .Completion("suggests", cs => cs
                .Field(f => f.Suggest)
                .Prefix(keyword)
                .Fuzzy(f => f
                    .Fuzziness(Fuzziness.Auto)
                )
                .Size(5)
            )
        )
     );

            var suggestions =
        from suggest in response.Suggest["suggests"]
        from option in suggest.Options
        select new ElasticResult
        {
            Name = option.Source.PageName,
            Value = option.Source.PageDesc
        };
            return suggestions.ToList();
        }
    }
}
