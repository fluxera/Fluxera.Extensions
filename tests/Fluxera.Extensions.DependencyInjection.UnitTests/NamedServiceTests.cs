// ReSharper disable PossibleMultipleEnumeration

namespace Fluxera.Extensions.DependencyInjection.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection.UnitTests.Model;
	using NUnit.Framework;

	[TestFixture]
	public class NamedServiceTests : TestBase
	{
		[Test]
		public void ShouldAllowMultipleServicesPerName()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				services.AddNamedTransient<ITestService>(builder =>
				{
					builder
						.AddNameFor<TestService>("One")
						.AddNameFor<AnotherTestService>("Two")
						.AddNameFor<TestService>("One");
				});
			});

			IEnumerable<ITestService> services1 = serviceProvider.GetNamedServices<ITestService>("One");
			services1.Should().NotBeNullOrEmpty();
			services1.Should().AllBeOfType<TestService>();

			ITestService service2 = serviceProvider.GetNamedService<ITestService>("Two");
			service2.Should().NotBeNull();
			service2.Should().BeOfType<AnotherTestService>();
		}

		[Test]
		public void ShouldGetNamedService()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				services.AddNamedTransient<ITestService>(builder =>
				{
					builder
						.AddNameFor<TestService>("One")
						.AddNameFor<AnotherTestService>("Two")
						.AddNameFor<TestService>("Three");
				});
			});

			ITestService service1 = serviceProvider.GetNamedService<ITestService>("One");
			service1.Should().NotBeNull();
			service1.Should().BeOfType<TestService>();

			ITestService service2 = serviceProvider.GetNamedService<ITestService>("Two");
			service2.Should().NotBeNull();
			service2.Should().BeOfType<AnotherTestService>();

			ITestService service3 = serviceProvider.GetNamedService<ITestService>("Three");
			service3.Should().NotBeNull();
			service3.Should().BeOfType<TestService>();
		}

		[Test]
		public void ShouldMergeRegistrations()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				services.AddNamedTransient<ITestService>(builder =>
				{
					builder
						.AddNameFor<TestService>("One")
						.AddNameFor<TestService>("Three");
				});

				services.AddNamedTransient<ITestService>(builder =>
				{
					builder
						.AddNameFor<AnotherTestService>("Two");
				});
			});

			ITestService service1 = serviceProvider.GetNamedService<ITestService>("One");
			service1.Should().NotBeNull();
			service1.Should().BeOfType<TestService>();

			ITestService service2 = serviceProvider.GetNamedService<ITestService>("Two");
			service2.Should().NotBeNull();
			service2.Should().BeOfType<AnotherTestService>();

			ITestService service3 = serviceProvider.GetNamedService<ITestService>("Three");
			service3.Should().NotBeNull();
			service3.Should().BeOfType<TestService>();
		}
	}
}
