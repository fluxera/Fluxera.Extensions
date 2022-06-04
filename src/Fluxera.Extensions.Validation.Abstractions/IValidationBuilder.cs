namespace Fluxera.Extensions.Validation
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contract for a builder that configures the validation service.
	/// </summary>
	[PublicAPI]
	public interface IValidationBuilder
	{
		/// <summary>
		///     Gets the service collection.
		/// </summary>
		IServiceCollection Services { get; }

		/// <summary>
		///     Adds a validator factory.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		IValidationBuilder AddValidatorFactory<T>() where T : class, IValidatorFactory;

		/// <summary>
		///     Adds a named validator factory.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		IValidationBuilder AddValidatorFactoryNamed<T>(string name) where T : class, IValidatorFactory;
	}
}
