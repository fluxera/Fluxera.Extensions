namespace Fluxera.Extensions.OData
{
	using Fluxera.Guards;
	using Http;
	using JetBrains.Annotations;

	[PublicAPI]
	public abstract class ODataClientServiceBase<T> : HttpClientServiceBase
		where T : class
	{
		protected ODataClientServiceBase(string name, string collectionName, IODataClientFactory oDataClientFactory) 
			: base(name, oDataClientFactory.HttpClientFactory)
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(collectionName, nameof(collectionName));

			this.CollectionName = collectionName;
			//this.ODataClient = oDataClientFactory.CreateClient(name);

			//V4Adapter.Reference();
		}

		protected string CollectionName { get; }

		//protected IODataClient ODataClient { get; }

		//protected async Task<TResult> ExecuteFunctionScalarAsync<TResult>(object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string functionName = null)
		//	where TResult : struct, IConvertible
		//{
		//	IBoundClient<TDto> boundClient = this.ODataClient
		//										 .For<TDto>(this.CollectionName)
		//										 .Function(functionName);

		//	if (parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<TDto> ExecuteFunctionSingleAsync(object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string functionName = null)
		//{
		//	IBoundClient<TDto> boundClient = this.ODataClient
		//										 .For<TDto>(this.CollectionName)
		//										 .Function(functionName);

		//	if (parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<IReadOnlyList<TDto>> ExecuteFunctionEnumerableAsync(object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string functionName = null)
		//{
		//	IBoundClient<TDto> boundClient = this.ODataClient
		//										 .For<TDto>(this.CollectionName)
		//										 .Function(functionName);

		//	if (parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	IEnumerable<TDto> results =
		//		await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
		//	return results.ToReadOnly();
		//}

		//protected async Task ExecuteActionAsync(object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<TDto> boundClient = this.ODataClient
		//										 .For<TDto>(this.CollectionName)
		//										 .Action(actionName);

		//	if (parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task ExecuteActionAsync(TDto dto,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<TDto> boundClient = this.ODataClient
		//										 .For<TDto>(this.CollectionName)
		//										 .Key(dto.ID)
		//										 .Action(actionName);

		//	await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<TDto> ExecuteActionSingleAsync(TDto dto,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<TDto> boundClient = this.ODataClient
		//										 .For<TDto>(this.CollectionName)
		//										 .Key(dto.ID)
		//										 .Action(actionName);

		//	return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<TResult> ExecuteActionScalar<TResult>(object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//	where TResult : struct, IConvertible
		//{
		//	IBoundClient<TDto> boundClient = this.ODataClient
		//										 .For<TDto>(this.CollectionName)
		//										 .Action(actionName);

		//	if (parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<TDto> ExecuteActionSingleAsync(object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<TDto> boundClient = this.ODataClient
		//										 .For<TDto>(this.CollectionName)
		//										 .Action(actionName);

		//	if (parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<IReadOnlyList<TDto>> ExecuteActionEnumerableAsync(object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<TDto> boundClient = this.ODataClient
		//										 .For<TDto>(this.CollectionName)
		//										 .Action(actionName);

		//	if (parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	IEnumerable<TDto> results =
		//		await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
		//	return results.ToReadOnly();
		//}
	}
}
