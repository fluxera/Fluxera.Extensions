namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using Fluxera.Guards;
	using Microsoft.Extensions.DependencyInjection;

	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the service implementations configured in the configure action for the <see cref="TService" /> type transient.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="configure">The name-type mapping configuration function.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddNamedTransient<TService>(this IServiceCollection services, Action<NamedTransientServiceBuilder<TService>> configure)
			where TService : class
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(configure, nameof(configure));

			NamedServiceMapper<TService> serviceMapper = services.GetSingletonInstanceOrDefault<NamedServiceMapper<TService>>();
			NamedTransientServiceBuilder<TService> builder = new NamedTransientServiceBuilder<TService>(services);
			configure.Invoke(builder);
			services.ReplaceSingleton(builder.BuildMapper(serviceMapper));

			return services;
		}

		/// <summary>
		///     Adds the service implementations configured in the configure action for the <see cref="TService" /> type singleton.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="configure">The name-type mapping configuration function.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddNamedSingleton<TService>(this IServiceCollection services, Action<NamedSingletonServiceBuilder<TService>> configure)
			where TService : class
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(configure, nameof(configure));

			NamedServiceMapper<TService> serviceMapper = services.GetSingletonInstanceOrDefault<NamedServiceMapper<TService>>();
			NamedSingletonServiceBuilder<TService> builder = new NamedSingletonServiceBuilder<TService>(services);
			configure.Invoke(builder);
			services.ReplaceSingleton(builder.BuildMapper(serviceMapper));

			return services;
		}

		/// <summary>
		///     Adds the service implementations configured in the configure action for the <see cref="TService" /> type scoped.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="configure">The name-type mapping configuration function.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddNamedScoped<TService>(this IServiceCollection services, Action<NamedScopedServiceBuilder<TService>> configure)
			where TService : class
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(configure, nameof(configure));

			NamedServiceMapper<TService> serviceMapper = services.GetSingletonInstanceOrDefault<NamedServiceMapper<TService>>();
			NamedScopedServiceBuilder<TService> builder = new NamedScopedServiceBuilder<TService>(services);
			configure.Invoke(builder);
			services.ReplaceSingleton(builder.BuildMapper(serviceMapper));

			return services;
		}
	}
}
