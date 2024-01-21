namespace Fluxera.Extensions.OData
{
	using System.Net.Http;
	using Fluxera.Extensions.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	/// <summary>
	///     An abstract base class for named OData clients.
	/// </summary>
	[PublicAPI]
	public abstract class ODataClientServiceBase<T, TKey> : HttpClientServiceBase, IODataClientService
		where T : class, IODataEntity<TKey>
	{
		/// <summary>
		///     Creates a new item of the <see cref="ODataClientServiceBase{T, TKey}" /> type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="collectionName"></param>
		/// <param name="httpClient"></param>
		/// <param name="oDataClient"></param>
		/// <param name="options"></param>
		protected ODataClientServiceBase(string name, string collectionName, HttpClient httpClient, IODataClient oDataClient, RemoteService options)
			: base(name, httpClient, options)
		{
			Guard.Against.Null(name);
			Guard.Against.NullOrWhiteSpace(collectionName);
			Guard.Against.Null(oDataClient);
			Guard.Against.Null(options);

			this.CollectionName = collectionName;
			this.ODataClient = oDataClient;

			V4Adapter.Reference();
		}

		/// <summary>
		///     Gets the collection name.
		/// </summary>
		protected string CollectionName { get; }

		/// <summary>
		///     Gets the OData client.
		/// </summary>
		protected IODataClient ODataClient { get; }
	}
}
