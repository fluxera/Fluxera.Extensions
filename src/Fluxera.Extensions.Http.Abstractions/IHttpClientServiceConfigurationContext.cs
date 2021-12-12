namespace Fluxera.Extensions.Http
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IHttpClientServiceConfigurationContext
	{
		string RemoteServiceName { get; }

		IServiceProvider ServiceProvider { get; }
	}
}
