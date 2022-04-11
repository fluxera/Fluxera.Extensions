namespace Fluxera.Extensions.Http.UnitTests
{
	using System;
	using System.Threading.Tasks;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	public class Tests
	{
		[Test]
		public async Task ShouldCreateNamedHttpClientService()
		{
			IServiceCollection services = new ServiceCollection();

			services.Configure<RemoteServiceOptions>(options =>
			{
				options.RemoteServices.Default = new RemoteService("http://www.fluxera.de");
			});

			services.AddHttpClientService<ITestHttpClientService>(
				context => new TestHttpClientService(context.RemoteServiceName, context.HttpClientFactory, context.OptionsWrapper));

			IServiceProvider serviceProvider = services.BuildServiceProvider();

			ITestHttpClientService service = serviceProvider.GetRequiredService<ITestHttpClientService>();
			string result = await service.GetSomethingAsync();
			Console.WriteLine(result);
		}
	}
}
