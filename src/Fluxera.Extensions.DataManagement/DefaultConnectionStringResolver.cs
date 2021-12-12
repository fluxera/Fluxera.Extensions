namespace Fluxera.Extensions.DataManagement
{
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	[PublicAPI]
	public class DefaultConnectionStringResolver : IConnectionStringResolver
	{
		public DefaultConnectionStringResolver(IOptions<DatabaseConnectionOptions> options)
		{
			this.Options = options.Value;
		}

		protected DatabaseConnectionOptions Options { get; }

		public string? ResolveConnectionString(string? connectionStringName = null)
		{
			// Get default value if no connection string name was given.
			connectionStringName ??= ConnectionStrings.DefaultConnectionStringName;

			// Get module specific value if provided.
			string? connectionString = this.Options.ConnectionStrings.GetOrDefault(connectionStringName);
			return !string.IsNullOrWhiteSpace(connectionString) 
				? connectionString 
				: this.Options.ConnectionStrings.Default; // Get default value.
		}
	}
}
