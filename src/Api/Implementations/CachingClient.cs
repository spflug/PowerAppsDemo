using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using PowerApps.Interfaces;
using PowerApps.Models;

namespace PowerApps.Implementations
{
    internal class CachingClient : ICachingClient
    {
        private readonly IMemoryCache _cache;

        public CachingClient(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T FromCache<T>(string resource) where T : IHttpResource =>
            _cache.TryGetValue(resource, out var item)
                ? (T)item
                : default;

        public async Task<QueryResult<T>> Search<T>(string path, string search, int max) where T : IHttpResource
        {

            var url = search switch
            {
                null => $"{Constants.SwapiBaseUrl}/{path}/",
                _ => $"{Constants.SwapiBaseUrl}/{path}/?search={search}"
            };

            var list = new List<T>();
            await foreach (var page in GetPaged<T>(url))
            {
                list.AddRange(page.Results);
                if (list.Count > max) break;
            }

            return new QueryResult<T>
            {
                Count = list.Count,
                Next = null,
                Previous = null,
                Results = list.Take(max)
            };
        }

        public async IAsyncEnumerable<QueryResult<T>> GetPaged<T>(string uri) where T : IHttpResource
        {
            while (uri is { })
            {
                var o = await GetValueCached<QueryResult<T>>(uri, _cache);
                foreach (var person in o.Results)
                {
                    _cache.Set(person.Url, person, TimeSpan.FromHours(1));
                }
                yield return o;

                if (o is not { Next: { } next }) break;
                uri = next;
            }
        }

        private static async Task<T> GetValueCached<T>(string uri, IMemoryCache cache)
        {
            if (cache.TryGetValue(uri, out T value)) return value;

            var freshValue = await GetValue<T>(uri);
            cache.Set(uri, freshValue, TimeSpan.FromHours(1));

            return freshValue;
        }

        private static async Task<T> GetValue<T>(string uri)
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(uri);

            return JsonSerializer.Deserialize<T>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }
    }
}