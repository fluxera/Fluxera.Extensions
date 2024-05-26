namespace Fluxera.Extensions.DataManagement
{
	using JetBrains.Annotations;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	///     Extension´methods for the <see cref="IConfiguration" /> type.
	/// </summary>
	[PublicAPI]
	public static class ConfigurationExtensions
	{
		/// <summary>
		///     Gets the <see cref="ConnectionStrings" /> from the <see cref="IConfiguration" /> instance.
		/// </summary>
		/// <param name="configuration"></param>
		public static ConnectionStrings GetConnectionStrings(this IConfiguration configuration)
		{
			Guard.ThrowIfNull(configuration);

			IConfigurationSection connectionStringsSection = configuration.GetSection("ConnectionStrings");
			ConnectionStrings connectionStrings = connectionStringsSection.Get<ConnectionStrings>();

			return connectionStrings;
		}
	}
}
