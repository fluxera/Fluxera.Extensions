namespace Fluxera.Extensions.Http.Handlers
{
	using System;
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class IdempotentPostRequestHandler : DelegatingHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if(request.Method == HttpMethod.Post)
			{
				request.Headers.Add("X-Idempotent-Token", Guid.NewGuid().ToString("N"));
			}

			return base.SendAsync(request, cancellationToken);
		}
	}
}
