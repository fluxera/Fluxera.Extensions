namespace Fluxera.Extensions.Validation{	using System.Threading.Tasks;	using JetBrains.Annotations;	/// <summary>
	///		A contract for validators. The underlying validation frameworks must implement this.
	/// </summary>	[PublicAPI]	public interface IValidator	{		Task<ValidationResult> ValidateAsync(object instance);	}}