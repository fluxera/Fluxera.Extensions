namespace Fluxera.Extensions.Http
{
	using System.Net.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///		An abstract base class for named HTTP clients.
	/// </summary>
	[PublicAPI]
	public abstract class HttpClientServiceBase : IHttpClientService
	{
		protected HttpClientServiceBase(string name, IHttpClientFactory httpClientFactory)
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.Null(httpClientFactory, nameof(httpClientFactory));

			this.Name = name;
			this.HttpClient = httpClientFactory.CreateClient(name);
		}

		public string Name { get; }

		protected HttpClient HttpClient { get; }
	}
}
