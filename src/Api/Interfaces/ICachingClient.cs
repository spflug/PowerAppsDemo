using System.Collections.Generic;
using System.Threading.Tasks;
using PowerApps.Models;

namespace PowerApps.Interfaces
{
    public interface ICachingClient
    {
        IAsyncEnumerable<QueryResult<T>> GetPaged<T>(string uri) where T : IHttpResource;
        Task<QueryResult<T>> Search<T>(string path, string search, int max) where T : IHttpResource;
        T FromCache<T>(string resource) where T : IHttpResource;
    }
}