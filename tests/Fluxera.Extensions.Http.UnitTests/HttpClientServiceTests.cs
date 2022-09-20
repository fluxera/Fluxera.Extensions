namespace Fluxera.Extensions.Http.UnitTests
{
	using System;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class HttpClientServiceTests
	{
		[Test]
		public void ShouldCreateNamedHttpClientService()
		{
			IServiceCollection services = new ServiceCollection();

			services.Configure<RemoteServiceOptions>(options =>
			{
				options.RemoteServices.Default = new RemoteService("http://www.fluxera.de");
			});

			services.AddHttpClientService<ITestHttpClientService, TestHttpClientService>(
				(context, serviceProvider) => new TestHttpClientService(context.Name, context.HttpClient, context.Options));

			IServiceProvider serviceProvider = services.BuildServiceProvider();

			ITestHttpClientService service = serviceProvider.GetRequiredService<ITestHttpClientService>();
			service.Should().NotBeNull();
		}
	}
}
