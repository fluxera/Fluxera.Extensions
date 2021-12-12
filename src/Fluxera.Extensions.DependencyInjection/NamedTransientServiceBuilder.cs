namespace Fluxera.Extensions.DependencyInjection
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public sealed class NamedTransientServiceBuilder<TService> : NamedServiceBuilder<TService>
		where TService : class
	{
		internal NamedTransientServiceBuilder(IServiceCollection services) 
			: base(services)
		{
		}

		public NamedTransientServiceBuilder<TService> AddNameFor<TImplementation>(string name)
			where TImplementation : class, TService
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			this.Services.AddTransient<TService, TImplementation>();
			this.Services.AddTransient<TImplementation>();
			this.AddTypeMap<TImplementation>(name);

			return this;
		}
	}
}
