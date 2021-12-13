namespace Fluxera.Extensions.DependencyInjection
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	public sealed class NamedSingletonServiceBuilder<TService> : NamedServiceBuilder<TService>
		where TService : class
	{
		internal NamedSingletonServiceBuilder(IServiceCollection services) 
			: base(services)
		{
		}

		public NamedSingletonServiceBuilder<TService> AddNameFor<TImplementation>(string name)
			where TImplementation : class, TService
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			this.Services.TryAddSingleton<TService, TImplementation>();
			this.Services.TryAddSingleton<TImplementation>();
			this.AddTypeMap<TImplementation>(name);

			return this;
		}
	}
}
