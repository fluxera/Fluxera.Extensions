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
			: this("The validation failed. See validation errors for details.", errors)
		{
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="ValidationException" /> type.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="errors"></param>
		public ValidationException(string message, ICollection<ValidationError> errors) : base(message)
		{
			this.Errors = Guard.Against.Null(errors);
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="ValidationException" /> type.
		/// </summary>
		/// <param name="innerException"></param>
		/// <param name="errors"></param>
		/// <param name="message"></param>
		public ValidationException(string message, Exception innerException, ICollection<ValidationError> errors)
			: base(message, innerException)
		{
			this.Errors = Guard.Against.Null(errors);
		}

		/// <summary>
		///     Gets the error messages.
		/// </summary>
		public ICollection<ValidationError> Errors { get; }
	}
}
