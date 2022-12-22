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
		/// <summary>
		///     Reads the <see cref="HttpContent" /> as <typeparamref name="T" /> by deserializing it using the
		///     <see cref="JsonSerializer" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="content"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static async Task<T> ReadAsAsync<T>(this HttpContent content, Action<JsonSerializerOptions> configureOptions = null) where T : class
		{
			await using(Stream contentStream = await content.ReadAsStreamAsync())
			{
				JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
					DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
					Converters =
					{
						new JsonStringEnumConverter()
					}
				};
				jsonSerializerOptions.UseEnumeration();
				jsonSerializerOptions.UsePrimitiveValueObject();
				jsonSerializerOptions.UseStronglyTypedId();

				configureOptions?.Invoke(jsonSerializerOptions);

				return await JsonSerializer.DeserializeAsync<T>(contentStream, jsonSerializerOptions);
			}
		}

		/// <summary>
		///     Creates a JSON <see cref="HttpContent" /> instance from the given object using the <see cref="JsonSerializer" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <param name="configureOptions"></param>
		/// <returns></returns>
		public static async Task<HttpContent> AsJsonContentAsync<T>(this T obj, Action<JsonSerializerOptions> configureOptions = null) where T : class
		{
			// https://johnthiriet.com/efficient-post-calls/
			MemoryStream memoryStream = new MemoryStream();

			JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
				Converters =
				{
					new JsonStringEnumConverter()
				}
			};
			jsonSerializerOptions.UseEnumeration();
			jsonSerializerOptions.UsePrimitiveValueObject();
			jsonSerializerOptions.UseStronglyTypedId();

			configureOptions?.Invoke(jsonSerializerOptions);

			await JsonSerializer.SerializeAsync(memoryStream, obj, jsonSerializerOptions);

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
