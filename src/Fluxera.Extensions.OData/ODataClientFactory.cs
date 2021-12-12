namespace Fluxera.Extensions.OData
{
	using System;
	using System.Net.Http;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	[UsedImplicitly]
	internal sealed class ODataClientFactory : IODataClientFactory
	{
		private readonly IODataClientSettingsFactory clientSettingsFactory;

		public ODataClientFactory(IODataClientSettingsFactory clientSettingsFactory)
		{
			this.clientSettingsFactory = clientSettingsFactory;
		}

		/// <inheritdoc />
		public IODataClient CreateClient(string name)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));

			return this.CreateClient(name, settings =>
			{
				settings.PreferredUpdateMethod = ODataUpdateMethod.Patch;
			});
		}

		/// <inheritdoc />
		public IODataClient CreateClient(string name, Action<ODataClientSettings> configureSettings)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.Null(configureSettings, nameof(configureSettings));

			ODataClientSettings settings = this.clientSettingsFactory.CreateSettings(name);
			configureSettings.Invoke(settings);

			return new ODataClient(settings);
		}

		/// <inheritdoc />
		public IHttpClientFactory HttpClientFactory { get; }
	}
}
