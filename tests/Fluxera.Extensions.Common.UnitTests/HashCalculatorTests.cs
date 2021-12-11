namespace Fluxera.Extensions.Common.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.Common;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class HashCalculatorTests
	{
		private IHashCalculator hashCalculator;

		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddHashCalculator();
			IServiceProvider serviceProvider = services.BuildServiceProvider();
			this.hashCalculator = serviceProvider.GetRequiredService<IHashCalculator>();
		}

		[Test]
		public void ShouldCalculateHash()
		{
			// Act
			string hash = this.hashCalculator.ComputeHash("Hallo");

			// Assert
			hash.Should().NotBeNullOrWhiteSpace();
			hash.Should().Be("D1BF93299DE1B68E6D382C893BF1215F");
		}

		[Test]
		public void ShouldCalculateHash_InputEmpty()
		{
			// Act
			string hash = this.hashCalculator.ComputeHash(string.Empty);

			// Assert
			hash.Should().NotBeNullOrWhiteSpace();
			hash.Should().Be("D41D8CD98F00B204E9800998ECF8427E");
		}

		[Test]
		public void ShouldThrow_InputNull()
		{
			// Act
			Action action = () => this.hashCalculator.ComputeHash(null);

			// Assert
			action.Should().Throw<ArgumentNullException>();
		}
	}
}
