namespace Fluxera.Extensions.Common.UnitTests
{
	using System;
	using FluentAssertions;
	using Fluxera.Extensions.Common;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class DateTimeProviderTests
	{
		private IDateTimeProvider dateTimeProvider;

		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddDateTimeProvider();
			IServiceProvider serviceProvider = services.BuildServiceProvider();
			this.dateTimeProvider = serviceProvider.GetRequiredService<IDateTimeProvider>();
		}

		[Test]
		public void ShouldBeNow()
		{
			// Act
			DateTime now = this.dateTimeProvider.Now;

			// Assert
			DateTime dt = DateTime.Now;
			now.Year.Should().Be(dt.Year);
			now.Month.Should().Be(dt.Month);
			now.Day.Should().Be(dt.Day);
			now.Hour.Should().Be(dt.Hour);
			now.Minute.Should().Be(dt.Minute);
			now.Second.Should().Be(dt.Second);
		}

		[Test]
		public void ShouldBeToday()
		{
			// Act
			DateTime now = this.dateTimeProvider.Today;

			// Assert
			DateTime dt = DateTime.Today;
			now.Year.Should().Be(dt.Year);
			now.Month.Should().Be(dt.Month);
			now.Day.Should().Be(dt.Day);
			now.Hour.Should().Be(0);
			now.Minute.Should().Be(0);
			now.Second.Should().Be(0);
			now.Millisecond.Should().Be(0);
		}

		[Test]
		public void ShouldBeUtcNow()
		{
			// Act
			DateTime now = this.dateTimeProvider.UtcNow;

			// Assert
			DateTime dt = DateTime.UtcNow;
			now.Year.Should().Be(dt.Year);
			now.Month.Should().Be(dt.Month);
			now.Day.Should().Be(dt.Day);
			now.Hour.Should().Be(dt.Hour);
			now.Minute.Should().Be(dt.Minute);
			now.Second.Should().Be(dt.Second);
		}
	}
}
