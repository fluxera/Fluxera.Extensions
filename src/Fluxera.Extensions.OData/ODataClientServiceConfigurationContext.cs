namespace Fluxera.Extensions.OData
{
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	/// <summary>
	///     The configuration context for named OData client services.
	/// </summary>
	[PublicAPI]
	public class ODataClientServiceConfigurationContext
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ODataClientServiceConfigurationContext" /> type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="collectionName"></param>
		/// <param name="oDataClient"></param>
		/// <param name="options"></param>
		public ODataClientServiceConfigurationContext(string name, string collectionName, IODataClient oDataClient, RemoteService options)
		{
			this.Name = name;
			this.CollectionName = collectionName;
			this.ODataClient = oDataClient;
			this.Options = options;
		}

		/// <summary>
		///     Gets the OData collection name.
		/// </summary>
		public string CollectionName { get; }

		/// <summary>
		///     Gets the name of the http client service.
		/// </summary>
		public string Name { get; }

		/// <summary>
		///     Get the underlying http client instance.
		/// </summary>
		public IODataClient ODataClient { get; }

		/// <summary>
		///     Gets the remote service options.
		/// </summary>
		public RemoteService Options { get; }
	}
}
