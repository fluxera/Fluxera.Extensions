// ReSharper disable UnusedTypeParameter

namespace Fluxera.Extensions.OData
{
	using Fluxera.Extensions.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	/// <summary>
	///     An abstract base class for named OData clients.
	/// </summary>
	[PublicAPI]
	public abstract class ODataClientServiceBase<T, TKey> : IODataClientService
		where T : class, IODataEntity<TKey>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ODataClientServiceBase{T, TKey}" /> type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="collectionName"></param>
		/// <param name="oDataClient"></param>
		/// <param name="options"></param>
		protected ODataClientServiceBase(string name, string collectionName, IODataClient oDataClient, RemoteService options)
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(collectionName, nameof(collectionName));
			Guard.Against.Null(oDataClient, nameof(oDataClient));
			Guard.Against.Null(options, nameof(options));

			this.Name = name;
			this.CollectionName = collectionName;
			this.ODataClient = oDataClient;
			this.Options = options;

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

		/// <summary>
		///     Gets the remote service options.
		/// </summary>
		protected RemoteService Options { get; }

		/// <inheritdoc />
		public string Name { get; }
	}
}
