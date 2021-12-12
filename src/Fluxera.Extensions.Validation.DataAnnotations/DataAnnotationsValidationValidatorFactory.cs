namespace Fluxera.Extensions.Validation.DataAnnotations
{
	using JetBrains.Annotations;

	/// <summary>
	///		A <see cref="IValidatorFactory"/> that produces <see cref="DataAnnotationsValidator"/> instances. 
	/// </summary>
	[UsedImplicitly]
	internal sealed class DataAnnotationsValidationValidatorFactory : IValidatorFactory
	{
		/// <inheritdoc />
		public IValidator CreateValidator()
		{
			return new DataAnnotationsValidator();
		}
	}
}
