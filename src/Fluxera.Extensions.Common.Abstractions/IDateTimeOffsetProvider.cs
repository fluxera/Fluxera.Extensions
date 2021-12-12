namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     Wrapper service for static <see cref="DateTimeOffset" /> properties.
	/// </summary>
	[PublicAPI]
	public interface IDateTimeOffsetProvider
	{
		/// <summary>
		///     <inheritdoc cref="DateTimeOffset.Now" />
		///     Gets <see cref="DateTimeOffset.Now" />.
		/// </summary>
		DateTimeOffset Now { get; }

		/// <summary>
		///     <inheritdoc cref="DateTimeOffset.UtcNow" />
		///     Gets <see cref="DateTimeOffset.UtcNow" />.
		/// </summary>
		DateTimeOffset UtcNow { get; }
	}
}
