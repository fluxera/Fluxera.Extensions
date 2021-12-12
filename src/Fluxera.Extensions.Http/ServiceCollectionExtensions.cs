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
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddHttp(this IServiceCollection services)
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
			Func<IHttpClientServiceConfigurationContext, TService> factory)
			where TService : class, IHttpClientService
		{
			Guard.Against.NullOrEmpty(remoteServiceName, nameof(remoteServiceName));
			Guard.Against.Null(factory, nameof(factory));

			services.TryAddApplicationServiceTransient(null, factory);
			return services;
		}

		public static IServiceCollection AddApplicationService<TService>(this IServiceCollection services,
			Func<IHttpClientServiceConfigurationContext, TService> factory)
			where TService : class, IHttpClientService
		{
			Guard.Against.Null(factory, nameof(factory));

			services.TryAddApplicationServiceTransient(Options.DefaultName, factory);
			return services;
		}

		private static IServiceCollection TryAddApplicationServiceTransient<TService>(this IServiceCollection services,
			string remoteServiceName,
			Func<IHttpClientServiceConfigurationContext, TService> factory)
			where TService : class, IHttpClientService
		{
			Guard.Against.Null(factory, nameof(factory));

			services.TryAddTransient(serviceProvider =>
			{
				IHttpClientServiceConfigurationContext context = new HttpClientServiceConfigurationContext
				{
					RemoteServiceName = remoteServiceName,
					ServiceProvider = serviceProvider,
				};

				return factory.Invoke(context);
			});

			return services;
		}
	}
}
