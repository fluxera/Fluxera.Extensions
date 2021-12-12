namespace Fluxera.Extensions.OData
{
	using System;
	using Fluxera.Extensions.Http;
	using JetBrains.Annotations;

	[PublicAPI]
	public class ODataServiceConfigurationContext : HttpClientServiceConfigurationContext
	{
		/// <inheritdoc />
		public ODataServiceConfigurationContext(string remoteServiceName, string collectionName, IServiceProvider serviceProvider) 
			: base(remoteServiceName, serviceProvider)
		{
			this.CollectionName = collectionName;
		}

		public string CollectionName { get; }
	}
}
