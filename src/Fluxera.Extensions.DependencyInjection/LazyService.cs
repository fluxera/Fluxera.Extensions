namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using Microsoft.Extensions.DependencyInjection;

	internal sealed class LazyService<T> : Lazy<T> where T : class
	{
		public LazyService(IServiceProvider provider) 
			: base(provider.GetRequiredService<T>)
		{
		}
	}
}
