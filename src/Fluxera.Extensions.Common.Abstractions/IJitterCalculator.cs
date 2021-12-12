namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     The Jitter class has methods to add entropy to any number.
	///     The degree of entropy is set by parameter percentage or by the percentage in the overloaded methods.
	/// </summary>
	[PublicAPI]
	public interface IJitterCalculator
	{
		/// <summary>
		///     Applies jitter to the <see cref="input" /> using the passed in percentage.
		/// </summary>
		/// <param name="input">An integer you want to apply jitter to.</param>
		/// <param name="percentage">The degree of entropy.</param>
		/// <returns>A random number between <see cref="input" /> +/- percentage.</returns>
		int Apply(int input, int percentage = 25);

		/// <summary>
		///     Applies jitter to the <see cref="input" /> using the passed in settings.
		/// </summary>
		/// <param name="input">A double you want to apply jitter to.</param>
		/// <param name="percentage">The degree of entropy.</param>
		/// <returns>A random number between <see cref="input" /> +/- percentage.</returns>
		double Apply(double input, int percentage = 25);

		/// <summary>
		///     Applies jitter to the <see cref="input" /> using the passed in settings.
		/// </summary>
		/// <param name="input">A time span you want to apply jitter to.</param>
		/// <param name="percentage">The degree of entropy.</param>
		/// <returns>A random number between <see cref="input" /> +/- percentage.</returns>
		TimeSpan Apply(TimeSpan input, int percentage = 25);
	}
}
