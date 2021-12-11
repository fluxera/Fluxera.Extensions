namespace Fluxera.Extensions.Validation.UnitTests
{
	using global::FluentValidation;

	public class PersonValidator : AbstractValidator<Person>
	{
		public PersonValidator()
		{
			this.RuleFor(x => x.Name).NotEmpty();
		}
	}
}
