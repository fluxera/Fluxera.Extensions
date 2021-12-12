namespace Fluxera.Extensions.DependencyInjection.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection.UnitTests.Model;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class LazyTests
	{
		[Test]
		public void ShouldAddLazyService()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			services.AddLazy();
			services.AddSingleton<ITestService, TestService>();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			// Act
			Lazy<ITestService> lazy = serviceProvider.GetRequiredService<Lazy<ITestService>>();
			ITestService testService = lazy.Value;

			// Assert
			testService.Should().BeOfType<TestService>();
		}
	}
}
