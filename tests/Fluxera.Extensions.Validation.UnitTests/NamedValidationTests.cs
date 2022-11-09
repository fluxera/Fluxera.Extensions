namespace Fluxera.Extensions.Validation.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Validation.DataAnnotations;
	using Fluxera.Extensions.Validation.FluentValidation;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class NamedValidationTests
	{
		[Test]
		public void ShouldGetNamedValidatorFactories()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddValidation(builder =>
			{
				builder
					.AddDataAnnotations("Name")
					.AddFluentValidation("Name", registration =>
					{
						registration.AddValidator<PersonValidator>();
					});
			});

			IServiceProvider serviceProvider = services.BuildServiceProvider();
			IEnumerable<IValidatorFactory> validatorFactories = serviceProvider.GetNamedServices<IValidatorFactory>("Name");

			validatorFactories.Count().Should().Be(2);
		}
	}
}
