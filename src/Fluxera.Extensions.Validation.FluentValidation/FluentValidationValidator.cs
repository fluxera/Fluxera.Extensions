namespace Fluxera.Extensions.Validation.FluentValidation
{
	using System.Threading.Tasks;

	/// <summary>
	///		A <see cref="IValidator"/> that uses FluentValidation to validate instances.
	/// </summary>
	internal sealed class FluentValidationValidator : IValidator
	{
		private readonly global::FluentValidation.IValidatorFactory validatorFactory;

		public FluentValidationValidator(global::FluentValidation.IValidatorFactory validatorFactory)
		{
			this.validatorFactory = validatorFactory;
		}

		public async Task<ValidationResult> ValidateAsync(object entity)
		{
			ValidationResult result = new ValidationResult();

			global::FluentValidation.IValidator validator = this.validatorFactory.GetValidator(entity.GetType());

			if (validator != null)
			{
				global::FluentValidation.Results.ValidationResult validationResult =
					await validator.ValidateAsync(new global::FluentValidation.ValidationContext<object>(entity));

				if (!validationResult.IsValid)
				{
					foreach (global::FluentValidation.Results.ValidationFailure validationFailure in validationResult.Errors)
					{
						result.ValidationErrors.Add(new ValidationError(validationFailure.PropertyName)
						{
							ErrorMessages =
							{
								validationFailure.ErrorMessage,
							},
						});
					}
				}
			}

			return result;
		}
	}
}
