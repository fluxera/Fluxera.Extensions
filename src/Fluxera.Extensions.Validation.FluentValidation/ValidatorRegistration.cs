namespace Fluxera.Extensions.Validation.FluentValidation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using global::FluentValidation;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A registrar for validators.
	/// </summary>
	[PublicAPI]
	public sealed class ValidatorRegistration
	{
		private readonly IServiceCollection services;

		internal ValidatorRegistration(IServiceCollection services)
		{
			this.services = services;
		}

		/// <summary>
		///     Adds all validators available in the given assemblies.
		/// </summary>
		/// <param name="assemblies">The assemblies to scan.</param>
		/// <returns>The registration.</returns>
		public ValidatorRegistration AddValidators(IEnumerable<Assembly> assemblies)
		{
			assemblies ??= Enumerable.Empty<Assembly>();

			foreach(Assembly assembly in assemblies)
			{
				this.AddValidators(assembly);
			}

			return this;
		}

		/// <summary>
		///     Adds all validators available in the assembly.
		/// </summary>
		/// <param name="assembly">The assembly to scan.</param>
		/// <returns>The registration.</returns>
		public ValidatorRegistration AddValidators(Assembly assembly)
		{
			Guard.Against.Null(assembly, nameof(assembly));

			// Register FluentValidation validators.
			IEnumerable<Type> types = assembly.GetTypes().Where(type => !type.IsInterface && !type.IsAbstract && type.Implements<IValidator>());
			foreach(Type validatorType in types)
			{
				this.AddValidator(validatorType);
			}

			return this;
		}

		/// <summary>
		///     Adds all validator types.
		/// </summary>
		/// <param name="types">The assemblies to scan.</param>
		/// <returns>The registration.</returns>
		public ValidatorRegistration AddValidators(IEnumerable<Type> types)
		{
			types ??= Enumerable.Empty<Type>();

			foreach(Type type in types)
			{
				this.AddValidator(type);
			}

			return this;
		}

		/// <summary>
		///     Adds the validator type.
		/// </summary>
		/// <param name="validatorType">The validator type.</param>
		/// <returns>The registration.</returns>
		public ValidatorRegistration AddValidator(Type validatorType)
		{
			Guard.Against.Null(validatorType, nameof(validatorType));

			foreach(Type interfaceType in validatorType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>)))
			{
				this.services.TryAddTransient(interfaceType, validatorType);
				this.services.TryAddTransient(validatorType);
			}

			return this;
		}

		/// <summary>
		///     Adds the validator type.
		/// </summary>
		/// <typeparam name="TValidator">The validator type.</typeparam>
		/// <returns>The registration.</returns>
		public ValidatorRegistration AddValidator<TValidator>() where TValidator : IValidator
		{
			this.AddValidator(typeof(TValidator));

			return this;
		}
	}
}
