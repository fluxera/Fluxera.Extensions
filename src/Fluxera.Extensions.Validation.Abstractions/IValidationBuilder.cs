namespace Fluxera.Extensions.Validation
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public interface IValidationBuilder
	{
		IValidationBuilder AddValidatorFactory<T>() where T : class, IValidatorFactory;

		IValidationBuilder AddValidatorFactoryNamed<T>(string name) where T : class, IValidatorFactory;

		IServiceCollection Services { get; }
	}
}
