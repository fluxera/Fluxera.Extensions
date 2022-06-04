namespace Fluxera.Extensions.Validation
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     An exception that provides error messages.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ValidationException : Exception
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="ValidationException" /> type.
		/// </summary>
		/// <param name="errors"></param>
		public ValidationException(ICollection<ValidationError> errors)
		{
			this.Errors = Guard.Against.Null(errors);
		}

		/// <summary>
		///     Gets the error messages.
		/// </summary>
		public ICollection<ValidationError> Errors { get; }
	}
}
