namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;
	using System.Net.Http;

	[UsedImplicitly]
	internal sealed class ODataClientFactory : IODataClientFactory
	{
		//private readonly IODataClientSettingsFactory oDataClientSettingsFactory;

		//public ODataClientFactory(IODataClientSettingsFactory oDataClientSettingsFactory)
		//{
		//	this.oDataClientSettingsFactory = oDataClientSettingsFactory;
		//}

		///// <inheritdoc />
		//public IODataClient CreateClient(string name)
		//{
		//	return this.CreateClient(name, settings =>
		//	{
		//		settings.PreferredUpdateMethod = ODataUpdateMethod.Patch;
		//	});
		//}

		///// <inheritdoc />
		//public IODataClient CreateClient(string name, Action<ODataClientSettings> configureSettings)
		//{
		//	ODataClientSettings settings = this.oDataClientSettingsFactory.CreateSettings(name);

		//	configureSettings?.Invoke(settings);

		//	return new ODataClient(settings);
		//}

		public IHttpClientFactory HttpClientFactory { get; }
	}
}
