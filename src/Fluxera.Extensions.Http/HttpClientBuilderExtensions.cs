namespace Fluxera.Extensions.Http
{
	using System.Net.Http;
	using Fluxera.Extensions.Http.Handlers;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     Extensions methods for the <see cref="IHttpClientBuilder" /> type.
	/// </summary>
	[PublicAPI]
	public static class HttpClientBuilderExtensions
	{
		/// <summary>
		///     Adds the <see cref="IdempotentPostRequestHandler" /> to the <see cref="IHttpClientBuilder" />.
		/// </summary>
		/// <param name="httpClientBuilder"></param>
		/// <returns></returns>
		public static IHttpClientBuilder AddIdempotentPostRequestHandler(this IHttpClientBuilder httpClientBuilder)
		{
			return httpClientBuilder.AddHttpMessageHandlerScoped<IdempotentPostRequestHandler>();
		}

		/// <summary>
		///     Adds the <see cref="ContentHashRequestHandler" /> to the <see cref="IHttpClientBuilder" />.
		/// </summary>
		/// <param name="httpClientBuilder"></param>
		/// <returns></returns>
		public static IHttpClientBuilder AddContentHashRequestHandler(this IHttpClientBuilder httpClientBuilder)
		{
			return httpClientBuilder.AddHttpMessageHandlerScoped<ContentHashRequestHandler>();
		}

		/// <summary>
		///     Adds the <see cref="ContentHashResponseHandler" /> to the <see cref="IHttpClientBuilder" />.
		/// </summary>
		/// <param name="httpClientBuilder"></param>
		/// <returns></returns>
		public static IHttpClientBuilder AddContentHashResponseHandler(this IHttpClientBuilder httpClientBuilder)
		{
			return httpClientBuilder.AddHttpMessageHandlerScoped<ContentHashResponseHandler>();
		}

		/// <summary>
		///     Adds the <see cref="AuthenticateRequestHandler" /> to the <see cref="IHttpClientBuilder" />.
		/// </summary>
		/// <param name="httpClientBuilder"></param>
		/// <returns></returns>
		public static IHttpClientBuilder AddAuthenticateRequestHandler(this IHttpClientBuilder httpClientBuilder)
		{
			return httpClientBuilder.AddHttpMessageHandlerScoped<AuthenticateRequestHandler>();
		}

		/// <summary>
		///     Add the given delegating handler to the <see cref="IServiceCollection" /> and the
		///     <see cref="IHttpClientBuilder" /> to enable dependency injection for the delegating
		///     handlers.
		/// </summary>
		/// <typeparam name="TDelegatingHandler"></typeparam>
		/// <param name="httpClientBuilder"></param>
		/// <returns></returns>
		public static IHttpClientBuilder AddHttpMessageHandlerScoped<TDelegatingHandler>(this IHttpClientBuilder httpClientBuilder)
			where TDelegatingHandler : DelegatingHandler
		{
			Guard.Against.Null(httpClientBuilder, nameof(httpClientBuilder));

			// Register the handler in services.
			httpClientBuilder.Services.TryAddScoped<TDelegatingHandler>();

			// Add the handler to the http client.
			return httpClientBuilder.AddHttpMessageHandler<TDelegatingHandler>();
		}

		//public static HttpClientBuilderList AddIdempotentPostRequestHandler(this HttpClientBuilderList httpClientBuilderList)
		//{
		//	return httpClientBuilderList.AddHttpMessageHandlerScoped<IdempotentPostRequestHandler>();
		//}

		//public static HttpClientBuilderList AddContentHashRequestHandler(this HttpClientBuilderList httpClientBuilderList)
		//{
		//	return httpClientBuilderList.AddHttpMessageHandlerScoped<ContentHashRequestHandler>();
		//}

		//public static HttpClientBuilderList AddContentHashResponseHandler(this HttpClientBuilderList httpClientBuilderList)
		//{
		//	return httpClientBuilderList.AddHttpMessageHandlerScoped<ContentHashResponseHandler>();
		//}

		//public static HttpClientBuilderList AddHttpMessageHandlerScoped<T>(this HttpClientBuilderList httpClientBuilderList)
		//	where T : DelegatingHandler
		//{
		//	Guard.Against.Null(httpClientBuilderList, nameof(httpClientBuilderList));

		//	foreach(IHttpClientBuilder httpClientBuilder in httpClientBuilderList)
		//	{
		//		httpClientBuilder.AddHttpMessageHandler<T>();
		//	}

		//	return httpClientBuilderList;
		//}
	}
}
