namespace Fluxera.Extensions.DependencyInjection.UnitTests.Model
{
	using System;

	public class TestService : ITestService
	{
		public TestService()
		{
			Console.WriteLine("Created TestService");
		}
	}
}
