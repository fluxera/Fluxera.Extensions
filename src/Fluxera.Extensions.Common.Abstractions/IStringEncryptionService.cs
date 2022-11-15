namespace Fluxera.Extensions.Common
{
	using System.Threading;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///     Can be used to simply encrypt/decrypt texts.
	/// </summary>
	[PublicAPI]
	public interface IStringEncryptionService
	{
		/// <summary>
		///     Encrypts a text.
		/// </summary>
		/// <param name="plainText">The text in plain format</param>
		/// <param name="passPhrase">A phrase to use as the encryption key (optional, uses default if not provided)</param>
		/// <param name="salt">Salt value (optional, uses default if not provided)</param>
		/// <returns>Encrypted text</returns>
		string Encrypt(string plainText, string passPhrase = null, byte[] salt = null);

		/// <summary>
		///     Encrypts a text.
		/// </summary>
		/// <param name="plainText">The text in plain format</param>
		/// <param name="passPhrase">A phrase to use as the encryption key (optional, uses default if not provided)</param>
		/// <param name="salt">Salt value (optional, uses default if not provided)</param>
		/// <param name="cancellationToken"></param>
		/// <returns>Encrypted text</returns>
		Task<string> EncryptAsync(string plainText, string passPhrase = null, byte[] salt = null, CancellationToken cancellationToken = default);

		/// <summary>
		///     Decrypts a text that is encrypted by the <see cref="Encrypt" /> method.
		/// </summary>
		/// <param name="cipherText">The text in encrypted format</param>
		/// <param name="passPhrase">A phrase to use as the encryption key (optional, uses default if not provided)</param>
		/// <param name="salt">Salt value (optional, uses default if not provided)</param>
		/// <returns>Decrypted text</returns>
		string Decrypt(string cipherText, string passPhrase = null, byte[] salt = null);

		/// <summary>
		///     Decrypts a text that is encrypted by the <see cref="Encrypt" /> method.
		/// </summary>
		/// <param name="cipherText">The text in encrypted format</param>
		/// <param name="passPhrase">A phrase to use as the encryption key (optional, uses default if not provided)</param>
		/// <param name="salt">Salt value (optional, uses default if not provided)</param>
		/// <param name="cancellationToken"></param>
		/// <returns>Decrypted text</returns>
		Task<string> DecryptAsync(string cipherText, string passPhrase = null, byte[] salt = null, CancellationToken cancellationToken = default);
	}
}
