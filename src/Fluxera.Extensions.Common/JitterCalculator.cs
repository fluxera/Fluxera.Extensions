namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[UsedImplicitly]
	internal sealed class JitterCalculator : IJitterCalculator
	{
		/// <inheritdoc />
		public int Apply(int input, int percentage = 25)
		{
			Guard.ThrowIfFalse(percentage is >= 0 and <= 100, nameof(percentage),
				"The percentage should be larger than 0 and smaller than 100.");

			int lowerBoundary = input * (100 - percentage) / 100;
			int upperBoundary = input * (100 + percentage) / 100;

			return Random.Shared.Next(lowerBoundary, upperBoundary);
		}

		/// <inheritdoc />
		public double Apply(double input, int percentage = 25)
		{
			Guard.ThrowIfFalse(percentage is >= 0 and <= 100, nameof(percentage),
				"The percentage should be larger than 0 and smaller than 100.");

			double lowerBoundary = input * (100 - percentage) / 100;
			double upperBoundary = input * (100 + percentage) / 100;

			return lowerBoundary + (upperBoundary - lowerBoundary) * Random.Shared.NextDouble();
		}

		/// <inheritdoc />
		public TimeSpan Apply(TimeSpan input, int percentage = 25)
		{
			Guard.ThrowIfFalse(percentage is >= 0 and <= 100, nameof(percentage),
				"The percentage should be larger than 0 and smaller than 100.");

			int milliseconds = (int)input.TotalMilliseconds;
			milliseconds = Apply(milliseconds, percentage);
			return TimeSpan.FromMilliseconds(milliseconds);
		}
	}
}
