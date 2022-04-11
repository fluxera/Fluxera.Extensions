namespace Fluxera.Extensions.Http.UnitTests
{
	using System;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	public class Tests
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
				(name, httpClient, options) => new TestHttpClientService(name, httpClient, options));

			IServiceProvider serviceProvider = services.BuildServiceProvider();

			ITestHttpClientService service = serviceProvider.GetRequiredService<ITestHttpClientService>();
			service.Should().NotBeNull();
		}
	}
}
