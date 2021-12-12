//namespace Fluxera.Foundation.Hosting.Modules.HttpClient.Handlers
//{
//	using System;
//	using System.Net.Http;
//	using System.Net.Http.Headers;
//	using System.Threading;
//	using System.Threading.Tasks;
//	using Fluxera.Foundation.Extensions.Abstractions;
//	using JetBrains.Annotations;
//	using Microsoft.Extensions.DependencyInjection;

//	/// <summary>
//	///     A <see cref="DelegatingHandler" /> that sets the Authorization header with a token
//	///     from a <see cref="IAccessTokenProvider" />.
//	/// </summary>
//	[UsedImplicitly]
//	internal sealed class AuthenticateRequestHandler : DelegatingHandler
//	{
//		private readonly IServiceProvider serviceProvider;

//		public AuthenticateRequestHandler(IServiceProvider serviceProvider)
//		{
//			this.serviceProvider = serviceProvider;
//		}

//		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//		{
//			IAccessTokenProvider accessTokenProvider = this.serviceProvider.GetService<IAccessTokenProvider>();

//			// Only add the header with a token when authentication is used.
//			if(accessTokenProvider != null)
//			{
//				string token = await accessTokenProvider.GetAccessTokenAsync();
//				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
//			}

//			return await base.SendAsync(request, cancellationToken);
//		}
//	}
//}


