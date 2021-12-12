namespace Fluxera.Extensions.Http
{
	using System.Net.Http;
	using Fluxera.Guards;
	using Handlers;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	public static class HttpClientBuilderExtensions
	{
		public static IHttpClientBuilder AddIdempotentPostRequestHandler(this IHttpClientBuilder httpClientBuilder)
		{
			return httpClientBuilder.AddHttpMessageHandlerScoped<IdempotentPostRequestHandler>();
		}

		public static HttpClientBuilderList AddIdempotentPostRequestHandler(this HttpClientBuilderList httpClientBuilderList)
		{
			return httpClientBuilderList.AddHttpMessageHandlerScoped<IdempotentPostRequestHandler>();
		}

		public static IHttpClientBuilder AddContentHashRequestHandler(this IHttpClientBuilder httpClientBuilder)
		{
			return httpClientBuilder.AddHttpMessageHandlerScoped<ContentHashRequestHandler>();
		}

		public static HttpClientBuilderList AddContentHashRequestHandler(this HttpClientBuilderList httpClientBuilderList)
		{
			return httpClientBuilderList.AddHttpMessageHandlerScoped<ContentHashRequestHandler>();
		}

		public static IHttpClientBuilder AddContentHashResponseHandler(this IHttpClientBuilder httpClientBuilder)
		{
			return httpClientBuilder.AddHttpMessageHandlerScoped<ContentHashResponseHandler>();
		}

		public static HttpClientBuilderList AddContentHashResponseHandler(this HttpClientBuilderList httpClientBuilderList)
		{
			return httpClientBuilderList.AddHttpMessageHandlerScoped<ContentHashResponseHandler>();
		}

		public static HttpClientBuilderList AddHttpMessageHandlerScoped<T>(this HttpClientBuilderList httpClientBuilderList)
			where T : DelegatingHandler
		{
			Guard.Against.Null(httpClientBuilderList, nameof(httpClientBuilderList));

			foreach (IHttpClientBuilder httpClientBuilder in httpClientBuilderList)
			{
				httpClientBuilder.AddHttpMessageHandler<T>();
			}

			return httpClientBuilderList;
		}

		public static IHttpClientBuilder AddHttpMessageHandlerScoped<T>(this IHttpClientBuilder httpClientBuilder)
			where T : DelegatingHandler
		{
			Guard.Against.Null(httpClientBuilder, nameof(httpClientBuilder));

			// Register the handler in services.
			httpClientBuilder.Services.TryAddScoped<T>();

			// Add the handler to the http client.
			return httpClientBuilder.AddHttpMessageHandler<T>();
		}
	}
}
