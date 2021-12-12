namespace Fluxera.Extensions.Http.Handlers
{
	using System.Net.Http;
	using System.Threading;
	using System.Threading.Tasks;
	using Common;
	using JetBrains.Annotations;

	/// <summary>
	///     Handler to assign the MD5 hash value if content is present.
	/// </summary>
	[UsedImplicitly]
	internal sealed class ContentHashRequestHandler : HashDelegatingHandler
	{
		public ContentHashRequestHandler(IHashCalculator hashCalculator)
			: base(hashCalculator)
		{
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
			CancellationToken cancellationToken)
		{
			if (request.Content != null)
			{
				await this.ApplyHash(request.Content).ConfigureAwait(false);
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}
}
