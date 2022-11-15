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
	using Simple.OData.Client;

	/// <summary>
	///     An abstract base class for named OData clients.
	/// </summary>
	[PublicAPI]
	public abstract class ODataClientServiceBase<T, TKey> : IODataClientService
		where T : class, IODataEntity<TKey>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="ODataClientServiceBase{T, TKey}" /> type.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="collectionName"></param>
		/// <param name="oDataClient"></param>
		/// <param name="options"></param>
		protected ODataClientServiceBase(string name, string collectionName, IODataClient oDataClient, RemoteService options)
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(collectionName, nameof(collectionName));
			Guard.Against.Null(oDataClient, nameof(oDataClient));
			Guard.Against.Null(options, nameof(options));

			this.Name = name;
			this.CollectionName = collectionName;
			this.ODataClient = oDataClient;
			this.Options = options;

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

		/// <summary>
		///     Gets the remote service options.
		/// </summary>
		protected RemoteService Options { get; }

		/// <inheritdoc />
		public string Name { get; }

		/// <summary>
		///     Execute a scalar function.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="functionName"></param>
		/// <returns></returns>
		protected async Task<TResult> ExecuteFunctionScalarAsync<TResult>(object parameters = null, CancellationToken cancellationToken = default, [CallerMemberName] string functionName = null)
			where TResult : struct, IConvertible
		{
			functionName = Guard.Against.NullOrWhiteSpace(functionName);

			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Function(functionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Execute a single function.
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="functionName"></param>
		/// <returns></returns>
		protected async Task<T> ExecuteFunctionSingleAsync(object parameters = null, CancellationToken cancellationToken = default, [CallerMemberName] string functionName = null)
		{
			functionName = Guard.Against.NullOrWhiteSpace(functionName);

			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Function(functionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Execute an enumerable function.
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="functionName"></param>
		/// <returns></returns>
		protected async Task<IReadOnlyCollection<T>> ExecuteFunctionEnumerableAsync(object parameters = null, CancellationToken cancellationToken = default, [CallerMemberName] string functionName = null)
		{
			functionName = Guard.Against.NullOrWhiteSpace(functionName);

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

		/// <summary>
		///     Execute an action.
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		protected async Task ExecuteActionAsync(object parameters = null, CancellationToken cancellationToken = default, [CallerMemberName] string actionName = null)
		{
			actionName = Guard.Against.NullOrWhiteSpace(actionName);

			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Execute an action.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		protected async Task ExecuteActionAsync(T instance, CancellationToken cancellationToken = default, [CallerMemberName] string actionName = null)
		{
			actionName = Guard.Against.NullOrWhiteSpace(actionName);

			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Key(instance.ID)
				.Action(actionName);

			await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Execute a single action.
		/// </summary>
		/// <param name="instance"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		protected async Task<T> ExecuteActionSingleAsync(T instance, CancellationToken cancellationToken = default, [CallerMemberName] string actionName = null)
		{
			actionName = Guard.Against.NullOrWhiteSpace(actionName);

			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Key(instance.ID)
				.Action(actionName);

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Execute a scalar action.
		/// </summary>
		/// <typeparam name="TResult"></typeparam>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		protected async Task<TResult> ExecuteActionScalar<TResult>(object parameters = null, CancellationToken cancellationToken = default, [CallerMemberName] string actionName = null)
			where TResult : struct, IConvertible
		{
			actionName = Guard.Against.NullOrWhiteSpace(actionName);

			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Execute a single action.
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		protected async Task<T> ExecuteActionSingleAsync(object parameters = null, CancellationToken cancellationToken = default, [CallerMemberName] string actionName = null)
		{
			actionName = Guard.Against.NullOrWhiteSpace(actionName);

			IBoundClient<T> boundClient = this.ODataClient
				.For<T>(this.CollectionName)
				.Action(actionName);

			if(parameters != null)
			{
				boundClient.Set(parameters);
			}

			return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		}

		/// <summary>
		///     Execute an enumerable action.
		/// </summary>
		/// <param name="parameters"></param>
		/// <param name="cancellationToken"></param>
		/// <param name="actionName"></param>
		/// <returns></returns>
		protected async Task<IReadOnlyCollection<T>> ExecuteActionEnumerableAsync(object parameters = null, CancellationToken cancellationToken = default, [CallerMemberName] string actionName = null)
		{
			actionName = Guard.Against.NullOrWhiteSpace(actionName);

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
