namespace Fluxera.Extensions.Validation
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ValidationExtensions
	{
		public static async Task<ValidationResult> ValidateAsync<T>(this IEnumerable<IValidator> validators, T item)
			where T : class
		{
			ValidationResult result = new ValidationResult();

			foreach (IValidator validator in validators)
			{
				ValidationResult validationResult = await validator.ValidateAsync(item);
				result.ValidationErrors.AddRange(validationResult.ValidationErrors);
			}

			return result;
		}
	}
}
