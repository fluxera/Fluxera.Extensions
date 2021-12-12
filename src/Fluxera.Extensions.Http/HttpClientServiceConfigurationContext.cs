namespace Fluxera.Extensions.Http
{
	using System;

	internal sealed class HttpClientServiceConfigurationContext : IHttpClientServiceConfigurationContext
	{
		/// <inheritdoc />
		public string RemoteServiceName { get; set; }

		/// <inheritdoc />
		public IServiceProvider ServiceProvider { get; set; }
	}
}
