namespace Fluxera.Extensions.Http.Handlers
{
	using System;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A delegating handler that sets the Authorization header with a token from a <see cref="IAccessTokenProvider" />.
	/// </summary>
	[UsedImplicitly]
	internal sealed class AuthenticateRequestHandler : DelegatingHandler
	{
		private readonly IServiceProvider serviceProvider;

		public AuthenticateRequestHandler(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			IAccessTokenProvider accessTokenProvider = this.serviceProvider.GetService<IAccessTokenProvider>();

			// Only add the header with a token when the token provider is used.
			if(accessTokenProvider != null)
			{
				string accessToken = await accessTokenProvider.GetAccessTokenAsync();
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}
}
