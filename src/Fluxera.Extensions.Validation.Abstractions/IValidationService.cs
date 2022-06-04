namespace Fluxera.Extensions.Validation
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     Service for manually validating a single instance.
	/// </summary>
	[PublicAPI]
	public interface IValidationService
	{
		/// <summary>
		///     Validates the given object and returns a <see cref="ValidationResult" /> instance.
		/// </summary>
		/// <typeparam name="T">The type.</typeparam>
		/// <param name="item">The instance to validate.</param>
		/// <returns>The validation result.</returns>
		Task<ValidationResult> ValidateAsync<T>(T item) where T : class;

		/// <summary>
		///     Validates the given object and throws a <see cref="ValidationException" /> if the validation failed.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		/// <exception cref="ValidationException"></exception>
		Task ThrowOnValidateAsync<T>(T item) where T : class;
	}
}
