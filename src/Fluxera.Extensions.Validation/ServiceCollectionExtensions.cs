﻿namespace Fluxera.Extensions.Validation
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the validation service and configures the underlying validation framework.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="configure">The action that configure the underlying validation framework.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddValidation(this IServiceCollection services, Action<IValidationBuilder> configure)
		{
			Guard.Against.Null(services);

			// Register validation service.
			services.TryAddTransient<IValidationService, ValidationService>();

			// Configure the validator(s) to use.
			configure?.Invoke(new ValidationBuilder(services));

			return services;
		}

		/// <summary>
		///     Adds the validation service and configures the underlying validation framework.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddValidation(this IServiceCollection services)
		{
			Guard.Against.Null(services);

			// Register validation service.
			services.TryAddTransient<IValidationService, ValidationService>();

			return services;
		}
	}
}
