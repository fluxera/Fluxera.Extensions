namespace Fluxera.Extensions.DependencyInjection.UnitTests.Model
{
	public class DisposableServiceDecorator : IDisposableService
	{
		public DisposableServiceDecorator(IDisposableService innerService)
		{
			this.InnerService = innerService;
		}

		public IDisposableService InnerService { get; }

		public bool IsDisposed { get; private set; }

		public void Dispose()
		{
			this.InnerService.Dispose();
			this.IsDisposed = true;
		}
	}
}