namespace Fluxera.Extensions.DataManagement
{
	using Fluxera.Guards;
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
			Guard.Against.Null(configuration, nameof(configuration));

			ConnectionStrings connectionStrings = null;

			IConfigurationSection connectionStringsSection = configuration.GetSection("ConnectionStrings");
			if(connectionStringsSection != null)
			{
				connectionStrings = connectionStringsSection.Get<ConnectionStrings>();
			}

			return connectionStrings ?? new ConnectionStrings();
		}
	}
}
