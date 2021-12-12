namespace Fluxera.Extensions.OData
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Microsoft.Extensions.Options;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddODataClientService(this IServiceCollection services)
		{
			// TODO

			return services;
		}

		public static IServiceCollection AddCrudApplicationService<TService>(this IServiceCollection services,
			string remoteServiceName,
			string collectionName,
			Func<ODataServiceConfigurationContext, TService> factory)
			where TService : class, IODataClientService
		{
			Guard.Against.NullOrEmpty(remoteServiceName, nameof(remoteServiceName));
			Guard.Against.NullOrEmpty(collectionName, nameof(collectionName));
			Guard.Against.Null(factory, nameof(factory));

			services.TryAddCrudApplicationServiceTransient(remoteServiceName, collectionName, factory);
			return services;
		}

		public static IServiceCollection AddCrudApplicationService<TService>(this IServiceCollection services,
			string collectionName,
			Func<ODataServiceConfigurationContext, TService> factory)
			where TService : class, IODataClientService
		{
			Guard.Against.NullOrEmpty(collectionName, nameof(collectionName));
			Guard.Against.Null(factory, nameof(factory));

			services.TryAddCrudApplicationServiceTransient(Options.DefaultName, collectionName, factory);
			return services;
		}

		private static IServiceCollection TryAddCrudApplicationServiceTransient<TService>(
			this IServiceCollection services,
			string remoteServiceName,
			string collectionName,
			Func<ODataServiceConfigurationContext, TService> factory)
			where TService : class, IODataClientService
		{
			Guard.Against.NullOrEmpty(remoteServiceName, nameof(remoteServiceName));
			Guard.Against.NullOrEmpty(collectionName, nameof(collectionName));
			Guard.Against.Null(factory, nameof(factory));

			services.TryAddTransient(serviceProvider =>
			{
				ODataServiceConfigurationContext context = new ODataServiceConfigurationContext(remoteServiceName, collectionName, serviceProvider);
				return factory.Invoke(context);
			});

			return services;
		}
	}
}
