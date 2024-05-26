namespace Fluxera.Extensions.DependencyInjection
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A builder to help configure named services.
	/// </summary>
	/// <typeparam name="TService"></typeparam>
	[PublicAPI]
	public sealed class NamedScopedServiceBuilder<TService> : NamedServiceBuilder<TService>
		where TService : class
	{
		internal NamedScopedServiceBuilder(IServiceCollection services)
			: base(services)
		{
		}

		/// <summary>
		///     Add the named implementation of the <typeparamref name="TService" /> type.
		/// </summary>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public NamedScopedServiceBuilder<TService> AddNameFor<TImplementation>(string name)
			where TImplementation : class, TService
		{
			Guard.ThrowIfNullOrWhiteSpace(name);

			this.Services.TryAddScoped<TService, TImplementation>();
			this.Services.TryAddScoped<TImplementation>();
			this.AddTypeMap<TImplementation>(name);

			return this;
		}
	}
}
