namespace Fluxera.Extensions.DependencyInjection.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection.UnitTests.Model;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ServiceCollectionExtensionsTests
	{
		[Test]
		public void ShouldReplaceSingleton()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			services.AddSingleton<ITestService, TestService>();

			// Act
			services.ReplaceSingleton<ITestService, AnotherTestService>();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			// Assert
			ITestService testService = serviceProvider.GetRequiredService<ITestService>();
			testService.Should().NotBeOfType<TestService>();
			testService.Should().BeOfType<AnotherTestService>();
		}

		[Test]
		public void ShouldReplaceSingletonInstance()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			services.AddSingleton<ITestService>(new TestService());

			// Act
			services.ReplaceSingleton<ITestService>(new AnotherTestService());
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			// Assert
			ITestService testService = serviceProvider.GetRequiredService<ITestService>();
			testService.Should().NotBeOfType<TestService>();
			testService.Should().BeOfType<AnotherTestService>();
		}

		[Test]
		public void ShouldReplaceTransient()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			services.AddTransient<ITestService, TestService>();

			// Act
			services.ReplaceTransient<ITestService, AnotherTestService>();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			// Assert
			ITestService testService = serviceProvider.GetRequiredService<ITestService>();
			testService.Should().NotBeOfType<TestService>();
			testService.Should().BeOfType<AnotherTestService>();
		}

		[Test]
		public void ShouldReplaceScoped()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();
			services.AddScoped<ITestService, TestService>();

			// Act
			services.ReplaceScoped<ITestService, AnotherTestService>();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			// Assert
			ITestService testService = serviceProvider.GetRequiredService<ITestService>();
			testService.Should().NotBeOfType<TestService>();
			testService.Should().BeOfType<AnotherTestService>();
		}

		[Test]
		public void ShouldReplaceSingleton_NoRegistrationAvailable()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();

			// Act
			services.ReplaceSingleton<ITestService, AnotherTestService>();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			// Assert
			ITestService testService = serviceProvider.GetRequiredService<ITestService>();
			testService.Should().NotBeOfType<TestService>();
			testService.Should().BeOfType<AnotherTestService>();
		}

		[Test]
		public void ShouldReplaceTransient_NoRegistrationAvailable()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();

			// Act
			services.ReplaceTransient<ITestService, AnotherTestService>();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			// Assert
			ITestService testService = serviceProvider.GetRequiredService<ITestService>();
			testService.Should().NotBeOfType<TestService>();
			testService.Should().BeOfType<AnotherTestService>();
		}

		[Test]
		public void ShouldReplaceScoped_NoRegistrationAvailable()
		{
			// Arrange
			IServiceCollection services = new ServiceCollection();

			// Act
			services.ReplaceScoped<ITestService, AnotherTestService>();
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			// Assert
			ITestService testService = serviceProvider.GetRequiredService<ITestService>();
			testService.Should().NotBeOfType<TestService>();
			testService.Should().BeOfType<AnotherTestService>();
		}
	}
}
