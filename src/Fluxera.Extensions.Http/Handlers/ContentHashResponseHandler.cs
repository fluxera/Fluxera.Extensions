﻿namespace Fluxera.Extensions.Http.Handlers
{
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Common;
	using JetBrains.Annotations;

	/// <summary>
	///     A delegating handler that assigns a MD5 hash value if content is present.
	/// </summary>
	[UsedImplicitly]
	internal sealed class ContentHashResponseHandler : HashDelegatingHandler
	{
		public ContentHashResponseHandler(IHashCalculator hashCalculator)
			: base(hashCalculator)
		{
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
			if(!response.IsSuccessStatusCode)
			{
				return response;
			}

			await this.ApplyHash(response.Content).ConfigureAwait(false);

			return response;
		}
	}
}
