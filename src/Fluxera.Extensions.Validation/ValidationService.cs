namespace Fluxera.Extensions.Validation
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class ValidationService : IValidationService
	{
		private readonly IList<IValidator> validators = new List<IValidator>();

		public ValidationService(IEnumerable<IValidatorFactory> validatorFactories)
		{
			validatorFactories ??= Enumerable.Empty<IValidatorFactory>();

			foreach(IValidatorFactory validatorFactory in validatorFactories)
			{
				IValidator validator = validatorFactory.CreateValidator();
				this.validators.Add(validator);
			}
		}

		/// <inheritdoc />
		public async Task<ValidationResult> ValidateAsync<T>(T item) where T : class
		{
			ValidationResult validationResult = await this.validators.ValidateAsync(item);
			return validationResult;
		}

		/// <inheritdoc />
		public async Task ThrowOnValidateAsync<T>(T item) where T : class
		{
			ValidationResult validationResult = await this.ValidateAsync(item);
			if(!validationResult.IsValid)
			{
				throw new ValidationException(validationResult.ValidationErrors);
			}
		}
	}
}
