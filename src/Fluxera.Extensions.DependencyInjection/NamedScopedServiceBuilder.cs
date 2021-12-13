namespace Fluxera.Extensions.DependencyInjection
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

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

			this.Services.TryAddScoped<TService, TImplementation>();
			this.Services.TryAddScoped<TImplementation>();
			this.AddTypeMap<TImplementation>(name);

			return this;
		}
	}
}
