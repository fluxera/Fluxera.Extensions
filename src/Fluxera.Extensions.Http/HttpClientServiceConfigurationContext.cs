namespace Fluxera.Extensions.Http
{
	using System.Net.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     The configuration context for named http client services.
	/// </summary>
	[PublicAPI]
	public class HttpClientServiceConfigurationContext
	{
		/// <summary>
		///     Creates a new instance of the <see cref="HttpClientServiceConfigurationContext" /> type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="httpClient"></param>
		/// <param name="options"></param>
		public HttpClientServiceConfigurationContext(string name, HttpClient httpClient, RemoteService options)
		{
			this.Name = Guard.Against.Null(name);
			this.HttpClient = Guard.Against.Null(httpClient);
			this.Options = Guard.Against.Null(options);
		}

		/// <summary>
		///     Gets the name of the http client service.
		/// </summary>
		public string Name { get; }

		/// <summary>
		///     Get the underlying http client instance.
		/// </summary>
		public HttpClient HttpClient { get; }

		/// <summary>
		///     Gets the remote service options.
		/// </summary>
		public RemoteService Options { get; }
	}
}
