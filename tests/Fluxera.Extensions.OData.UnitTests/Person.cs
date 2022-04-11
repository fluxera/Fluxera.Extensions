namespace Fluxera.Extensions.OData.UnitTests
{
	public class Person : IODataEntity<string>
	{
		/// <inheritdoc />
		public string ID { get; set; }
	}
}
