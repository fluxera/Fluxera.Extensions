namespace Fluxera.Extensions.OData
{
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for a named OData client.
	/// </summary>
	[PublicAPI]
	public interface IODataClientService : IHttpClientService
	{
	}
}
