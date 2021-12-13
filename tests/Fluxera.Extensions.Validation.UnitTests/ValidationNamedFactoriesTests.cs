namespace Fluxera.Extensions.Validation.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Extensions.Validation.DataAnnotations;
	using Fluxera.Extensions.Validation.FluentValidation;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ValidationNamedFactoriesTests
	{
		[Test]
		public void ShouldRegisterNamedValidatorFactories()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddValidation(builder =>
			{
				builder
					.AddDataAnnotations("One")
					.AddFluentValidation("Two", registration =>
					{
						registration.AddValidator<PersonValidator>();
					})
					.AddDataAnnotations("Three");
			});

			IServiceProvider serviceProvider = services.BuildServiceProvider();

			IValidatorFactory validatorFactory1 = serviceProvider.GetRequiredNamedService<IValidatorFactory>("Two");
			IValidatorFactory validatorFactory2 = serviceProvider.GetRequiredNamedService<IValidatorFactory>("One");

			validatorFactory1.GetType().Name.Should().Be("FluentValidationValidatorFactory");
			validatorFactory2.GetType().Name.Should().Be("DataAnnotationsValidationValidatorFactory");
		}
	}
}
