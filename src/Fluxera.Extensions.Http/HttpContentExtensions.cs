namespace Fluxera.Extensions.Http
{
	using System;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Net.Http.Json;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Enumeration.SystemTextJson;
	using Fluxera.StronglyTypedId.SystemTextJson;
	using Fluxera.ValueObject.SystemTextJson;
	using JetBrains.Annotations;

	/// <summary>
	///     Extensions methods for the <see cref="HttpContent" /> type.
	/// </summary>
	[PublicAPI]
	public static class HttpContentExtensions
	{
		private static readonly Lazy<JsonSerializerOptions> jsonSerializerOptions = new Lazy<JsonSerializerOptions>(() =>
		{
			JsonSerializerOptions options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
				Converters =
				{
					new JsonStringEnumConverter()
				}
			};
			options.UseEnumeration();
			options.UsePrimitiveValueObject();
			options.UseStronglyTypedId();

			return options;
		});

		/// <summary>
		///     Reads the <see cref="HttpContent" /> as <typeparamref name="T" /> by deserializing it using the
		///     <see cref="JsonSerializer" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content"></param>
		/// <param name="options"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public static async Task<T> ReadAsAsync<T>(this HttpContent content, JsonSerializerOptions options = null, CancellationToken cancellationToken = default) where T : class
		{
			return await content.ReadFromJsonAsync<T>(options ?? jsonSerializerOptions.Value, cancellationToken: cancellationToken);
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

			return JsonContent.Create(obj, typeof(T), mediaTypeHeaderValue, options ?? jsonSerializerOptions.Value);
		}
	}
}
