namespace Fluxera.Extensions.DependencyInjection
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

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

			this.Services.TryAddTransient<TService, TImplementation>();
			this.Services.TryAddTransient<TImplementation>();
			this.AddTypeMap<TImplementation>(name);

			return this;
		}
	}
}
