﻿namespace Fluxera.Extensions.Validation.FluentValidation
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the <see cref="IValidationBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class ValidationBuilderExtensions
	{
		/// <summary>
		///     Add the <see cref="IValidatorFactory" /> for FluentValidation.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="configure">Action to configure the registered validators.</param>
		/// <returns>The builder.</returns>
		public static IValidationBuilder AddFluentValidation(this IValidationBuilder builder, Action<ValidatorRegistration> configure)
		{
			Guard.Against.Null(builder, nameof(builder));
			Guard.Against.Null(configure, nameof(configure));

			builder.AddValidatorFactory<FluentValidationValidatorFactory>();

			configure.Invoke(new ValidatorRegistration(builder.Services));

			return builder;
		}

		/// <summary>
		///     Add the <see cref="IValidatorFactory" /> for FluentValidation.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="name">The name of the validator factory.</param>
		/// <param name="configure">Action to configure the registered validators.</param>
		/// <returns>The builder.</returns>
		public static IValidationBuilder AddFluentValidation(this IValidationBuilder builder, string name, Action<ValidatorRegistration> configure)
		{
			Guard.Against.Null(builder, nameof(builder));
			Guard.Against.Null(configure, nameof(configure));

			builder.AddValidatorFactoryNamed<FluentValidationValidatorFactory>(name);

			configure.Invoke(new ValidatorRegistration(builder.Services));

			return builder;
		}
	}
}
