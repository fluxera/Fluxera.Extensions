namespace Fluxera.Extensions.DependencyInjection.UnitTests.Model
{
	public class Decorated : IDecoratedService
	{
		public Decorated(ITestService injectedService = null)
		{
			this.InjectedService = injectedService;
		}

		public ITestService InjectedService { get; }
	}
}
