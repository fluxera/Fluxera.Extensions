namespace Fluxera.Extensions.Http
{
	using System.Threading.Tasks;

	/// <summary>
	///     A contract for services that provide access tokens from different sources.
	/// </summary>
	public interface IAccessTokenProvider
	{
		/// <summary>
		///     Gets the access token to use in a HTTP client.
		/// </summary>
		/// <returns></returns>
		Task<string> GetAccessTokenAsync();
	}
}
