namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddODataClient(this IServiceCollection services)
		{
			// TODO

			return services;
		}

		//public static IServiceCollection AddCrudApplicationService<TService>(this IServiceCollection services,
		//	string remoteServiceName,
		//	string collectionName,
		//	Func<ICrudApplicationServiceConfigurationContext, TService> factory)
		//	where TService : class, ICrudApplicationService
		//{
		//	Guard.AgainstNullOrEmpty(nameof(remoteServiceName), remoteServiceName);
		//	Guard.AgainstNullOrEmpty(nameof(collectionName), collectionName);
		//	Guard.AgainstNull(nameof(factory), factory);

		//	services.TryAddCrudApplicationServiceTransient(remoteServiceName, collectionName, factory);
		//	return services;
		//}

		//public static IServiceCollection AddCrudApplicationService<TService>(this IServiceCollection services,
		//	string collectionName,
		//	Func<ICrudApplicationServiceConfigurationContext, TService> factory)
		//	where TService : class, ICrudApplicationService
		//{
		//	Guard.AgainstNullOrEmpty(nameof(collectionName), collectionName);
		//	Guard.AgainstNull(nameof(factory), factory);

		//	services.TryAddCrudApplicationServiceTransient(Options.DefaultName, collectionName, factory);
		//	return services;
		//}

		//private static IServiceCollection TryAddCrudApplicationServiceTransient<TService>(
		//	this IServiceCollection services,
		//	string remoteServiceName,
		//	string collectionName,
		//	Func<ICrudApplicationServiceConfigurationContext, TService> factory)
		//	where TService : class, ICrudApplicationService
		//{
		//	Guard.AgainstNullOrEmpty(nameof(collectionName), collectionName);
		//	Guard.AgainstNull(nameof(factory), factory);

		//	services.TryAddTransient(serviceProvider =>
		//	{
		//		ICrudApplicationServiceConfigurationContext context = new ApplicationServiceConfigurationContext
		//		{
		//			RemoteServiceName = remoteServiceName,
		//			CollectionName = collectionName,
		//			ServiceProvider = serviceProvider,
		//		};

		//		return factory.Invoke(context);
		//	});

		//	return services;
		//}
	}
}
