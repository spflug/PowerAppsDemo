using System.Collections.Generic;

namespace PowerApps.Models
{
    public class QueryResult<T>
    {
        public int Count { get; init; }
        public string Next { get; init; }
        public string Previous { get; init; }
        public IEnumerable<T> Results { get; init; }
    }
}