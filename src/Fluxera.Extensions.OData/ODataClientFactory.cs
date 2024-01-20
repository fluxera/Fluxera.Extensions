namespace Fluxera.Extensions.OData
{
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	[UsedImplicitly]
	internal sealed class ODataClientFactory : IODataClientFactory
	{
		/// <inheritdoc />
		public IODataClient CreateClient(string name, ODataClientSettings oDataClientSettings)
		{
			Guard.Against.NullOrWhiteSpace(name);

			oDataClientSettings.PreferredUpdateMethod = ODataUpdateMethod.Patch;

			return new ODataClient(oDataClientSettings);
		}
	}
}
