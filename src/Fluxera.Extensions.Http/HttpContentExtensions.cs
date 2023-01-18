namespace Fluxera.Extensions.Http
{
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using System.Threading.Tasks;
	using Fluxera.Enumeration.SystemTextJson;
	using Fluxera.StronglyTypedId.SystemTextJson;
	using Fluxera.Utilities.Extensions;
	using Fluxera.ValueObject.SystemTextJson;
	using JetBrains.Annotations;

	/// <summary>
	///     Extensions methods for the <see cref="HttpContent" /> type.
	/// </summary>
	[PublicAPI]
	public static class HttpContentExtensions
	{
		private static Lazy<JsonSerializerOptions> jsonSerializerOptions = new Lazy<JsonSerializerOptions>(() =>
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
		/// <returns></returns>
		public static async Task<T> ReadAsAsync<T>(this HttpContent content, JsonSerializerOptions options = null) where T : class
		{
			await using(Stream contentStream = await content.ReadAsStreamAsync())
			{
				options ??= jsonSerializerOptions.Value;
				return await JsonSerializer.DeserializeAsync<T>(contentStream, options);
			}
		}

		/// <summary>
		///     Creates a JSON <see cref="HttpContent" /> instance from the given object using the <see cref="JsonSerializer" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static async Task<HttpContent> AsJsonContentAsync<T>(this T obj, JsonSerializerOptions options = null) where T : class
		{
			// https://johnthiriet.com/efficient-post-calls/
			MemoryStream memoryStream = new MemoryStream();

			options ??= jsonSerializerOptions.Value;
			await JsonSerializer.SerializeAsync(memoryStream, obj, options);

			memoryStream.Rewind();
			HttpContent httpContent = new StreamContent(memoryStream);
			httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json")
			{
				CharSet = "utf-8"
			};

			return httpContent;
		}
	}
}
