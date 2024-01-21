namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;

	/// <summary>
	///     Contract for a model that is used as OData entity in an OData client service.
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public interface IODataEntity<TKey>
	{
		/// <summary>
		///     Gets or sets the ID of the OData entity.
		/// </summary>
		TKey ID { get; set; }
	}
}
