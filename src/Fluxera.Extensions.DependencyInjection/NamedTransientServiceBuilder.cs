namespace Fluxera.Extensions.DependencyInjection
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     A builder that helps to configure transient named services.
	/// </summary>
	/// <typeparam name="TService"></typeparam>
	[PublicAPI]
	public sealed class NamedTransientServiceBuilder<TService> : NamedServiceBuilder<TService>
		where TService : class
	{
		internal NamedTransientServiceBuilder(IServiceCollection services)
			: base(services)
		{
		}

		/// <summary>
		///     Adds an implementation of the <typeparamref name="TService" /> service as transient named service.
		/// </summary>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="name"></param>
		/// <returns></returns>
		public NamedTransientServiceBuilder<TService> AddNameFor<TImplementation>(string name)
			where TImplementation : class, TService
		{
			Guard.ThrowIfNullOrWhiteSpace(name);

			this.Services.TryAddTransient<TService, TImplementation>();
			this.Services.TryAddTransient<TImplementation>();
			this.AddTypeMap<TImplementation>(name);

			return this;
		}
	}
}
