namespace Fluxera.Extensions.Http
{
	using System;
	using System.Net.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     An abstract base class for named HTTP clients.
	/// </summary>
	[PublicAPI]
	public abstract class HttpClientServiceBase : IHttpClientService
	{
		/// <summary>
		///     Creates a new instance of the <see cref="HttpClientServiceBase" /> type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="httpClientFactory"></param>
		/// <param name="optionsWrapper"></param>
		protected HttpClientServiceBase(string name, IHttpClientFactory httpClientFactory, IOptions<RemoteServiceOptions> optionsWrapper)
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.Null(httpClientFactory, nameof(httpClientFactory));
			Guard.Against.Null(optionsWrapper, nameof(optionsWrapper));

			this.Name = name;
			this.HttpClient = httpClientFactory.CreateClient(name);

			string baseAddress = optionsWrapper.Value.RemoteServices[name].BaseAddress;
			this.HttpClient.BaseAddress = new Uri(baseAddress);
		}

		/// <summary>
		///     Gets the underlying http client instance.
		/// </summary>
		protected HttpClient HttpClient { get; }

		/// <inheritdoc />
		public string Name { get; }
	}
}
