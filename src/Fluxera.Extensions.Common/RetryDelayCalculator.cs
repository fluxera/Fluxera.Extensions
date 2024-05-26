namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[UsedImplicitly]
	internal sealed class RetryDelayCalculator : IRetryDelayCalculator
	{
		private readonly IJitterCalculator jitterCalculator;

		public RetryDelayCalculator(IJitterCalculator jitterCalculator)
		{
			this.jitterCalculator = jitterCalculator;
		}

		/// <inheritdoc />
		public TimeSpan CalculateDelay(
			int numberOfAttempt,
			int millisecondsPerSlot = 32,
			bool truncateNumberOfSlots = true,
			int maximumNumberOfSlotsWhenTruncated = 16,
			int jitterPercentage = 25)
		{
			Guard.ThrowIfFalse(numberOfAttempt >= 1, nameof(numberOfAttempt),
				"The number of attempt per slot needs to be 1 or larger.");

			Guard.ThrowIfFalse(millisecondsPerSlot >= 1, nameof(millisecondsPerSlot),
				"The milliseconds per slot needs to be 1 or larger.");

			Guard.ThrowIfFalse(maximumNumberOfSlotsWhenTruncated >= 1, nameof(maximumNumberOfSlotsWhenTruncated),
				"The maximum number of slots when truncated needs to be 1 or larger.");

			Guard.ThrowIfFalse(jitterPercentage >= 0 && jitterPercentage <= 100, nameof(jitterPercentage),
				"The percentage should be larger than 0 and smaller than 100.");

			// Binary exponential back-off.
			double numberOfSlots = Math.Pow(2, numberOfAttempt) - 1;

			// Truncate if 'truncateNumberOfSlots' is true, otherwise cap at int.MaxValue.
			int maximumNumberOfSlots = truncateNumberOfSlots ? maximumNumberOfSlotsWhenTruncated : int.MaxValue;
			int numberOfSlotsAsInteger =
				numberOfSlots > maximumNumberOfSlots ? maximumNumberOfSlots : (int)numberOfSlots;

			// Multiply slots times 'millisecondsPerSlot'.
			int delayMilliseconds = numberOfSlotsAsInteger * millisecondsPerSlot;

			// Apply jitter to the resulting time in order to increase entropy in your system
			int delayMillisecondsWithJitter = jitterCalculator.Apply(delayMilliseconds, 20);

			TimeSpan delay = TimeSpan.FromMilliseconds(delayMillisecondsWithJitter);
			return delay;
		}
	}
}
