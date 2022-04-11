namespace Fluxera.Extensions.Http.UnitTests
{
	using System.Threading.Tasks;

	public interface ITestHttpClientService : IHttpClientService
	{
		Task<string> GetSomethingAsync();
	}
}
