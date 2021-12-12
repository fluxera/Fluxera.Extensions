namespace Fluxera.Extensions.Http
{
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///		A specialized list for <see cref="IHttpClientBuilder"/> instances.
	/// </summary>
	[PublicAPI]
	public sealed class HttpClientBuilderList : List<IHttpClientBuilder>
	{
	}
}
