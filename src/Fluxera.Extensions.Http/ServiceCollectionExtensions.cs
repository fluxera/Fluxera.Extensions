namespace Fluxera.Extensions.Http
{
	using System;
	using Fluxera.Extensions.Common;
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
		///     Adds a named HTTP client <typeparamref name="TService" /> to the services.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="remoteServiceName">The name of the remote service.</param>
		/// <param name="factory">The factory function that creates a service client instance.</param>
		/// <returns></returns>
		public static IHttpClientBuilder AddHttpClientService<TService, TImplementation>(this IServiceCollection services,
			string remoteServiceName,
			Func<HttpClientServiceConfigurationContext, IServiceProvider, TImplementation> factory)
			where TService : class
			where TImplementation : class, TService, IHttpClientService
		{
			Guard.ThrowIfNull(services);
			Guard.ThrowIfNullOrWhiteSpace(remoteServiceName);
			Guard.ThrowIfNull(factory);

			services.AddOptions();
			services.AddHttpClient();
			services.AddHashCalculator();

			return services
				.AddHttpClient<TService>(remoteServiceName)
				.AddTypedClient<TService>((httpClient, serviceProvider) =>
				{
					IOptions<RemoteServiceOptions> optionsWrapper = serviceProvider.GetRequiredService<IOptions<RemoteServiceOptions>>();
					RemoteService options = optionsWrapper.Value.RemoteServices[remoteServiceName];

					if(!string.IsNullOrWhiteSpace(options.BaseAddress))
					{
						httpClient.BaseAddress = new Uri(options.BaseAddress);
					}

					HttpClientServiceConfigurationContext context = new HttpClientServiceConfigurationContext(remoteServiceName, httpClient, options);
					return factory.Invoke(context, serviceProvider);
				});
		}

		/// <summary>
		///     Adds HTTP client <typeparamref name="TService" /> to the services.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="factory">The factory function that creates a service client instance.</param>
		/// <returns></returns>
		public static IHttpClientBuilder AddHttpClientService<TService, TImplementation>(this IServiceCollection services,
			Func<HttpClientServiceConfigurationContext, IServiceProvider, TImplementation> factory)
			where TService : class
			where TImplementation : class, TService, IHttpClientService
		{
			return services.AddHttpClientService<TService, TImplementation>(RemoteServices.DefaultRemoteServiceName, factory);
		}
	}
}
