namespace Fluxera.Extensions.DependencyInjection.UnitTests.Model
{
	public class DisposableService : IDisposableService
	{
		public virtual void Dispose()
		{
			this.IsDisposed = true;
		}

		public bool IsDisposed { get; private set; }
	}
}