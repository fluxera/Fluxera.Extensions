﻿namespace Fluxera.Extensions.Validation
{
	using System.Collections.Generic;
	using FluentValidation;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using System.Reflection;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the validation service and adds the validators contained in the calling assembly.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddValidation(this IServiceCollection services)
		{
			Guard.ThrowIfNull(services);

			Assembly callingAssembly = Assembly.GetCallingAssembly();
			return services.AddValidation(callingAssembly);
		}

		/// <summary>
		///     Adds the validation service and adds the validators contained in the given assembly.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="validatorsAssembly">The assembly containing validators.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddValidation(this IServiceCollection services, Assembly validatorsAssembly)
		{
			Guard.ThrowIfNull(services);
			Guard.ThrowIfNull(validatorsAssembly);

			return services.AddValidation([validatorsAssembly]);
		}

		/// <summary>
		///     Adds the validation service and adds the validators contained in the given assemblies.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="validatorsAssemblies">The assemblies containing validators.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddValidation(this IServiceCollection services, params Assembly[] validatorsAssemblies)
		{
			Guard.ThrowIfNull(services);

			return services.AddValidation((IEnumerable<Assembly>)validatorsAssemblies);
		}

		/// <summary>
		///     Adds the validation service and adds the validators contained in the given assemblies.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="validatorsAssemblies">The assemblies containing validators.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddValidation(this IServiceCollection services, IEnumerable<Assembly> validatorsAssemblies)
		{
			Guard.ThrowIfNull(services);
			validatorsAssemblies = Guard.ThrowIfNullOrEmpty(validatorsAssemblies);

			// Register the validation service.
			services.TryAddTransient<IValidationService, ValidationService>();

			// Add the validators.
			services.AddValidatorsFromAssemblies(validatorsAssemblies, includeInternalTypes: true);

			return services;
		}
	}
}
