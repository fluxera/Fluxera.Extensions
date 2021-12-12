namespace Fluxera.Extensions.Caching
{
	using DataManagement;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Caching.Distributed;

	/// <summary>
	///		Provides the cache options for an entry in <see cref="IDistributedCache"/>.
	/// </summary>
	[PublicAPI]
	public sealed class CachingOptions
	{
		public CachingOptions()
		{
			this.Caching = new CachingConfiguration();
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///		Provides the caching configuration.
		/// </summary>
		public CachingConfiguration Caching { get; set; }

		/// <summary>
		///		Provides the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; set; }
	}
}
