namespace Fluxera.Extensions.Http.UnitTests
{
	using System.Net.Http;
	using System.Threading.Tasks;

	public class TestHttpClientService : HttpClientServiceBase, ITestHttpClientService
	{
		/// <inheritdoc />
		public TestHttpClientService(string name, HttpClient httpClient, RemoteService options)
			: base(name, httpClient, options)
		{
		}

		public async Task<string> GetSomethingAsync()
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync("/");
			return await response.Content.ReadAsStringAsync();
		}
	}
}
