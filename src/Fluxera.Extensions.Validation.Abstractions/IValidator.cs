namespace Fluxera.Extensions.Validation
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for validators. The underlying validation frameworks must implement this.
	/// </summary>
	[PublicAPI]
	public interface IValidator
	{
		/// <summary>
		///     Validates the given instance.
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		Task<ValidationResult> ValidateAsync(object instance);
	}
}
