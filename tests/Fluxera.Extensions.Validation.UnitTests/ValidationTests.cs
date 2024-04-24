namespace Fluxera.Extensions.Validation.UnitTests
{
	using System;
	using System.Linq;
	using System.Threading.Tasks;
	using FluentAssertions;
	using FluentAssertions.Specialized;
	using FluentValidation;
	using FluentValidation.Results;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class ValidationTests
	{
		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();

			services.AddValidation();
				
			IServiceProvider serviceProvider = services.BuildServiceProvider();
			this.validationService = serviceProvider.GetRequiredService<IValidationService>();
		}

		private IValidationService validationService;

		[Test]
		public async Task ShouldThrowValidationExceptionForInvalidInstanceAsync()
		{
			Person person = new Person
			{
				Name = null
			};

			Func<Task> func = async () => await this.validationService.ValidateAndThrowAsync(person);
			ExceptionAssertions<ValidationException> assertions = await func.Should().ThrowAsync<ValidationException>();
			assertions.Subject.First().Errors.Count().Should().Be(1);
		}

		[Test]
		public void ShouldThrowValidationExceptionForInvalidInstance()
		{
			Person person = new Person
			{
				Name = null
			};

			Action action = () => this.validationService.ValidateAndThrow(person);
			ExceptionAssertions<ValidationException> assertions = action.Should().Throw<ValidationException>();
			assertions.Subject.First().Errors.Count().Should().Be(1);
		}

		[Test]
		public async Task ShouldValidateInvalidInstanceAsync()
		{
			Person person = new Person
			{
				Name = null
			};

			ValidationResult result = await this.validationService.ValidateAsync(person);

			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
			result.Errors.Count.Should().Be(1);
		}

		[Test]
		public void ShouldValidateInvalidInstance()
		{
			Person person = new Person
			{
				Name = null
			};

			ValidationResult result = this.validationService.Validate(person);

			result.Should().NotBeNull();
			result.IsValid.Should().BeFalse();
			result.Errors.Count.Should().Be(1);
		}

		[Test]
		public async Task ShouldValidateValidInstanceAsync()
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
		public void ShouldValidateValidInstance()
		{
			Person person = new Person
			{
				Name = "Tester"
			};

			ValidationResult result = this.validationService.Validate(person);

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
		}

		[Test]
		public async Task ShouldValidateValidInstanceWithoutAnyValidationsAsync()
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
		public void ShouldValidateValidInstanceWithoutAnyValidations()
		{
			Company company = new Company
			{
				Name = "Test Inc."
			};

			ValidationResult result = this.validationService.Validate(company);

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
		}
	}
}
