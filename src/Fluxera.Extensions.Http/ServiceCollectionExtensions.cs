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
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="remoteServiceName">The name of the remote service.</param>
		/// <param name="factory">The factory function that creates a service client instance.</param>
		/// <returns></returns>
		public static IHttpClientBuilder AddHttpClientService<TService, TImplementation>(this IServiceCollection services,
			string remoteServiceName,
			Func<string, HttpClient, RemoteService, TImplementation> factory)
			where TService : class, IHttpClientService
			where TImplementation : class, TService
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.NullOrWhiteSpace(remoteServiceName, nameof(remoteServiceName));
			Guard.Against.Null(factory, nameof(factory));

			services.AddOptions();
			services.AddHttpClient();
			services.AddHashCalculator();

			return services
				.AddHttpClient<TService>(remoteServiceName)
				.AddTypedClient<TService>((httpClient, serviceProvider) =>
				{
					IOptions<RemoteServiceOptions> optionsWrapper = serviceProvider.GetRequiredService<IOptions<RemoteServiceOptions>>();
					RemoteService options = optionsWrapper.Value.RemoteServices[remoteServiceName];

					httpClient.BaseAddress = new Uri(options.BaseAddress);

					return factory.Invoke(remoteServiceName, httpClient, options);
				});
		}

		/// <summary>
		///     Adds HTTP client <see cref="TService" /> to the services.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="factory">The factory function that creates a service client instance.</param>
		/// <returns></returns>
		public static IHttpClientBuilder AddHttpClientService<TService, TImplementation>(this IServiceCollection services,
			Func<string, HttpClient, RemoteService, TImplementation> factory)
			where TService : class, IHttpClientService
			where TImplementation : class, TService
		{
			return services.AddHttpClientService<TService, TImplementation>(RemoteServices.DefaultRemoteServiceName, factory);
		}
	}
}
