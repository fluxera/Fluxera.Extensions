namespace Fluxera.Extensions.Validation{	using JetBrains.Annotations;	/// <summary>
	///		A contract for a validator factory. The underlying validation frameworks must implement this.
	/// </summary>	[PublicAPI]	public interface IValidatorFactory	{		/// <summary>
		///		Creates the validator to use.
		/// </summary>
		/// <returns>The validation instance.</returns>		IValidator CreateValidator();	}}