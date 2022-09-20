namespace Fluxera.Extensions.Validation.FluentValidation
{
	using System;
	using System.Threading.Tasks;
	using global::FluentValidation;

	/// <summary>
	///     A <see cref="IValidator" /> that uses FluentValidation to validate instances.
	/// </summary>
	internal sealed class FluentValidationValidator : Fluxera.Extensions.Validation.IValidator
	{
		private readonly IServiceProvider serviceProvider;

		public FluentValidationValidator(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public async Task<ValidationResult> ValidateAsync(object entity)
		{
			ValidationResult result = new ValidationResult();

			Type validatorType = typeof(IValidator<>).MakeGenericType(entity.GetType());

			if(this.serviceProvider.GetService(validatorType) is IValidator validator)
			{
				global::FluentValidation.Results.ValidationResult validationResult =
					await validator.ValidateAsync(new global::FluentValidation.ValidationContext<object>(entity));

				if(!validationResult.IsValid)
				{
					foreach(global::FluentValidation.Results.ValidationFailure validationFailure in validationResult.Errors)
					{
						result.ValidationErrors.Add(new ValidationError(validationFailure.PropertyName)
						{
							ErrorMessages =
							{
								validationFailure.ErrorMessage,
							}
						});
					}
				}
			}

			return result;
		}
	}
}
