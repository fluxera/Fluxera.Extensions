namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[UsedImplicitly]
	internal sealed class DateTimeProvider : IDateTimeProvider
	{
		/// <inheritdoc />
		public DateTime Now => DateTime.Now;

		/// <inheritdoc />
		public DateTime UtcNow => DateTime.UtcNow;

		/// <inheritdoc />
		public DateTime Today => DateTime.Today;
	}
}
