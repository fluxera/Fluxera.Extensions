namespace Fluxera.Extensions.DependencyInjection.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection.UnitTests.Model;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class DecoratorTests : TestBase
	{
		[Test]
		public void ShouldDecorate()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				// Add the service that gets decorated.
				services.AddSingleton<IDecoratedService, Decorated>();

				// Add the decorator to the service.
				services
					.Decorate<IDecoratedService>()
					.With<Decorator>();
			});

			IDecoratedService service = serviceProvider.GetRequiredService<IDecoratedService>();

			Decorator decorator = service.Should().BeOfType<Decorator>().Subject;
			decorator.InnerService.Should().BeOfType<Decorated>();
		}

		[Test]
		public void ShouldDecorateDifferentServices()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				// Add the service(s) that get decorated.
				services.AddSingleton<IDecoratedService, Decorated>();
				services.AddSingleton<IDecoratedService, AnotherDecorated>();

				// Add the decorator to the services.
				services
					.Decorate<IDecoratedService>()
					.With<Decorator>();
			});

			IDecoratedService[] services = serviceProvider.GetRequiredService<IEnumerable<IDecoratedService>>().ToArray();

			services.Length.Should().Be(2);
			services.Should().AllBeOfType<Decorator>();
		}

		[Test]
		public void ShouldDecorateExistingInstance()
		{
			Decorated instance = new Decorated();

			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				// Add the service instance that get decorated.
				services.AddSingleton<IDecoratedService>(instance);

				// Add the decorator to the service instance.
				services
					.Decorate<IDecoratedService>()
					.With<Decorator>();
			});

			IDecoratedService service = serviceProvider.GetRequiredService<IDecoratedService>();

			Decorator decorator = service.Should().BeOfType<Decorator>().Subject;
			Decorated decorated = decorator.InnerService.Should().BeOfType<Decorated>().Subject;

			instance.Should().BeSameAs(decorated);
		}

		[Test]
		public void ShouldDecorateMultipleLevels()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				// Add the service that gets decorated.
				services.AddSingleton<IDecoratedService, Decorated>();

				// Add the decorator(s) to the service.
				services
					.Decorate<IDecoratedService>()
					.With<Decorator>()
					.With<AnotherDecorator>();
			});

			IDecoratedService service = serviceProvider.GetRequiredService<IDecoratedService>();

			AnotherDecorator outerDecorator = service.Should().BeOfType<AnotherDecorator>().Subject;
			Decorator innerDecorator = outerDecorator.InnerService.Should().BeOfType<Decorator>().Subject;
			innerDecorator.InnerService.Should().BeOfType<Decorated>();
		}

		[Test]
		public void ShouldDecorateWithDecoratedDependency()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				// Add a service.
				services.AddSingleton<ITestService, TestService>();

				// Add the service that gets decorated.
				services.AddSingleton<IDecoratedService, Decorated>();

				// Add the decorator to the service.
				services
					.Decorate<IDecoratedService>()
					.With<Decorator>();
			});

			ITestService expected = serviceProvider.GetRequiredService<ITestService>();
			IDecoratedService service = serviceProvider.GetRequiredService<IDecoratedService>();

			Decorator decorator = service.Should().BeOfType<Decorator>().Subject;
			decorator.InjectedService.Should().BeSameAs(expected);
		}


		[Test]
		public void ShouldDecorateWithDecoratorDependency()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				// Add a service.
				services.AddSingleton<ITestService, TestService>();

				// Add the service that gets decorated.
				services.AddSingleton<IDecoratedService, Decorated>();

				// Add the decorator to the service.
				services
					.Decorate<IDecoratedService>()
					.With<Decorator>();
			});

			IDecoratedService service = serviceProvider.GetRequiredService<IDecoratedService>();

			Decorator decorator = service.Should().BeOfType<Decorator>().Subject;
			decorator.InjectedService.Should().BeOfType<TestService>();
		}

		[Test]
		public void ShouldDisposeDisposableServices()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				// Add the disposable service that gets decorated.
				services.AddTransient<IDisposableService, DisposableService>();

				// Add the decorator to the service.
				services
					.Decorate<IDisposableService>()
					.With<DisposableServiceDecorator>();
			});

			IDisposableService disposable = serviceProvider.GetRequiredService<IDisposableService>();
			DisposableServiceDecorator decorator = disposable.Should().BeOfType<DisposableServiceDecorator>().Subject;

			// Disposing the service provider should dispose services.
			((ServiceProvider)serviceProvider).Dispose();

			decorator.IsDisposed.Should().BeTrue();
			decorator.InnerService.IsDisposed.Should().BeTrue();
		}

		[Test]
		public void ShouldReplaceExistingServiceDescriptorWithFactory()
		{
			IServiceCollection services = new ServiceCollection();

			// Add the service that get decorated.
			services.AddSingleton<IDecoratedService, Decorated>();

			ServiceDescriptor originalDescriptor = services.GetDescriptor<IDecoratedService>();
			originalDescriptor.ImplementationFactory.Should().BeNull();
			originalDescriptor.ServiceType.Should().Be<IDecoratedService>();

			// Add the decorator to the service.
			services
				.Decorate<IDecoratedService>()
				.With<Decorator>();

			ServiceDescriptor changedDescriptor = services.GetDescriptor<IDecoratedService>();
			changedDescriptor.ServiceType.Should().Be<IDecoratedService>();
			changedDescriptor.ImplementationFactory.Should().NotBeNull();

			originalDescriptor.Should().NotBeSameAs(changedDescriptor);
		}

		[Test]
		public void ShouldThrowWhenDecoratingServiceNotRegistered()
		{
			Action action = () => BuildServiceProvider(services =>
			{
				// Add the decorator to a service that is not registered.
				services
					.Decorate<IDecoratedService>()
					.With<Decorator>();
			});
			action.Should().Throw<InvalidOperationException>();
		}
	}
}
