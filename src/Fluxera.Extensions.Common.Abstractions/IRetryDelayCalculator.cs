namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     Calculate retry delay with (truncated) binary exponential back-off.
	/// </summary>
	[PublicAPI]
	public interface IRetryDelayCalculator
	{
		/// <summary>
		///     Per attempt the number of slots doubles.
		///     The number of slots either continuous to rise or is truncate if <paramref name="truncateNumberOfSlots" /> is
		///     <c>true</c>and the slots grow past <paramref name="truncateNumberOfSlots" />.
		/// </summary>
		/// <returns>The delay interval.</returns>
		/// <param name="numberOfAttempt">The xth attempt.</param>
		/// <param name="millisecondsPerSlot">With each attempt the amount of slots doubles. This is the time per slot.</param>
		/// <param name="truncateNumberOfSlots">Determines whether the exponential back-off truncates or continuous to expand.</param>
		/// <param name="maximumNumberOfSlotsWhenTruncated">
		///     If <paramref name="truncateNumberOfSlots" /> is <c>true</c> this maximizes
		///     the number of slots.
		/// </param>
		/// <param name="jitterPercentage">The percentage of jitter to add or remove randomly.</param>
		TimeSpan CalculateDelay(
			int numberOfAttempt,
			int millisecondsPerSlot = 32,
			bool truncateNumberOfSlots = true,
			int maximumNumberOfSlotsWhenTruncated = 16,
			int jitterPercentage = 25);
	}
}
