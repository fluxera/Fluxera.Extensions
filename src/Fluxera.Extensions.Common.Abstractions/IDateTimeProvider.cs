namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     Wrapper service for static <see cref="DateTime" /> properties.
	/// </summary>
	[PublicAPI]
	public interface IDateTimeProvider
	{
		/// <summary>
		///     <inheritdoc cref="DateTime.Now" />
		///     Gets <see cref="DateTime.Now" />.
		/// </summary>
		DateTime Now { get; }

		/// <summary>
		///     <inheritdoc cref="DateTime.UtcNow" />
		///     Gets <see cref="DateTime.UtcNow" />.
		/// </summary>
		DateTime UtcNow { get; }

		/// <summary>
		///     <inheritdoc cref="DateTime.Today" />
		///     Gets <see cref="DateTime.Today" />.
		/// </summary>
		DateTime Today { get; }
	}
}
