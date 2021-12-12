namespace Fluxera.Extensions.Http
{
	using System.IO;
	using System.Net.Http;
	using System.Net.Http.Headers;
	using System.Threading.Tasks;
	using JetBrains.Annotations;
	using System.Text.Json;
	using System.Text.Json.Serialization;
	using Fluxera.Utilities.Extensions;

	[PublicAPI]
	public static class HttpContentExtensions
	{
		public static async Task<T?> ReadAsAsync<T>(this HttpContent content)
			where T : class
		{
			T? obj = null;

			try
			{
				await using (Stream contentStream = await content.ReadAsStreamAsync())
				{
					JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
					{
						DefaultIgnoreCondition = JsonIgnoreCondition.Always,
						PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
						DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
						Converters =
						{
							new JsonStringEnumConverter()
						}
					};
					obj = await JsonSerializer.DeserializeAsync<T>(contentStream, jsonSerializerOptions);
				}
			}
			catch
			{
				// Note: Intentionally left blank.
			}

			return obj;
		}

		public static async Task<HttpContent> AsJsonContentAsync<T>(this T obj)
			where T : class
		{
			// https://johnthiriet.com/efficient-post-calls/
			MemoryStream memoryStream = new MemoryStream();

			JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
			{
				DefaultIgnoreCondition = JsonIgnoreCondition.Always,
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
				Converters =
				{
					new JsonStringEnumConverter()
				}
			};
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