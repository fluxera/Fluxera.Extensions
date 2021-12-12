namespace Fluxera.Extensions.DependencyInjection.UnitTests.Model
{
	using System;

	public interface IDisposableService : IDisposable
	{
		bool IsDisposed { get; }
	}
}
