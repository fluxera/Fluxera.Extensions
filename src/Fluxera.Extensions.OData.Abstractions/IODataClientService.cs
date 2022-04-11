namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a named OData client.
	/// </summary>
	[PublicAPI]
	public interface IODataClientService
	{
		/// <summary>
		///     Gets the name of the OData client service.
		/// </summary>
		string Name { get; }
	}
}
