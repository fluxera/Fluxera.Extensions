namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[UsedImplicitly]
	internal sealed class DateTimeOffsetProvider : IDateTimeOffsetProvider
	{
		/// <inheritdoc />
		public DateTimeOffset Now => DateTimeOffset.Now;

		/// <inheritdoc />
		public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
	}
}
