using Dev.Application;
using Dev.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading.Tasks;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;

namespace Dev.Instratructure.Cache
{
    public class RedisCache : ICache
    {
        private readonly NewtonsoftSerializer serializer;
        private readonly StackExchangeRedisCacheClient cacheClient;
        public RedisCache(IOptions<AppSettings> settings)
        {
            var redisConfiguration = new RedisConfiguration()
            {
                Hosts = new RedisHost[]
                 {
                    new RedisHost(){Host = settings.Value.RedisCache, Port = settings.Value.RedisPort},
                 },
                AllowAdmin = true,
                Database = 0,
                SyncTimeout = 30000,
                ConnectTimeout = 30000
            };
            serializer = new NewtonsoftSerializer();
            cacheClient = new StackExchangeRedisCacheClient(serializer, redisConfiguration);
        }

        public async Task<bool> Delete(string key)
        {
            return await cacheClient.RemoveAsync(key);
        }

        public async Task<T> Get<T>(string key)
        {
            return await cacheClient.GetAsync<T>(key);
        }

        public async Task<IDictionary<string, T>> GetAll<T>(string key)
        {
            var stringlist = cacheClient.SearchKeys("*" + key + "*");

            return await cacheClient.GetAllAsync<T>(stringlist);
        }

        public async Task<bool> Store<T>(string key, T value)
        {
            return await cacheClient.AddAsync(key, value);
        }

        private string GenerateKeyWithParams(string key, string[] @params)
        {
            if (@params == null)
            {
                return key;
            }

            var complexKey = key;

            foreach (var param in @params)
            {
                complexKey += $"&{param}";
            }

            return complexKey;
        }
    }
}

