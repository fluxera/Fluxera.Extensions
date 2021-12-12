namespace Fluxera.Extensions.Validation.UnitTests
{
	using System.ComponentModel.DataAnnotations;

	public class Person
	{
		[Required]
		public string Name { get; set; }

		public string Address { get; set; }
	}
}
