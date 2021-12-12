namespace Fluxera.Extensions.OData
{
	using System;
	using System.Net.Http;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	[PublicAPI]
	public interface IODataClientFactory //: IHttpClientFactory
	{
		IODataClient CreateClient(string name);
		
		IODataClient CreateClient(string name, Action<ODataClientSettings> configureSettings);
		
		IHttpClientFactory HttpClientFactory { get; }
	}
}
