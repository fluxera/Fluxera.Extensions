namespace Fluxera.Extensions.Http
{
	using System;
	using System.Net.Http;
	using Fluxera.Extensions.Common;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     Extensions methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds the <see cref="IBaseAddressProvider" /> to be used.
		/// </summary>
		/// <typeparam name="TProvider"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns></returns>
		public static IServiceCollection AddBaseAddressProvider<TProvider>(this IServiceCollection services)
			where TProvider : class, IBaseAddressProvider
		{
			services.TryAddSingleton<IBaseAddressProvider, TProvider>();

			return services;
		}

		/// <summary>
		///     Adds the <see cref="IAccessTokenProvider" /> to be used.
		/// </summary>
		/// <typeparam name="TProvider"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns></returns>
		public static IServiceCollection AddAccessTokenProvider<TProvider>(this IServiceCollection services)
			where TProvider : class, IAccessTokenProvider
		{
			services.TryAddSingleton<IAccessTokenProvider, TProvider>();

			return services;
		}

		/// <summary>
		///     Adds a named HTTP client <see cref="TService" /> to the services.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="remoteServiceName">The name of the remote service.</param>
		/// <param name="factory">The factory function that creates a service client instance.</param>
		/// <returns></returns>
		public static IServiceCollection AddHttpClientService<TService>(this IServiceCollection services, string remoteServiceName,
			Func<HttpClientServiceCreationContext, TService> factory)
			where TService : class, IHttpClientService
		{
			services.TryAddHttpClientService(remoteServiceName, factory);
			return services;
		}

		/// <summary>
		///     Adds HTTP client <see cref="TService" /> to the services.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="factory">The factory function that creates a service client instance.</param>
		/// <returns></returns>
		public static IServiceCollection AddHttpClientService<TService>(this IServiceCollection services,
			Func<HttpClientServiceCreationContext, TService> factory)
			where TService : class, IHttpClientService
		{
			services.TryAddHttpClientService(RemoteServices.DefaultRemoteServiceName, factory);
			return services;
		}

		private static IServiceCollection TryAddHttpClientService<TService>(this IServiceCollection services, string remoteServiceName,
			Func<HttpClientServiceCreationContext, TService> factory)
			where TService : class, IHttpClientService
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.NullOrEmpty(remoteServiceName, nameof(remoteServiceName));
			Guard.Against.Null(factory, nameof(factory));

			services.AddOptions();
			services.AddHttpClient();
			services.AddHashCalculator();

			services.TryAddTransient(serviceProvider =>
			{
				IHttpClientFactory httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
				IOptions<RemoteServiceOptions> optionsWrapper = serviceProvider.GetRequiredService<IOptions<RemoteServiceOptions>>();
				HttpClientServiceCreationContext context = new HttpClientServiceCreationContext(remoteServiceName, httpClientFactory, optionsWrapper);
				return factory.Invoke(context);
			});

			return services;
		}
	}
}
