namespace Fluxera.Extensions.Common
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[UsedImplicitly]
	internal sealed class JitterCalculator : IJitterCalculator
	{
		private readonly Random random;

		/// <summary>
		///		Initializes a new instance of the <see cref="JitterCalculator"/> class.
		/// </summary>
		public JitterCalculator()
		{
			random = new Random();
		}

		/// <inheritdoc />
		public int Apply(int input, int percentage = 25)
		{
			Guard.Against.False(percentage >= 0 && percentage <= 100, nameof(percentage),
				"The percentage should be larger than 0 and smaller than 100.");

			int lowerBoundary = input * (100 - percentage) / 100;
			int upperBoundary = input * (100 + percentage) / 100;

			return random.Next(lowerBoundary, upperBoundary);
		}

		/// <inheritdoc />
		public double Apply(double input, int percentage = 25)
		{
			Guard.Against.False(percentage >= 0 && percentage <= 100, nameof(percentage),
				"The percentage should be larger than 0 and smaller than 100.");

			double lowerBoundary = input * (100 - percentage) / 100;
			double upperBoundary = input * (100 + percentage) / 100;

			return lowerBoundary + (upperBoundary - lowerBoundary) * random.NextDouble();
		}

		/// <inheritdoc />
		public TimeSpan Apply(TimeSpan input, int percentage = 25)
		{
			Guard.Against.False(percentage >= 0 && percentage <= 100, nameof(percentage),
				"The percentage should be larger than 0 and smaller than 100.");

			int milliseconds = (int)input.TotalMilliseconds;
			milliseconds = Apply(milliseconds, percentage);
			return TimeSpan.FromMilliseconds(milliseconds);
		}
	}
}
