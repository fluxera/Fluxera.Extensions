namespace Fluxera.Extensions.Http.Handlers
{
	using System.Net.Http;
	using System.Threading.Tasks;
	using Common;

	internal abstract class HashDelegatingHandler : DelegatingHandler
	{
		private readonly IHashCalculator hashCalculator;

		protected HashDelegatingHandler(IHashCalculator hashCalculator)
		{
			this.hashCalculator = hashCalculator;
		}

		protected async Task ApplyHash(HttpContent content)
		{
			byte[] inputBytes = await content.ReadAsByteArrayAsync().ConfigureAwait(false);
			byte[] hash = this.hashCalculator.ComputeHash(inputBytes, new MD5Managed()/*, MD5.Create()*/);

			content.Headers.ContentMD5 = hash;
		}
	}
}
