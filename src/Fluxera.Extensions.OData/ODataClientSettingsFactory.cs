namespace Fluxera.Extensions.OData
{
	using System;
	using System.Net.Http;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Logging;
	using Microsoft.Extensions.Options;
	using Simple.OData.Client;

	[UsedImplicitly]
	internal sealed class ODataClientSettingsFactory : IODataClientSettingsFactory
	{
		private readonly IHttpClientFactory httpClientFactory;
		private readonly ILoggerFactory loggerFactory;
		private readonly ODataClientOptions options;

		public ODataClientSettingsFactory(
			IHttpClientFactory httpClientFactory,
			IOptions<ODataClientOptions> options,
			ILoggerFactory loggerFactory)
		{
			this.httpClientFactory = httpClientFactory;
			this.options = options.Value;
			this.loggerFactory = loggerFactory;
		}

		/// <inheritdoc />
		public ODataClientSettings CreateSettings(string name)
		{
			HttpClient httpClient = this.httpClientFactory.CreateClient(name);
			httpClient.BaseAddress = new Uri(httpClient.BaseAddress, "odata");

			ILogger logger = this.loggerFactory.CreateLogger<ODataClientSettings>();

			return new ODataClientSettings(httpClient)
			{
				IncludeAnnotationsInResults = true,
				RenewHttpConnection = false,
				OnTrace = (message, parameters) =>
				{
					if (parameters != null && parameters.Length > 0)
					{
						logger.LogTrace(string.Format(message, parameters));
					}
					else
					{
						logger.LogTrace(message);
					}
				},
			};
		}
	}
}
