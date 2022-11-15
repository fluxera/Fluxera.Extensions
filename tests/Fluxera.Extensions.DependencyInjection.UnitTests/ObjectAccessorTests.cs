// ReSharper disable PossibleMultipleEnumeration

// ReSharper disable LocalVariableHidesMember

namespace Fluxera.Extensions.DependencyInjection.UnitTests
{
	using System;
	using System.Collections.Generic;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection.UnitTests.Model;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ObjectAccessorTests
	{
		[SetUp]
		public void SetUp()
		{
			// Arrange
			this.services = new ServiceCollection();
			this.testService = new TestService();
			this.services.AddObjectAccessor(this.testService);
			this.services.AddObjectAccessor<IAnotherTestService>();
			this.serviceProvider = this.services.BuildServiceProvider();
		}

		private IServiceCollection services;
		private ITestService testService;
		private ServiceProvider serviceProvider;

		[Test]
		public void ShouldGeAllObjectAccessorsFromServiceProvider()
		{
			// Act
			IEnumerable<IObjectAccessor> accessors = this.serviceProvider.GetObjectAccessors();

			// Assert
			accessors.Should().NotBeNull();
			accessors.Should().HaveCount(2);
		}

		[Test]
		public void ShouldGeAllObjectAccessorsFromServices()
		{
			// Act
			IEnumerable<IObjectAccessor> accessors = this.services.GetObjectAccessors();

			// Assert
			accessors.Should().NotBeNull();
			accessors.Should().HaveCount(2);
		}

		[Test]
		public void ShouldGetDefaultFromServiceProvider()
		{
			// Act
			IAnotherTestService testService = this.serviceProvider.GetObjectOrDefault<IAnotherTestService>();

			// Assert
			testService.Should().BeNull();
		}

		[Test]
		public void ShouldGetDefaultFromServices()
		{
			// Act
			IAnotherTestService testService = this.services.GetObjectOrDefault<IAnotherTestService>();

			// Assert
			testService.Should().BeNull();
		}

		[Test]
		public void ShouldGetDefaultObjectFromServiceProvider()
		{
			// Act
			ITestService testService = this.serviceProvider.GetObjectOrDefault<ITestService>();

			// Assert
			testService.Should().NotBeNull();
			testService.Should().BeEquivalentTo(this.testService);
		}

		[Test]
		public void ShouldGetDefaultObjectFromServices()
		{
			// Act
			ITestService testService = this.services.GetObjectOrDefault<ITestService>();

			// Assert
			testService.Should().NotBeNull();
			testService.Should().BeEquivalentTo(this.testService);
		}

		[Test]
		public void ShouldGetDefaultObjectFromServicesNotAvailable()
		{
			// Act
			IDisposableService testService = this.services.GetObjectOrDefault<IDisposableService>();

			// Assert
			testService.Should().BeNull();
		}

		[Test]
		public void ShouldGetObjectAccessorFromServiceProvider()
		{
			// Act
			IObjectAccessor<ITestService> objectAccessor = this.serviceProvider.GetRequiredService<IObjectAccessor<ITestService>>();

			// Assert
			objectAccessor.Should().NotBeNull();
		}

		[Test]
		public void ShouldGetObjectAccessorFromServices()
		{
			// Act
			IObjectAccessor<ITestService> objectAccessor = this.services.GetObjectAccessor<ITestService>();

			// Assert
			objectAccessor.Should().NotBeNull();
		}

		[Test]
		public void ShouldGetObjectFromServiceProvider()
		{
			// Act
			ITestService testService = this.serviceProvider.GetObject<ITestService>();

			// Assert
			testService.Should().NotBeNull();
			testService.Should().BeEquivalentTo(this.testService);
		}

		[Test]
		public void ShouldGetObjectFromServices()
		{
			// Act
			ITestService testService = this.services.GetObject<ITestService>();

			// Assert
			testService.Should().NotBeNull();
			testService.Should().BeEquivalentTo(this.testService);
		}

		[Test]
		public void ShouldThrowOnGetObjectFromServiceProvider()
		{
			// Act
			Action action = () =>
			{
				this.serviceProvider.GetObject<IAnotherTestService>();
			};

			action.Should().Throw<InvalidOperationException>();
		}

		[Test]
		public void ShouldThrowOnGetObjectFromServices()
		{
			// Act
			Action action = () =>
			{
				this.services.GetObject<IAnotherTestService>();
			};

			action.Should().Throw<InvalidOperationException>();
		}
	}
}
