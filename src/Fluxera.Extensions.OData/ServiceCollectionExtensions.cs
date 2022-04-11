namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		//public static IServiceCollection AddCrudApplicationService<TService>(this IServiceCollection services,
		//	string remoteServiceName,
		//	string collectionName,
		//	Func<ODataServiceCreationContext, TService> factory)
		//	where TService : class, IODataClientService
		//{
		//	Guard.Against.NullOrEmpty(remoteServiceName, nameof(remoteServiceName));
		//	Guard.Against.NullOrEmpty(collectionName, nameof(collectionName));
		//	Guard.Against.Null(factory, nameof(factory));

		//	services.TryAddCrudApplicationServiceTransient(remoteServiceName, collectionName, factory);
		//	return services;
		//}

		//public static IServiceCollection AddCrudApplicationService<TService>(this IServiceCollection services,
		//	string collectionName,
		//	Func<ODataServiceCreationContext, TService> factory)
		//	where TService : class, IODataClientService
		//{
		//	Guard.Against.NullOrEmpty(collectionName, nameof(collectionName));
		//	Guard.Against.Null(factory, nameof(factory));

		//	services.TryAddCrudApplicationServiceTransient(Options.DefaultName, collectionName, factory);
		//	return services;
		//}

		//private static IServiceCollection TryAddCrudApplicationServiceTransient<TService>(
		//	this IServiceCollection services,
		//	string remoteServiceName,
		//	string collectionName,
		//	Func<ODataServiceCreationContext, TService> factory)
		//	where TService : class, IODataClientService
		//{
		//	Guard.Against.NullOrEmpty(remoteServiceName, nameof(remoteServiceName));
		//	Guard.Against.NullOrEmpty(collectionName, nameof(collectionName));
		//	Guard.Against.Null(factory, nameof(factory));

		//	services.TryAddTransient(serviceProvider =>
		//	{
		//		IHttpClientFactory httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
		//		ODataServiceCreationContext context = new ODataServiceCreationContext(remoteServiceName, collectionName, httpClientFactory);
		//		return factory.Invoke(context);
		//	});

		//	return services;
		//}
	}
}
