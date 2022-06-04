namespace Fluxera.Extensions.Validation
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the validators.
	/// </summary>
	[PublicAPI]
	public static class ValidationExtensions
	{
		/// <summary>
		///     Validates all given validators ans returns a cumulative <see cref="ValidationResult" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="validators"></param>
		/// <param name="item"></param>
		/// <returns></returns>
		public static async Task<ValidationResult> ValidateAsync<T>(this IEnumerable<IValidator> validators, T item)
			where T : class
		{
			ValidationResult result = new ValidationResult();

			foreach(IValidator validator in validators)
			{
				ValidationResult validationResult = await validator.ValidateAsync(item);
				result.ValidationErrors.AddRange(validationResult.ValidationErrors);
			}

			return result;
		}
	}
}
