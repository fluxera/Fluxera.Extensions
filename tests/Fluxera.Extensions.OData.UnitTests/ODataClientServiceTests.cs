namespace Fluxera.Extensions.OData.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.Http;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ODataClientServiceTests
	{
		[Test]
		public void ShouldCreateNamedODataClientService()
		{
			IServiceCollection services = new ServiceCollection();

			services.Configure<RemoteServiceOptions>(options =>
			{
				options.RemoteServices.Default = new RemoteService("http://www.fluxera.de");
			});

			services.AddODataClientService<ITestODataClientService, TestODataClientService>("People",
				(context, _) => new TestODataClientService(context.Name, context.CollectionName, context.ODataClient, context.Options));

			IServiceProvider serviceProvider = services.BuildServiceProvider();

			ITestODataClientService service = serviceProvider.GetRequiredService<ITestODataClientService>();
			service.Should().NotBeNull();
		}
	}
}
