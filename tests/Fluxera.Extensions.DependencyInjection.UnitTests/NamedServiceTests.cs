namespace Fluxera.Extensions.DependencyInjection.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class NamedServiceTests : TestBase
	{
		[Test]
		public void ShouldGetNamedService()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				services.AddNamedTransient<ITestService>(builder =>
				{
					builder
						.AddNameFor<TestService>("One")
						.AddNameFor<AnotherTestService>("Two");
				});
			});

			ITestService? service = serviceProvider.GetService<ITestService>("One");
			service.Should().NotBeNull();
			service.Should().BeOfType<TestService>();
		}
	}
}
