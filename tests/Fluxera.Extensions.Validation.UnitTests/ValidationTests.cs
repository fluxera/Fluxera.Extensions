namespace Fluxera.Extensions.Validation.UnitTests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using FluentAssertions.Specialized;
	using Fluxera.Extensions.Validation.DataAnnotations;
	using Fluxera.Extensions.Validation.FluentValidation;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ValidationTests
	{
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

		private IValidationService validationService;

		[Test]
		public async Task ShouldThrowValidationExceptionForInvalidInstance()
		{
			Person person = new Person
			{
				Name = null
			};

			Func<Task> func = async () => await this.validationService.ThrowOnValidateAsync(person);
			ExceptionAssertions<ValidationException> assertions = await func.Should().ThrowAsync<ValidationException>();
			assertions.Subject.First().Errors.Count.Should().Be(2);
		}

		[Test]
		public async Task ShouldValidate_InvalidInstance()
		{
			Person person = new Person
			{
				Name = null
			};

			ValidationResult result = await this.validationService.ValidateAsync(person);

			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
			result.ValidationErrors.Count.Should().Be(2);
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
	}
}
