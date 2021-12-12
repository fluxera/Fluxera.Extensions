namespace Fluxera.Extensions.DependencyInjection
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

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

			this.Services.AddSingleton<TService, TImplementation>();
			this.Services.AddSingleton<TImplementation>();
			this.AddTypeMap<TImplementation>(name);

			return this;
		}
	}
}
