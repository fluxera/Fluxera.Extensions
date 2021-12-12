namespace Fluxera.Extensions.Common
{
	using JetBrains.Annotations;

	/// <summary>
	///     A service that generates random passwords.
	/// </summary>
	[PublicAPI]
	public interface IPasswordGenerator
	{
		/// <summary>
		///     Generates a random password having the specified length.
		/// </summary>
		/// <param name="length">The desired length of the password.</param>
		/// <returns>The new random password.</returns>
		string GeneratePassword(int length);
	}
}
