namespace Fluxera.Extensions.Http
{
	using System;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Net.Http.Json;
	using System.Text.Json;
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     Extensions methods for the <see cref="HttpContent" /> type.
	/// </summary>
	[PublicAPI]
	public static class HttpContentExtensions
	{
		/// <summary>
		///     Reads the <see cref="HttpContent" /> as <typeparamref name="T" /> by deserializing it using the
		///     <see cref="JsonSerializer" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content"></param>
		/// <param name="options"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[Obsolete("Use built-in ReadFromJsonAsync method.")]
		public static async Task<T> ReadAsAsync<T>(this HttpContent content, JsonSerializerOptions options = null, CancellationToken cancellationToken = default) where T : class
		{
			return await content.ReadFromJsonAsync<T>(options, cancellationToken: cancellationToken);
		}

		/// <summary>
		///     Creates a JSON <see cref="HttpContent" /> instance from the given object using the <see cref="JsonSerializer" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static HttpContent AsJsonContent<T>(this T obj, JsonSerializerOptions options = null) where T : class
		{
			MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue("application/json")
			{
				CharSet = "utf-8"
			};

			return JsonContent.Create(obj, typeof(T), mediaTypeHeaderValue, options);
		}
	}
}
