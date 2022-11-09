namespace Fluxera.Extensions.DependencyInjection.UnitTests.Model
{
	public class Decorator : IDecoratedService
	{
		public Decorator(IDecoratedService innerService, ITestService injectedService = null)
		{
			this.InnerService = innerService;
			this.InjectedService = injectedService;
		}

		public IDecoratedService InnerService { get; }

		public ITestService InjectedService { get; }
	}
}
