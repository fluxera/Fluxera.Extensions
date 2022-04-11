namespace Fluxera.Extensions.OData
{
	using System.Net.Http;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     The configuration context for a named OData service.
	/// </summary>
	[PublicAPI]
	public class ODataServiceCreationContext : HttpClientServiceCreationContext
	{
		/// <inheritdoc />
		public ODataServiceCreationContext(string remoteServiceName, string collectionName, IHttpClientFactory httpClientFactory, IOptions<RemoteServiceOptions> optionsWrapper)
			: base(remoteServiceName, httpClientFactory, optionsWrapper)
		{
			this.CollectionName = collectionName;
		}

		/// <summary>
		///     Gets the name of the OData collection of this service.
		/// </summary>
		public string CollectionName { get; }
	}
}
