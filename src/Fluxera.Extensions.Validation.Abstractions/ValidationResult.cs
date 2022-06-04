namespace Fluxera.Extensions.Validation
{
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;

	/// <summary>
	///     A class that represents the result of a validation.
	/// </summary>
	[PublicAPI]
	public sealed class ValidationResult
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ValidationResult" /> type.
		/// </summary>
		public ValidationResult()
		{
			this.ValidationErrors = new List<ValidationError>();
		}

		/// <summary>
		///     Gets a flag, indicating if the validation was successful.
		/// </summary>
		public bool IsValid => !this.ValidationErrors.Any();

		/// <summary>
		///     Gets the potential validation errors.
		/// </summary>
		public ICollection<ValidationError> ValidationErrors { get; }
	}
}
