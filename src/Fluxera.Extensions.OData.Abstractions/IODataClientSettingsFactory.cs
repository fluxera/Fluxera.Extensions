namespace Fluxera.Extensions.OData
{
	using System.Net.Http;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	/// <summary>
	///     A contract for a factory that creates <see cref="ODataClientSettings" />.
	/// </summary>
	[PublicAPI]
	public interface IODataClientSettingsFactory
	{
		/// <summary>
		///     Creates new <see cref="ODataClientSettings" /> for the given name,
		/// </summary>
		/// <param name="name"></param>
		/// <param name="httpClient"></param>
		/// <returns></returns>
		ODataClientSettings CreateSettings(string name, HttpClient httpClient);
	}
}
