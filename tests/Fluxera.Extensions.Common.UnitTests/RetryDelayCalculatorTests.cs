namespace Fluxera.Extensions.Common.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.Common;
	using Fluxera.Utilities.Extensions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class RetryDelayCalculatorTests 
	{
		private IRetryDelayCalculator retryDelayCalculator;

		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddRetryDelayCalculator();
			IServiceProvider serviceProvider = services.BuildServiceProvider();
			this.retryDelayCalculator = serviceProvider.GetRequiredService<IRetryDelayCalculator>();
		}

		[Test]
		public void Should_Calculate_Exponential_Delay()
		{
			// Act
			5.Times(i =>
			{
				// Assert
				TimeSpan delay = this.retryDelayCalculator.CalculateDelay(++i,
					4096,
					maximumNumberOfSlotsWhenTruncated: 32);
				Console.WriteLine($"{delay}");
			});
		}

		[Test]
		public void Should_Throw_ArgumentException_If_MaximumNumberOfSlotsWhenTruncated_Is_Less_Than_One()
		{
			// Act
			Action action = () => this.retryDelayCalculator.CalculateDelay(1, maximumNumberOfSlotsWhenTruncated: 0);

			// Assert
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void Should_Throw_ArgumentException_If_MillisecondsPerSlot_Is_Less_Than_One()
		{
			// Act
			Action action = () => this.retryDelayCalculator.CalculateDelay(1, 0);

			// Assert
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void Should_Throw_ArgumentException_If_NumberOfAttempt_Is_Less_Than_One()
		{
			// Act
			Action action = () => this.retryDelayCalculator.CalculateDelay(0);

			// Assert
			action.Should().Throw<ArgumentException>();
		}
	}
}
