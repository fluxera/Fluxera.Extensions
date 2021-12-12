namespace Fluxera.Extensions.Validation
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///		Adds the validation service and configures the underlying validation framework.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="configure">The action that configure the underlying validation framework.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddValidation(this IServiceCollection services, Action<ValidationBuilder> configure)
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(configure, nameof(configure));

			// Register validation service.
			services.TryAddTransient<IValidationService, ValidationService>();

			// Configure the validator(s) to use.
			configure.Invoke(new ValidationBuilder(services));

			return services;
		}
	}
}
