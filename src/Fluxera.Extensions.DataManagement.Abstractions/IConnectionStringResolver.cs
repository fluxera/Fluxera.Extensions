namespace Fluxera.Extensions.DataManagement
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract that resolves a named connection string.
	/// </summary>
	[PublicAPI]
	public interface IConnectionStringResolver
	{
		/// <summary>
		///     Resolves the connection string for the given connection string name.
		/// </summary>
		/// <param name="name">The name of the connection string.</param>
		/// <returns>The connection string.</returns>
		string ResolveConnectionString(string name = ConnectionStrings.DefaultConnectionStringName);
	}
}
