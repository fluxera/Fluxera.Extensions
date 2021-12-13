namespace Fluxera.Extensions.DependencyInjection.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection.UnitTests.Model;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class OpenGenericDecoratorTests : TestBase
	{
		[Test]
		public void ShouldDecorateOpenGenericService()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				// Add the service that gets decorated.
				services.AddTransient<IRepository<Employee>, Repository<Employee>>();

				// Add the decorator to the service.
				services.Decorate(typeof(IRepository<>)).With(typeof(DecoratingRepository<>));
			});

			IRepository<Employee> instance = serviceProvider.GetRequiredService<IRepository<Employee>>();
			DecoratingRepository<Employee> decorator = instance.Should().BeOfType<DecoratingRepository<Employee>>().Subject;
			decorator.InnerService.Should().BeOfType<Repository<Employee>>();
		}
	}
}
