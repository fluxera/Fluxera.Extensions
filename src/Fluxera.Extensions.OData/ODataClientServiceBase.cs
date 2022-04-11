namespace Fluxera.Extensions.OData
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Http;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Options;
	using Simple.OData.Client;

	/// <summary>
	///     A base class for OData feed client services.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public abstract class ODataClientServiceBase<T, TKey> : HttpClientServiceBase, IODataClientService
		where T : class, IODataEntity<TKey>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ODataClientServiceBase{T,TKey}" /> type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="collectionName"></param>
		/// <param name="clientFactory"></param>
		/// <param name="optionsWrapper"></param>
		protected ODataClientServiceBase(string name, string collectionName, IODataClientFactory clientFactory, IOptions<RemoteServiceOptions> optionsWrapper)
			: base(name, clientFactory.HttpClientFactory.CreateClient(name), optionsWrapper.Value.RemoteServices[name])
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(collectionName, nameof(collectionName));
			Guard.Against.Null(clientFactory, nameof(clientFactory));

			this.CollectionName = collectionName;
			this.ODataClient = clientFactory.CreateClient(name);

			V4Adapter.Reference();
		}

		/// <summary>
		///     Gets the collection name.
		/// </summary>
		protected string CollectionName { get; }

		/// <summary>
		///     Gets the OData client.
		/// </summary>
		protected IODataClient ODataClient { get; }

		protected async Task<TResult> ExecuteFunctionScalarAsync<TResult>(
			object parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string functionName = null)
			where TResult : struct, IConvertible
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Function(functionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<T> ExecuteFunctionSingleAsync(
			object parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string functionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Function(functionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<IReadOnlyCollection<T>> ExecuteFunctionEnumerableAsync(
			object parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string functionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Function(functionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			IEnumerable<T> results = await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
			return results.AsReadOnly();
		}

		protected async Task ExecuteActionAsync(
			object parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task ExecuteActionAsync(T instance,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Key(instance.ID)
				.Action(actionName);

			await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<T> ExecuteActionSingleAsync(T instance,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Key(instance.ID)
				.Action(actionName);

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<TResult> ExecuteActionScalar<TResult>(
			object parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string actionName = null)
			where TResult : struct, IConvertible
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<T> ExecuteActionSingleAsync(
			object parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		protected async Task<IReadOnlyCollection<T>> ExecuteActionEnumerableAsync(
			object parameters = null,
			CancellationToken cancellationToken = default,
			[CallerMemberName] string actionName = null)
		{
			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			IEnumerable<T> results = await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
			return results.AsReadOnly();
		}
	}
}
