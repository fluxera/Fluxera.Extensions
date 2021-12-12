namespace Fluxera.Extensions.DependencyInjection
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public sealed class NamedScopedServiceBuilder<TService> : NamedServiceBuilder<TService>
		where TService : class
	{
		internal NamedScopedServiceBuilder(IServiceCollection services) 
			: base(services)
		{
		}

		public NamedScopedServiceBuilder<TService> AddNameFor<TImplementation>(string name)
			where TImplementation : class, TService
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			this.Services.AddScoped<TService, TImplementation>();
			this.Services.AddScoped<TImplementation>();
			this.AddTypeMap<TImplementation>(name);

			return this;
		}
	}
}
