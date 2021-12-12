namespace Fluxera.Extensions.Http
{
	using System;
	using Common;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Microsoft.Extensions.Options;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///		Adds the required services for the http client.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddHttpClientService(this IServiceCollection services)
		{
			services.AddOptions();
			services.AddHashCalculator();
			services.AddHttpClient();

			return services;
		}

		public static IServiceCollection AddBaseAddressProvider<T>(this IServiceCollection services)
			where T : class, IBaseAddressProvider
		{
			services.TryAddSingleton<IBaseAddressProvider, T>();

			return services;
		}

		public static IServiceCollection AddHttpClientService<TService>(this IServiceCollection services,
			string remoteServiceName,
			Func<HttpClientServiceConfigurationContext, TService> factory)
			where TService : class, IHttpClientService
		{
			Guard.Against.NullOrEmpty(remoteServiceName, nameof(remoteServiceName));
			Guard.Against.Null(factory, nameof(factory));

			services.TryAddHttpClientService(remoteServiceName, factory);
			return services;
		}

		public static IServiceCollection AddHttpClientService<TService>(this IServiceCollection services,
			Func<HttpClientServiceConfigurationContext, TService> factory)
			where TService : class, IHttpClientService
		{
			Guard.Against.Null(factory, nameof(factory));

			services.TryAddHttpClientService(Options.DefaultName, factory);
			return services;
		}

		private static IServiceCollection TryAddHttpClientService<TService>(this IServiceCollection services,
			string remoteServiceName,
			Func<HttpClientServiceConfigurationContext, TService> factory)
			where TService : class, IHttpClientService
		{
			Guard.Against.Null(factory, nameof(factory));

			services.TryAddTransient(serviceProvider =>
			{
				HttpClientServiceConfigurationContext context = new HttpClientServiceConfigurationContext(remoteServiceName, serviceProvider);
				return factory.Invoke(context);
			});

			return services;
		}
	}
}
