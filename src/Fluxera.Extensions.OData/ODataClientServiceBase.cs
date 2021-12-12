namespace Fluxera.Extensions.OData
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using Http;
	using JetBrains.Annotations;
	using Simple.OData.Client;

	[PublicAPI]
	public abstract class ODataClientServiceBase<T, TKey> : HttpClientServiceBase, IODataClientService
		where T : class, IODataEntity<TKey>
	{
		protected ODataClientServiceBase(string name, string collectionName, IODataClientFactory clientFactory) 
			: base(name, clientFactory.HttpClientFactory)
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(collectionName, nameof(collectionName));
			Guard.Against.Null(clientFactory, nameof(clientFactory));

			this.CollectionName = collectionName;
			this.ODataClient = clientFactory.CreateClient(name);

			V4Adapter.Reference();
		}

		protected string CollectionName { get; }

		protected IODataClient ODataClient { get; }

		protected async Task<TResult> ExecuteFunctionScalarAsync<TResult>(
			object? parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string? functionName = null)
			where TResult : struct, IConvertible
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Function(functionName);

			if (parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<T> ExecuteFunctionSingleAsync(
			object? parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string? functionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Function(functionName);

			if (parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<IReadOnlyCollection<T>> ExecuteFunctionEnumerableAsync(
			object? parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string? functionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Function(functionName);

			if (parameters != null)
			{
				boundClient.Set(parameters);
			}

			IEnumerable<T> results = await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
			return results.AsReadOnly();
		}

		protected async Task ExecuteActionAsync(
			object? parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string? actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if (parameters != null)
			{
				boundClient.Set(parameters);
			}

			await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task ExecuteActionAsync(T instance,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string? actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Key(instance.ID)
				.Action(actionName);

			await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<T> ExecuteActionSingleAsync(T instance,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string? actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Key(instance.ID)
				.Action(actionName);

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<TResult> ExecuteActionScalar<TResult>(
			object? parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string? actionName = null)
			where TResult : struct, IConvertible
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if (parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<T> ExecuteActionSingleAsync(
			object? parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string? actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if (parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<IReadOnlyCollection<T>> ExecuteActionEnumerableAsync(
			object? parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string? actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if (parameters != null)
			{
				boundClient.Set(parameters);
			}

			IEnumerable<T> results = await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
			return results.AsReadOnly();
		}
	}
}
