namespace Fluxera.Extensions.Validation.UnitTests
{
	using System;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Fluxera.Extensions.Validation.DataAnnotations;
	using Fluxera.Extensions.Validation.FluentValidation;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ValidationTests
	{
		private IValidationService validationService;

		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddValidation(builder =>
			{
				builder
					.AddDataAnnotations()
					.AddFluentValidation(registration =>
					{
						registration.AddValidator<PersonValidator>();
					});
			});

			IServiceProvider serviceProvider = services.BuildServiceProvider();
			this.validationService = serviceProvider.GetRequiredService<IValidationService>();
		}

		[Test]
		public async Task ShouldValidate_ValidInstance()
		{
			Person person = new Person
			{
				Name = "Tester"
			};

			ValidationResult result = await this.validationService.ValidateAsync(person);

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
		}

		[Test]
		public async Task ShouldValidate_ValidInstanceWithoutAnyValidations()
		{
			Company company = new Company
			{
				Name = "Test Inc."
			};

			ValidationResult result = await this.validationService.ValidateAsync(company);

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
		}

		[Test]
		public async Task ShouldValidate_InvalidInstance()
		{
			Person person = new Person
			{
				Name = null!
			};

			ValidationResult result = await this.validationService.ValidateAsync(person);

			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
			result.ValidationErrors.Count.Should().Be(2);
		}
	}
}
