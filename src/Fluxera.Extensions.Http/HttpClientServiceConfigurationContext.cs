namespace Fluxera.Extensions.Http
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///		The configuration context for a named HTTP client.
	/// </summary>
	[PublicAPI]
	public class HttpClientServiceConfigurationContext
	{
		public HttpClientServiceConfigurationContext(string remoteServiceName, IServiceProvider serviceProvider)
		{
			Guard.Against.NullOrWhiteSpace(remoteServiceName, nameof(remoteServiceName));
			Guard.Against.Null(serviceProvider, nameof(serviceProvider));

			this.RemoteServiceName = remoteServiceName;
			this.ServiceProvider = serviceProvider;
		}

		/// <summary>
		///		The name of the remote service.
		/// </summary>
		public string RemoteServiceName { get; }

		/// <summary>
		///		The service provider instance.
		/// </summary>
		public IServiceProvider ServiceProvider { get; }
	}
}
