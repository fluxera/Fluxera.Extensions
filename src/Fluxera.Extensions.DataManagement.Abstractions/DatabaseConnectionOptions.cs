namespace Fluxera.Extensions.DataManagement
{
	using JetBrains.Annotations;

	/// <summary>
	///		Provides the database connection options.
	/// </summary>
	[PublicAPI]
	public sealed class DatabaseConnectionOptions
	{
		public DatabaseConnectionOptions()
		{
			this.ConnectionStrings = new ConnectionStrings();
		}

		/// <summary>
		///		Provides the connection strings.
		/// </summary>
		public ConnectionStrings ConnectionStrings { get; set; }
	}
}
