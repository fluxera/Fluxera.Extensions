namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;

	/// <summary>
	///     Extension methods for the odata client.
	/// </summary>
	[PublicAPI]
	public static class ODataClientServiceExtensions
	{
		/// <summary>
		///     Checks if the entity is transient.
		/// </summary>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="entity"></param>
		/// <returns></returns>
		public static bool IsTransient<TKey>(this IODataEntity<TKey> entity)
		{
			return entity.ID.Equals(default);
		}
	}
}
