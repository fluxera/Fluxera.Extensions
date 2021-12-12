namespace Fluxera.Extensions.Http
{
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for a named HTTP client.
	/// </summary>
	[PublicAPI]
	public interface IHttpClientService
	{
		string Name { get; }
	}
}
