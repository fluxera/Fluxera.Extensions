namespace Fluxera.Extensions.OData.UnitTests
{
	using System.Net.Http;
	using Fluxera.Extensions.Http;
	using Simple.OData.Client;

	public class TestODataClientService : ODataClientServiceBase<Person, string>, ITestODataClientService
	{
		/// <inheritdoc />
		public TestODataClientService(string name, string collectionName, HttpClient httpClient, IODataClient oDataClient, RemoteService options)
			: base(name, collectionName, httpClient, oDataClient, options)
		{
		}
	}
}
