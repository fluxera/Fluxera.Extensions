namespace Fluxera.Extensions.Http
{
	using System.Net.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;

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
		/// <param name="httpClient"></param>
		/// <param name="options"></param>
		protected HttpClientServiceBase(string name, HttpClient httpClient, RemoteService options)
		{
			this.Name = Guard.Against.Null(name);
			this.HttpClient = Guard.Against.Null(httpClient);
			this.Options = Guard.Against.Null(options);
		}

		/// <summary>
		///     Gets the remote service options.
		/// </summary>
		public RemoteService Options { get; }

		/// <summary>
		///     Gets the underlying http client instance.
		/// </summary>
		protected HttpClient HttpClient { get; }

		/// <inheritdoc />
		public string Name { get; }
	}
}
