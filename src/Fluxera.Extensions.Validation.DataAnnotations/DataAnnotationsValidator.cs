namespace Fluxera.Extensions.Validation.DataAnnotations
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Validation;
	using ValidationResult = Fluxera.Extensions.Validation.ValidationResult;
	using DataAnnotationsValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

	/// <summary>
	///		A <see cref="IValidator"/> that uses data annotations to validate instances.
	/// </summary>
	internal sealed class DataAnnotationsValidator : IValidator
	{
		public Task<ValidationResult> ValidateAsync(object entity)
		{
			ValidationResult result = new ValidationResult();

			ICollection<DataAnnotationsValidationResult> validationResults = new List<DataAnnotationsValidationResult>();
			Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults);

			foreach (DataAnnotationsValidationResult validationResult in validationResults)
			{
				result.ValidationErrors.Add(new ValidationError(validationResult.MemberNames.FirstOrDefault())
				{
					ErrorMessages =
					{
						validationResult.ErrorMessage,
					},
				});
			}

			return Task.FromResult(result);
		}
	}
}
