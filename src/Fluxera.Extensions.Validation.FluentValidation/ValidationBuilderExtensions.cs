namespace Fluxera.Extensions.Validation.FluentValidation
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ValidationBuilderExtensions
	{
		///  <summary>
		/// 		Add the <see cref="IValidatorFactory"/> for FluentValidation.
		///  </summary>
		///  <param name="builder">The builder.</param>
		///  <param name="configure">Action to configure the registered validators.</param>
		///  <returns>The builder.</returns>
		public static ValidationBuilder AddFluentValidation(this ValidationBuilder builder, Action<ValidatorRegistration> configure)
		{
			Guard.Against.Null(builder, nameof(builder));
			Guard.Against.Null(configure, nameof(configure));

			builder.AddValidatorFactory<FluentValidationValidatorFactory>();

			configure.Invoke(new ValidatorRegistration(builder.Services));

			return builder;
		}

		///  <summary>
		/// 		Add the <see cref="IValidatorFactory"/> for FluentValidation.
		///  </summary>
		///  <param name="builder">The builder.</param>
		///  <param name="name">The name of the validator factory.</param>
		///  <param name="configure">Action to configure the registered validators.</param>
		///  <returns>The builder.</returns>
		public static ValidationBuilder AddFluentValidation(this ValidationBuilder builder, string name, Action<ValidatorRegistration> configure)
		{
			Guard.Against.Null(builder, nameof(builder));
			Guard.Against.Null(configure, nameof(configure));

			builder.AddValidatorFactoryNamed<FluentValidationValidatorFactory>(name);

			configure.Invoke(new ValidatorRegistration(builder.Services));

			return builder;
		}
	}
}
