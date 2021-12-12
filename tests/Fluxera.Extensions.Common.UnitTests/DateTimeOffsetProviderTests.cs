namespace Fluxera.Extensions.Common.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.Common;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class DateTimeOffsetProviderTests
	{
		private IDateTimeOffsetProvider dateTimeOffsetProvider;

		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddDateTimeOffsetProvider();
			IServiceProvider serviceProvider = services.BuildServiceProvider();
			this.dateTimeOffsetProvider = serviceProvider.GetRequiredService<IDateTimeOffsetProvider>();
		}

		[Test]
		public void ShouldBeNow()
		{
			// Act
			DateTimeOffset now = this.dateTimeOffsetProvider.Now;

			// Assert
			DateTimeOffset dt = DateTimeOffset.Now;
			now.Year.Should().Be(dt.Year);
			now.Month.Should().Be(dt.Month);
			now.Day.Should().Be(dt.Day);
			now.Hour.Should().Be(dt.Hour);
			now.Minute.Should().Be(dt.Minute);
			now.Second.Should().Be(dt.Second);
		}

		[Test]
		public void ShouldBeUtcNow()
		{
			// Act
			DateTimeOffset now = this.dateTimeOffsetProvider.UtcNow;

			// Assert
			DateTimeOffset dt = DateTimeOffset.UtcNow;
			now.Year.Should().Be(dt.Year);
			now.Month.Should().Be(dt.Month);
			now.Day.Should().Be(dt.Day);
			now.Hour.Should().Be(dt.Hour);
			now.Minute.Should().Be(dt.Minute);
			now.Second.Should().Be(dt.Second);
		}
	}
}
