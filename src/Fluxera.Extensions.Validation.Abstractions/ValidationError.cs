namespace Fluxera.Extensions.Validation
{
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///		A class representing a validation error.
	/// </summary>
	[PublicAPI]
	public sealed class ValidationError
	{
		public ValidationError(string? propertyName)
		{
			this.PropertyName = propertyName ?? string.Empty;
			this.ErrorMessages = new List<string>();
		}

		/// <summary>
		///		Gets the property that the errors is for, or <see cref="string.Empty"/>
		///		when it's a class level error.
		/// </summary>
		public string PropertyName { get; }

		/// <summary>
		///		Gets the error messages associated this this validation error.
		/// </summary>
		public ICollection<string> ErrorMessages { get; set; }
	}
}
