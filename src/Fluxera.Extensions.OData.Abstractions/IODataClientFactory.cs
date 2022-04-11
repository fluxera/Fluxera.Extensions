namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;
	using Simple.OData.Client;

	/// <summary>
	///     A contract for a factory that creates instances of <see cref="IODataClient" />.
	/// </summary>
	[PublicAPI]
	public interface IODataClientFactory
	{
		/// <summary>
		///     Creates a new <see cref="IODataClient" /> for the given name.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="oDataClientSettings"></param>
		/// <returns></returns>
		IODataClient CreateClient(string name, ODataClientSettings oDataClientSettings);
	}
}
