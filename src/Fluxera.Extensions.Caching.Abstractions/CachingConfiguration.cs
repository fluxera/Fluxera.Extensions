namespace Fluxera.Extensions.Caching
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Caching.Distributed;

	/// <summary>
	///     Provides the cache configuration for an entry in <see cref="IDistributedCache" />.
	/// </summary>
	[PublicAPI]
	public sealed class CachingConfiguration
	{
		public const string DefaultConnectionStringName = "Cache";

		public CachingConfiguration()
		{
			this.CacheEntryOptions = new DistributedCacheEntryOptions();
		}

		/// <summary>
		///     Provides the cache options for an entry in <see cref="IDistributedCache" />.
		/// </summary>
		public DistributedCacheEntryOptions CacheEntryOptions { get; set; }

		/// <summary>
		///     Provides the connection string to an external cache service.
		/// </summary>
		public string ConnectionStringName { get; set; } = DefaultConnectionStringName;
	}
}
