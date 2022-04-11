namespace Fluxera.Extensions.Http
{
	using System.Net.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     The configuration context for a named HTTP client.
	/// </summary>
	[PublicAPI]
	public class HttpClientServiceCreationContext
	{
		/// <summary>
		///     Creates a new instance of the <see cref="HttpClientServiceCreationContext" /> type.
		/// </summary>
		/// <param name="remoteServiceName"></param>
		/// <param name="httpClientFactory"></param>
		/// <param name="optionsWrapper"></param>
		public HttpClientServiceCreationContext(string remoteServiceName, IHttpClientFactory httpClientFactory, IOptions<RemoteServiceOptions> optionsWrapper)
		{
			Guard.Against.Null(remoteServiceName, nameof(remoteServiceName));
			Guard.Against.Null(httpClientFactory, nameof(httpClientFactory));

			this.RemoteServiceName = remoteServiceName;
			this.HttpClientFactory = httpClientFactory;
			this.OptionsWrapper = optionsWrapper;
		}

		/// <summary>
		///     Gets the name of the remote service.
		/// </summary>
		public string RemoteServiceName { get; }

		/// <summary>
		///     Gets the http client factory.
		/// </summary>
		public IHttpClientFactory HttpClientFactory { get; }

		/// <summary>
		///     Gets the remote services options.
		/// </summary>
		public IOptions<RemoteServiceOptions> OptionsWrapper { get; }
	}
}
