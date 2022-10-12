namespace Fluxera.Extensions.OData
{
	using System.Net.Http;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Logging;
	using Simple.OData.Client;

	[UsedImplicitly]
	internal sealed class ODataClientSettingsFactory : IODataClientSettingsFactory
	{
		private readonly ILogger logger;

		public ODataClientSettingsFactory(ILoggerFactory loggerFactory)
		{
			this.logger = loggerFactory.CreateLogger<ODataClientSettings>();
		}

		/// <inheritdoc />
		public ODataClientSettings CreateSettings(string name, HttpClient httpClient)
		{
			return new ODataClientSettings(httpClient)
			{
				IncludeAnnotationsInResults = true,
				RenewHttpConnection = false,
				OnTrace = (message, parameters) =>
				{
					if(parameters != null && parameters.Length > 0)
					{
						this.logger.LogTrace(string.Format(message, parameters));
					}
					else
					{
						this.logger.LogTrace(message);
					}
				},
			};
		}
	}
}
