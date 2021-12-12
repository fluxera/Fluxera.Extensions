namespace Fluxera.Extensions.DependencyInjection.UnitTests.Model
{
	using System;

	public class AnotherTestService : ITestService
	{
		public AnotherTestService()
		{
			Console.WriteLine("Created AnotherTestService");
		}
	}
}
