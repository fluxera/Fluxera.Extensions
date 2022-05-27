// ReSharper disable UnusedTypeParameter

namespace Fluxera.Extensions.OData
{
	using Fluxera.Extensions.Http;
	using Fluxera.Guards;
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

		//public async Task AddAsync(T instance, CancellationToken cancellationToken)
		//{
		//	Guard.Against.Null(instance, nameof(instance));
		//	if(!instance.IsTransient())
		//	{
		//		throw Errors.CanNotAddExistingItem();
		//	}

		//	T result = await this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Set(instance)
		//		.InsertEntryAsync(cancellationToken)
		//		.ConfigureAwait(false);

		//	instance.ID = result.ID;
		//	//this.TransferAuditValues(result, instance);
		//}

		//public async Task AddAsync(IEnumerable<T> instances, CancellationToken cancellationToken = default)
		//{
		//	Guard.Against.Null(instances, nameof(instances));
		//	IList<T> instanceList = instances.ToList();
		//	if(instanceList.Any(x => !x.IsTransient()))
		//	{
		//		throw Errors.CanNotAddExistingItem();
		//	}

		//	ODataBatch batch = new ODataBatch(this.ODataClient);

		//	foreach(T dto in instanceList)
		//	{
		//		batch += async client =>
		//		{
		//			T result = await client
		//				.For<T>(this.CollectionName)
		//				.Set(dto)
		//				.InsertEntryAsync(cancellationToken)
		//				.ConfigureAwait(false);

		//			if(result != null)
		//			{
		//				dto.ID = result.ID;
		//				//this.TransferAuditValues(result, dto);
		//			}
		//		};
		//	}

		//	await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		//}

		//public async Task UpdateAsync(T instance, CancellationToken cancellationToken)
		//{
		//	Guard.Against.Null(instance, nameof(instance));
		//	if(instance.IsTransient())
		//	{
		//		throw Errors.CanNotUpdateTransientItem();
		//	}

		//	object data = instance;
		//	//if(instance is IPatchableEntityDto patchableEntityDto)
		//	//{
		//	//	data = patchableEntityDto.ChangeTracker.GetChangesObject();
		//	//}

		//	T result = await this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Key(instance.ID)
		//		.Set(data)
		//		.UpdateEntryAsync(cancellationToken)
		//		.ConfigureAwait(false);

		//	//this.TransferAuditValues(result, instance);
		//}

		//public async Task UpdateAsync(IEnumerable<T> instances, CancellationToken cancellationToken = default)
		//{
		//	Guard.Against.Null(instances, nameof(instances));
		//	IList<T> instanceList = instances.ToList();
		//	if(instanceList.Any(x => x.IsTransient()))
		//	{
		//		throw Errors.CanNotUpdateTransientItem();
		//	}

		//	ODataBatch batch = new ODataBatch(this.ODataClient);

		//	foreach(T dto in instanceList)
		//	{
		//		object data = dto;
		//		//if(dto is IPatchableEntityDto patchableEntityDto)
		//		//{
		//		//	data = patchableEntityDto.ChangeTracker.GetChangesObject();
		//		//}

		//		batch += async client =>
		//		{
		//			T result = await client
		//				.For<T>(this.CollectionName)
		//				.Key(dto.ID)
		//				.Set(data)
		//				.UpdateEntryAsync(cancellationToken)
		//				.ConfigureAwait(false);

		//			//this.TransferAuditValues(result, dto);
		//		};
		//	}

		//	await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		//}

		//public async Task DeleteAsync(T instance, CancellationToken cancellationToken = default)
		//{
		//	Guard.Against.Null(instance, nameof(instance));
		//	if(instance.IsTransient())
		//	{
		//		throw Errors.CanNotDeleteTransientItem();
		//	}

		//	await this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Key(instance.ID)
		//		.DeleteEntryAsync(cancellationToken)
		//		.ConfigureAwait(false);
		//}

		//public async Task DeleteAsync(TKey id, CancellationToken cancellationToken)
		//{
		//	Guard.Against.Default(id, nameof(id)); // TODO

		//	await this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Key(id)
		//		.DeleteEntryAsync(cancellationToken)
		//		.ConfigureAwait(false);
		//}

		//public async Task DeleteAsync(IEnumerable<T> instances, CancellationToken cancellationToken = default)
		//{
		//	Guard.Against.Null(instances, nameof(instances));

		//	IList<T> instanceList = instances.ToList();
		//	if(instanceList.Any(x => x.IsTransient()))
		//	{
		//		throw Errors.CanNotDeleteTransientItem();
		//	}

		//	ODataBatch batch = new ODataBatch(this.ODataClient);

		//	foreach(T dto in instanceList)
		//	{
		//		batch += async client => await client
		//			.For<T>(this.CollectionName)
		//			.Key(dto.ID)
		//			.DeleteEntryAsync(cancellationToken)
		//			.ConfigureAwait(false);
		//	}

		//	await batch.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		//}

		//public async Task DeleteAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
		//{
		//	Guard.Against.Null(predicate, nameof(predicate));

		//	await this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Filter(predicate)
		//		.DeleteEntryAsync(cancellationToken)
		//		.ConfigureAwait(false);
		//}

		////private void TransferAuditValues(T source, T target)
		////{
		////	if((source != null) && (target != null))
		////	{
		////		if(target is IAuditedObject targetAuditedObject)
		////		{
		////			IAuditedObject sourceAuditedObject = source as IAuditedObject;

		////			targetAuditedObject.CreatedAt = sourceAuditedObject?.CreatedAt;
		////			targetAuditedObject.LastModifiedAt = sourceAuditedObject?.LastModifiedAt;
		////			targetAuditedObject.DeletedAt = sourceAuditedObject?.DeletedAt;

		////			targetAuditedObject.CreatedBy = sourceAuditedObject?.CreatedBy;
		////			targetAuditedObject.LastModifiedBy = sourceAuditedObject?.LastModifiedBy;
		////			targetAuditedObject.DeletedBy = sourceAuditedObject?.DeletedBy;
		////		}
		////	}
		////}

		//protected async Task<T> GetAsync(TKey id, CancellationToken cancellationToken)
		//{
		//	try
		//	{
		//		return await this.ODataClient
		//			.For<T>(this.CollectionName)
		//			.Key(id)
		//			.FindEntryAsync(cancellationToken)
		//			.ConfigureAwait(false);
		//	}
		//	catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
		//	{
		//		return null;
		//	}
		//}

		//protected async Task<TResult> GetAsync<TResult>(TKey id, Expression<Func<T, TResult>> selector,
		//	CancellationToken cancellationToken = default)
		//{
		//	try
		//	{
		//		T item = await this.ODataClient
		//			.For<T>(this.CollectionName)
		//			.Key(id)
		//			.Select(selector.ConvertSelector())
		//			.FindEntryAsync(cancellationToken)
		//			.ConfigureAwait(false);

		//		// HACK: The OData client should return a dict, object or dynamic instead of the entity.
		//		return selector.Compile().Invoke(item);
		//	}
		//	catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
		//	{
		//		return default;
		//	}
		//}

		//protected async Task<bool> ExistsAsync(TKey id, CancellationToken cancellationToken)
		//{
		//	T result = await this.GetAsync(id, cancellationToken).ConfigureAwait(false);
		//	return result != null;
		//}

		//protected async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate,
		//	CancellationToken cancellationToken = default)
		//{
		//	long count = await this.CountAsync(predicate, cancellationToken).ConfigureAwait(false);
		//	return count > 0;
		//}

		//protected async Task<long> CountAsync(CancellationToken cancellationToken = default)
		//{
		//	return await this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Count()
		//		.FindScalarAsync<long>(cancellationToken)
		//		.ConfigureAwait(false);
		//}

		//protected async Task<long> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
		//{
		//	return await this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Filter(predicate)
		//		.Count()
		//		.FindScalarAsync<long>(cancellationToken)
		//		.ConfigureAwait(false);
		//}

		//protected async Task<T> FindOneAsync(Expression<Func<T, bool>> predicate,
		//	//IQueryOptions<T> queryOptions = null, 
		//	CancellationToken cancellationToken = default)
		//{
		//	try
		//	{
		//		return await this.ODataClient
		//			.For<T>(this.CollectionName)
		//			.Filter(predicate)
		//			//.ApplyOptions(queryOptions)
		//			.FindEntryAsync(cancellationToken)
		//			.ConfigureAwait(false);
		//	}
		//	catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
		//	{
		//		return null;
		//	}
		//}

		//protected async Task<TResult> FindOneAsync<TResult>(Expression<Func<T, bool>> predicate,
		//	Expression<Func<T, TResult>> selector,
		//	//IQueryOptions<T> queryOptions = null,
		//	CancellationToken cancellationToken = default)
		//{
		//	try
		//	{
		//		T result = await this.ODataClient
		//			.For<T>(this.CollectionName)
		//			.Filter(predicate)
		//			//.ApplyOptions(queryOptions)
		//			.Select(selector.ConvertSelector())
		//			.FindEntryAsync(cancellationToken)
		//			.ConfigureAwait(false);

		//		// HACK: The OData client should return a dict, object or dynamic instead of the entity.
		//		return selector.Compile().Invoke(result);
		//	}
		//	catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
		//	{
		//		return default;
		//	}
		//}

		//protected async Task<IReadOnlyCollection<T>> FindManyAsync(Expression<Func<T, bool>> predicate,
		//	//IQueryOptions<T> queryOptions = null, 
		//	CancellationToken cancellationToken = default)
		//{
		//	try
		//	{
		//		IEnumerable<T> results = await this.ODataClient
		//			.For<T>(this.CollectionName)
		//			.Filter(predicate)
		//			//.ApplyOptions(queryOptions)
		//			.FindEntriesAsync(cancellationToken)
		//			.ConfigureAwait(false);

		//		return results.AsReadOnly();
		//	}
		//	catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
		//	{
		//		return Enumerable.Empty<T>().AsReadOnly();
		//	}
		//}

		//protected async Task<IReadOnlyCollection<TResult>> FindManyAsync<TResult>(Expression<Func<T, bool>> predicate,
		//	Expression<Func<T, TResult>> selector,
		//	//IQueryOptions<T> queryOptions = null,
		//	CancellationToken cancellationToken = default)
		//{
		//	try
		//	{
		//		IEnumerable<T> results = await this.ODataClient
		//			.For<T>(this.CollectionName)
		//			.Filter(predicate)
		//			//.ApplyOptions(queryOptions)
		//			.Select(selector.ConvertSelector())
		//			.FindEntriesAsync(cancellationToken)
		//			.ConfigureAwait(false);

		//		// HACK: The OData client should return a dict, object or dynamic instead of the entity.
		//		Func<T, TResult> selectorFunc = selector.Compile();

		//		IList<TResult> result = new List<TResult>();
		//		foreach(T item in results)
		//		{
		//			result.Add(selectorFunc.Invoke(item));
		//		}

		//		return result.AsReadOnly();
		//	}
		//	catch(WebRequestException ex) when(ex.Code == HttpStatusCode.NotFound)
		//	{
		//		return Enumerable.Empty<TResult>().AsReadOnly();
		//	}
		//}

		//protected async Task<TResult> ExecuteFunctionScalarAsync<TResult>(
		//	object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string functionName = null)
		//	where TResult : struct, IConvertible
		//{
		//	IBoundClient<T> boundClient = this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Function(functionName);

		//	if(parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<T> ExecuteFunctionSingleAsync(
		//	object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string functionName = null)
		//{
		//	IBoundClient<T> boundClient = this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Function(functionName);

		//	if(parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<IReadOnlyCollection<T>> ExecuteFunctionEnumerableAsync(
		//	object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string functionName = null)
		//{
		//	IBoundClient<T> boundClient = this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Function(functionName);

		//	if(parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	IEnumerable<T> results = await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
		//	return results.AsReadOnly();
		//}

		//protected async Task ExecuteActionAsync(
		//	object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<T> boundClient = this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Action(actionName);

		//	if(parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task ExecuteActionAsync(T instance,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<T> boundClient = this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Key(instance.ID)
		//		.Action(actionName);

		//	await boundClient.ExecuteAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<T> ExecuteActionSingleAsync(T instance,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<T> boundClient = this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Key(instance.ID)
		//		.Action(actionName);

		//	return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<TResult> ExecuteActionScalar<TResult>(
		//	object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//	where TResult : struct, IConvertible
		//{
		//	IBoundClient<T> boundClient = this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Action(actionName);

		//	if(parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	return await boundClient.ExecuteAsScalarAsync<TResult>(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<T> ExecuteActionSingleAsync(
		//	object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<T> boundClient = this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Action(actionName);

		//	if(parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	return await boundClient.ExecuteAsSingleAsync(cancellationToken).ConfigureAwait(false);
		//}

		//protected async Task<IReadOnlyCollection<T>> ExecuteActionEnumerableAsync(
		//	object parameters = null,
		//	CancellationToken cancellationToken = default,
		//	[CallerMemberName] string actionName = null)
		//{
		//	IBoundClient<T> boundClient = this.ODataClient
		//		.For<T>(this.CollectionName)
		//		.Action(actionName);

		//	if(parameters != null)
		//	{
		//		boundClient.Set(parameters);
		//	}

		//	IEnumerable<T> results = await boundClient.ExecuteAsEnumerableAsync(cancellationToken).ConfigureAwait(false);
		//	return results.AsReadOnly();
		//}
	}
}
