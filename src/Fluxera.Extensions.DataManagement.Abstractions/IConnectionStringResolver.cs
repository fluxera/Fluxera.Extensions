namespace Fluxera.Extensions.DataManagement
{
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for a connection string resolver.
	/// </summary>
	[PublicAPI]
	public interface IConnectionStringResolver
	{
		/// <summary>
		///		Provides the connection string for the given name.
		/// </summary>
		/// <param name="connectionStringName">The name of the connection string.</param>
		/// <returns>The connection string.</returns>
		string? ResolveConnectionString(string? connectionStringName = null);
	}
}
