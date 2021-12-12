namespace Fluxera.Extensions.Http
{
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IHttpClientService
	{
		string Name { get; }
	}
}
