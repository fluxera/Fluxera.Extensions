namespace Fluxera.Extensions.Http
{
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public sealed class HttpClientBuilderList : List<IHttpClientBuilder>
	{
	}
}
