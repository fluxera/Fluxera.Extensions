namespace Fluxera.Extensions.OData.UnitTests
{
	using Fluxera.Extensions.Http;
	using Simple.OData.Client;

	public class TestODataClientService : ODataClientServiceBase<Person, string>, ITestODataClientService
	{
		/// <inheritdoc />
		public TestODataClientService(string name, string collectionName, IODataClient oDataClient, RemoteService options)
			: base(name, collectionName, oDataClient, options)
		{
		}
	}
}
