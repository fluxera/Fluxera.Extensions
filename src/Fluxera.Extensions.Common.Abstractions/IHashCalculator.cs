namespace Fluxera.Extensions.Common
{
	using System.Security.Cryptography;
	using System.Text;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IHashCalculator
	{
		/// <summary>
		///     Calculates a hash using the given algorithm.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="algorithm"></param>
		/// <returns></returns>
		byte[] ComputeHash(byte[] input, HashAlgorithm algorithm);

		/// <summary>
		///     Calculates a hash using the given algorithm. If no algorithm is applied <see cref="MD5" /> is used.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="algorithm"></param>
		/// <returns></returns>
		string ComputeHash(string input, HashAlgorithm algorithm = null);

		/// <summary>
		///     Calculates a hash using the given algorithm. If no algorithm is applied <see cref="MD5" /> is used.
		/// </summary>
		/// <param name="input"></param>
		/// <param name="encoding"></param>
		/// <param name="algorithm"></param>
		/// <returns></returns>
		string ComputeHash(string input, Encoding encoding, HashAlgorithm algorithm = null);
	}
}
