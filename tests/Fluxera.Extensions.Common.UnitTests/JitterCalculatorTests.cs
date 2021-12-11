namespace Fluxera.Extensions.Common.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.Common;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class JitterCalculatorTests
	{
		private IJitterCalculator jitterCalculator;

		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddJitterCalculator();
			IServiceProvider serviceProvider = services.BuildServiceProvider();
			this.jitterCalculator = serviceProvider.GetRequiredService<IJitterCalculator>();
		}

		[Test]
		public void Should_Return_A_Number_Within_Percentage_Below_And_Above_Number_Double()
		{
			// Act
			double result = this.jitterCalculator.Apply(100d);

			// Assert
			result.Should().BeInRange(75d, 125d);
		}

		[Test]
		public void Should_Return_A_Number_Within_Percentage_Below_And_Above_Number_Integer()
		{
			// Act
			int result = this.jitterCalculator.Apply(100);

			// Assert
			result.Should().BeInRange(75, 125);
		}

		[Test]
		public void Should_Throw_An_ArgumentException_If_Percentage_Is_Too_High_Double()
		{
			// Act
			Action action = () => this.jitterCalculator.Apply(100d, 200);

			// Assert
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void Should_Throw_An_ArgumentException_If_Percentage_Is_Too_High_Integer()
		{
			// Act
			Action action = () => this.jitterCalculator.Apply(100, 200);

			// Assert
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void Should_Throw_An_ArgumentException_If_Percentage_Is_Too_Low_Double()
		{
			// Act
			Action action = () => this.jitterCalculator.Apply(100d, -10);

			// Assert
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void Should_Throw_An_ArgumentException_If_Percentage_Is_Too_Low_Integer()
		{
			// Act
			Action action = () => this.jitterCalculator.Apply(100, -10);

			// Assert
			action.Should().Throw<ArgumentException>();
		}
	}
}
