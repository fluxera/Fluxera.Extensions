namespace Fluxera.Extensions.Validation
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.Serialization;
	using JetBrains.Annotations;

	[PublicAPI]
	[Serializable]
	public sealed class ValidationException : Exception
	{
		public ValidationException()
		{
		}

		public ValidationException(string message) : base(message)
		{
		}

		public ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public ValidationException(string message, Exception? innerException) : base(message, innerException)
		{
		}

		public ICollection<ValidationError> Errors { get; } = new List<ValidationError>();
	}
}
