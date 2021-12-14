namespace Fluxera.Extensions.Validation.DataAnnotations
{
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ValidationBuilderExtensions
	{
		/// <summary>
		///		Add the <see cref="IValidatorFactory"/> for data annotations.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>The builder.</returns>
		public static IValidationBuilder AddDataAnnotations(this IValidationBuilder builder)
		{
			builder.AddValidatorFactory<DataAnnotationsValidationValidatorFactory>();

			return builder;
		}

		///  <summary>
		/// 		Add the <see cref="IValidatorFactory"/> for data annotations.
		///  </summary>
		///  <param name="builder">The builder.</param>
		///  <param name="name">The name of the validator factory.</param>
		///  <returns>The builder.</returns>
		public static IValidationBuilder AddDataAnnotations(this IValidationBuilder builder, string name)
		{
			builder.AddValidatorFactoryNamed<DataAnnotationsValidationValidatorFactory>(name);

			return builder;
		}
	}
}
