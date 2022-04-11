namespace Fluxera.Extensions.Http
{
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a named HTTP client.
	/// </summary>
	[PublicAPI]
	public interface IHttpClientService
	{
		/// <summary>
		///     Gets the name of the HTTP client service.
		/// </summary>
		string Name { get; }
	}
}
