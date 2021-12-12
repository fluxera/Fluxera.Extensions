namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;
	using Simple.OData.Client;

	[PublicAPI]
	public interface IODataClientSettingsFactory
	{
		ODataClientSettings CreateSettings(string name);
	}
}
