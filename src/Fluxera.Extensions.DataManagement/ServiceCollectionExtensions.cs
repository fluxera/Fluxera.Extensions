namespace Fluxera.Extensions.DataManagement
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		///     Configures the <see cref="ConnectionStrings" /> for use in the <see cref="IOptions{TOptions}" /> infrastructure.
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		public static void ConfigureConnectionStrings(this IServiceCollection services, IConfiguration configuration)
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(configuration, nameof(configuration));

			IConfigurationSection connectionStringsSection = configuration.GetSection("ConnectionStrings");
			if(connectionStringsSection != null)
			{
				services.Configure<ConnectionStrings>(connectionStringsSection);
			}
		}
	}
}
