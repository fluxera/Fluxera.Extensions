namespace Fluxera.Extensions.OData
{
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IODataEntity<TKey>
	{
		TKey ID { get; set; }
	}
}
