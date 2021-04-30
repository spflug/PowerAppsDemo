using System.Collections.Generic;

namespace PowerApps.Models
{
    /// <summary>
    /// A query result container.
    /// </summary>
    /// <typeparam name="T">The specific resource type been queried.</typeparam>
    public class QueryResult<T>
    {
        /// <summary>
        /// Gets a value indicating how many items were found.
        /// </summary>
        public int Count { get; init; }

        /// <summary>
        /// Gets a link to the next page when the result is paged. Otherwise this property is not set.
        /// </summary>
        public string Next { get; init; }

        /// <summary>
        /// Gets a link to the previous page when the result is paged. Otherwise this property is not set.
        /// </summary>
        public string Previous { get; init; }

        /// <summary>
        /// The result of the given query. This collection contains <see cref="Count"/> amount of items.
        /// </summary>
        /// <typeparamref name="T">The specific resource type been queried.</typeparamref>
        public IEnumerable<T> Results { get; init; }
    }
}