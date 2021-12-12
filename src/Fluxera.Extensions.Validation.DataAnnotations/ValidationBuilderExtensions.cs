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
		public static ValidationBuilder AddDataAnnotations(this ValidationBuilder builder)
		{
			builder.AddValidatorFactory<DataAnnotationsValidationValidatorFactory>();

			return builder;
		}
	}
}
