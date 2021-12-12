namespace Fluxera.Extensions.DataManagement
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///		Adds the required services for the data management.
		/// </summary>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IServiceCollection AddDataManagement(this IServiceCollection services)
		{
			services.AddOptions();
			services.TryAddSingleton<IConnectionStringResolver, DefaultConnectionStringResolver>();
			services.TryAddSingleton<IDataSeeder, DataSeeder>();

			return services;
		}
	}
}
