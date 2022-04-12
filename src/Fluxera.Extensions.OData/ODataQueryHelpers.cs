namespace Fluxera.Extensions.OData
{
	using Simple.OData.Client;

	internal static class ODataQueryHelpers
	{
		internal static IBoundClient<T> ApplyOptions<T>(
			this IBoundClient<T> client /*, IQueryOptions<T> queryOptions*/)
			where T : class
		{
			//QueryOptions<TDto> options = (QueryOptions<TDto>) queryOptions;

			//if (options.HasPagingOptions())
			//{
			//	if (options.PagingOptions is PagingOptions<TDto> pagingOptions)
			//	{
			//		client = client.Skip(pagingOptions.Skip).Top(pagingOptions.PageSize);
			//	}
			//}

			//if (options.HasSkipTakeOptions())
			//{
			//	if (options.SkipTakeOptions.Skip.HasValue)
			//	{
			//		client = client.Skip(options.SkipTakeOptions.Skip.Value);
			//	}

			//	if (options.SkipTakeOptions.Take.HasValue)
			//	{
			//		client = client.Top(options.SkipTakeOptions.Take.Value);
			//	}
			//}

			//if (options.HasOrderByOptions())
			//{
			//	OrderByOptions<TDto> orderByOptions = options.OrderByOptions as OrderByOptions<TDto>;
			//	OrderByExpressionContainer<TDto> sortBy = orderByOptions?.OrderByExpression;
			//	if (sortBy != null)
			//	{
			//		IBoundClient<TDto> orderedClient = sortBy.IsDescending
			//			? client.OrderByDescending(sortBy.SortExpression)
			//			: client.OrderBy(sortBy.SortExpression);

			//		if (orderByOptions.ThenByExpressions != null)
			//		{
			//			foreach (OrderByExpressionContainer<TDto> thenBy in orderByOptions.ThenByExpressions)
			//			{
			//				orderedClient = thenBy.IsDescending
			//					? orderedClient.ThenBy(thenBy.SortExpression)
			//					: orderedClient.ThenByDescending(thenBy.SortExpression);
			//			}
			//		}

			//		client = orderedClient;
			//	}
			//}

			return client;
		}
	}
}
