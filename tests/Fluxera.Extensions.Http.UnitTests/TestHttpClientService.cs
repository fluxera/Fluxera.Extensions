namespace Fluxera.Extensions.Http.UnitTests
{
	using System.Net.Http;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Options;

	public class TestHttpClientService : HttpClientServiceBase, ITestHttpClientService
	{
		/// <inheritdoc />
		public TestHttpClientService(string name, IHttpClientFactory httpClientFactory, IOptions<RemoteServiceOptions> optionsWrapper)
			: base(name, httpClientFactory, optionsWrapper)
		{
		}

		public async Task<string> GetSomethingAsync()
		{
			HttpResponseMessage response = await this.HttpClient.GetAsync("/");
			return await response.Content.ReadAsStringAsync();
		}
	}
}
