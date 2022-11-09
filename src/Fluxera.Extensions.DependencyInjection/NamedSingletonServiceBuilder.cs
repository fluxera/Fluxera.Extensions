namespace Fluxera.Extensions.DependencyInjection
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A builder that helps configuring named singletons.
	/// </summary>
	/// <typeparam name="TService"></typeparam>
	[PublicAPI]
	public sealed class NamedSingletonServiceBuilder<TService> : NamedServiceBuilder<TService>
		where TService : class
	{
		internal NamedSingletonServiceBuilder(IServiceCollection services)
			: base(services)
		{
		}

		/// <summary>
		///     Adds an implementation of the <typeparamref name="TService" /> service as named singleton.
		/// </summary>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
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
