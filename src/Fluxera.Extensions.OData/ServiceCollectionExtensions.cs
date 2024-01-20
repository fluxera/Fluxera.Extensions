namespace Fluxera.Extensions.OData
{
	using System;
	using Fluxera.Extensions.Common;
	using Fluxera.Extensions.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;
	using Simple.OData.Client;

	/// <summary>
	///     Extensions methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds a named OData client <typeparamref name="TService" /> to the services.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="remoteServiceName">The name of the remote service.</param>
		/// <param name="collectionName">The name of the OData collection.</param>
		/// <param name="factory">The factory function that creates a service client instance.</param>
		/// <returns></returns>
		public static IHttpClientBuilder AddODataClientService<TService, TImplementation>(this IServiceCollection services,
			string remoteServiceName,
			string collectionName,
			Func<ODataClientServiceConfigurationContext, IServiceProvider, TImplementation> factory)
			where TService : class
			where TImplementation : class, TService, IODataClientService
		{
			Guard.Against.Null(services);
			Guard.Against.NullOrWhiteSpace(remoteServiceName);
			Guard.Against.NullOrWhiteSpace(collectionName);
			Guard.Against.Null(factory);

			services.AddOptions();
			services.AddHttpClient();
			services.AddHashCalculator();
			services.AddTransient<IODataClientFactory, ODataClientFactory>();
			services.AddTransient<IODataClientSettingsFactory, ODataClientSettingsFactory>();

			return services
				.AddHttpClient<TService>(remoteServiceName)
				.AddTypedClient<TService>((httpClient, serviceProvider) =>
				{
					IOptions<RemoteServiceOptions> optionsWrapper = serviceProvider.GetRequiredService<IOptions<RemoteServiceOptions>>();
					RemoteService options = optionsWrapper.Value.RemoteServices[remoteServiceName];

					httpClient.BaseAddress = new Uri(options.BaseAddress);

					IODataClientSettingsFactory oDataClientSettingsFactory = serviceProvider.GetRequiredService<IODataClientSettingsFactory>();
					ODataClientSettings oDataClientSettings = oDataClientSettingsFactory.CreateSettings(remoteServiceName, httpClient);

					IODataClientFactory oDataClientFactory = serviceProvider.GetRequiredService<IODataClientFactory>();
					IODataClient oDataClient = oDataClientFactory.CreateClient(remoteServiceName, oDataClientSettings);

					ODataClientServiceConfigurationContext context = new ODataClientServiceConfigurationContext(remoteServiceName, collectionName, httpClient, oDataClient, options);
					return factory.Invoke(context, serviceProvider);
				});
		}

		/// <summary>
		///     Adds a named OData client <typeparamref name="TService" /> to the services.
		/// </summary>
		/// <typeparam name="TService"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="collectionName">The name of the OData collection.</param>
		/// <param name="factory">The factory function that creates a service client instance.</param>
		/// <returns></returns>
		public static IHttpClientBuilder AddODataClientService<TService, TImplementation>(this IServiceCollection services,
			string collectionName,
			Func<ODataClientServiceConfigurationContext, IServiceProvider, TImplementation> factory)
			where TService : class
			where TImplementation : class, TService, IODataClientService
		{
			return services.AddODataClientService<TService, TImplementation>(RemoteServices.DefaultRemoteServiceName, collectionName, factory);
		}
	}
}
