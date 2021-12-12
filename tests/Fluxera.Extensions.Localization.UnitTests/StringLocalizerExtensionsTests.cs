namespace Fluxera.Extensions.Localization.UnitTests
{
	using System;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Localization;
	using NUnit.Framework;
	using FluentAssertions;

	[TestFixture]
	public class StringLocalizerExtensionsTests : TestBase
	{
		[Test]
		public void ShouldGetStringEx()
		{
			IServiceProvider serviceProvider = BuildServiceProvider(services =>
			{
				services.AddLogging();
				services.AddLocalization(options =>
				{
					options.ResourcesPath = "Resources";
				});
			});

			IStringLocalizerFactory stringLocalizerFactory = serviceProvider.GetRequiredService<IStringLocalizerFactory>();
			IStringLocalizer stringLocalizer = stringLocalizerFactory.Create(typeof(TestClass));

			stringLocalizer.GetStringEx("TestKey").Should().Be("TestTestTest");
		}
	}
}
