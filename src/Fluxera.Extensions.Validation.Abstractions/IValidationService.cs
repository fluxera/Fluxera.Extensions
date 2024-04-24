namespace Fluxera.Extensions.Validation
{
	using System.Threading;
	using System.Threading.Tasks;
	using FluentValidation;
	using FluentValidation.Results;
	using JetBrains.Annotations;

	/// <summary>
	///     Service for manually validating a single instance.
	/// </summary>
	[PublicAPI]
	public interface IValidationService
	{
		/// <summary>
		///		Validates the given object and returns a <see cref="ValidationResult" /> instance.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		public ValidationResult Validate<T>(T item);

		/// <summary>
		///		Validates the given object and throws a <see cref="ValidationException" /> if invalid.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <exception cref="ValidationException"></exception>
		public void ValidateAndThrow<T>(T item);

		///  <summary>
		/// 		Validates the given object and returns a <see cref="ValidationResult" /> instance.
		///  </summary>
		///  <typeparam name="T"></typeparam>
		///  <param name="item"></param>
		///  <param name="cancellationToken"></param>
		///  <returns></returns>
		public Task<ValidationResult> ValidateAsync<T>(T item, CancellationToken cancellationToken = default);

		///  <summary>
		/// 		Validates the given object and throws a <see cref="ValidationException" /> if invalid.
		///  </summary>
		///  <typeparam name="T"></typeparam>
		///  <param name="item"></param>
		///  <param name="cancellationToken"></param>
		///  <exception cref="ValidationException"></exception>
		public Task ValidateAndThrowAsync<T>(T item, CancellationToken cancellationToken = default);
	}
}
