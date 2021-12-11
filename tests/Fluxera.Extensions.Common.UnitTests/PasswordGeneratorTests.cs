namespace Fluxera.Extensions.Common.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.Common;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class PasswordGeneratorTests
	{
		private IPasswordGenerator passwordGenerator;

		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddPasswordGenerator();
			IServiceProvider serviceProvider = services.BuildServiceProvider();
			this.passwordGenerator = serviceProvider.GetRequiredService<IPasswordGenerator>();
		}

		[Test]
		public void ShouldCreatePasswordWithCorrectLength()
		{
			// Act
			string password = this.passwordGenerator.GeneratePassword(10);

			// Assert
			password.Should().NotBeNullOrWhiteSpace();
			password.Length.Should().Be(10);
		}
	}
}
