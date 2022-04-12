namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;

	[PublicAPI]
	public static class ODataClientServiceExtensions
	{
		public static bool IsTransient<TKey>(this IODataEntity<TKey> entity)
		{
			return entity.ID.Equals(default); // TODO
		}

		///// <summary>
		/////     Adds the item or updates it.
		///// </summary>
		///// <typeparam name="TDto">The type of the dto.</typeparam>
		///// <param name="crudApplicationService">The crud application service.</param>
		///// <param name="dto">The dto.</param>
		///// <param name="cancellationToken">The cancellation token.</param>
		//public static async Task AddOrUpdateAsync<TDto>(this ICrudApplicationService<TDto> crudApplicationService, TDto dto, CancellationToken cancellationToken = default)
		//	where TDto : class, IEntityDto
		//{
		//	Guard.AgainstNull(nameof(crudApplicationService), crudApplicationService);
		//	Guard.AgainstNull(nameof(dto), dto);

		//	if (dto.IsTransient())
		//	{
		//		await crudApplicationService.AddAsync(dto, cancellationToken);
		//	}
		//	else
		//	{
		//		await crudApplicationService.UpdateAsync(dto, cancellationToken);
		//	}
		//}

		///// <summary>
		/////     Adds the item or updates it.
		///// </summary>
		///// <typeparam name="TDto">The type of the dto.</typeparam>
		///// <param name="crudApplicationService">The crud application service.</param>
		///// <param name="dtos">The dtos.</param>
		///// <param name="cancellationToken">The cancellation token.</param>
		//public static async Task AddOrUpdateAsync<TDto>(this ICrudApplicationService<TDto> crudApplicationService, IEnumerable<TDto> dtos, CancellationToken cancellationToken = default)
		//	where TDto : class, IEntityDto
		//{
		//	Guard.AgainstNull(nameof(crudApplicationService), crudApplicationService);
		//	Guard.AgainstNull(nameof(dtos), dtos);

		//	foreach (TDto item in dtos)
		//	{
		//		await crudApplicationService.AddOrUpdateAsync(item, cancellationToken);
		//	}
		//}
	}
}
