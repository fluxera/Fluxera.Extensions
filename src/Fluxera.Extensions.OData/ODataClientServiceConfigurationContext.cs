namespace Fluxera.Extensions.OData
{
	using System.Net.Http;
	using Fluxera.Extensions.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	/// <summary>
	///     The configuration context for named OData client services.
	/// </summary>
	[PublicAPI]
	public class ODataClientServiceConfigurationContext : HttpClientServiceConfigurationContext
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ODataClientServiceConfigurationContext" /> type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="collectionName"></param>
		/// <param name="httpClient"></param>
		/// <param name="oDataClient"></param>
		/// <param name="options"></param>
		public ODataClientServiceConfigurationContext(string name, string collectionName, HttpClient httpClient, IODataClient oDataClient, RemoteService options)
			: base(name, httpClient, options)
		{
			this.CollectionName = Guard.Against.NullOrWhiteSpace(collectionName);
			this.ODataClient = Guard.Against.Null(oDataClient);
		}

		/// <summary>
		///     Gets the OData collection name.
		/// </summary>
		public string CollectionName { get; }

		/// <summary>
		///     Get the underlying http client instance.
		/// </summary>
		public IODataClient ODataClient { get; }
	}
}
