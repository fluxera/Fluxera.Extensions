namespace Fluxera.Extensions.Common
{
	using JetBrains.Annotations;

	/// <summary>
	///     Options used by <see cref="IStringEncryptionService" />.
	/// </summary>
	[PublicAPI]
	public sealed class StringEncryptionOptions
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="StringEncryptionOptions" />.
		/// </summary>
		public StringEncryptionOptions()
		{
			this.KeySize = 256;
		}

		/// <summary>
		///     This constant is used to determine the key-size of the encryption algorithm.
		///     The default value is 256.
		/// </summary>
		public int KeySize { get; set; }

		/// <summary>
		///     The default password to encrypt/decrypt texts.
		/// </summary>
		public string DefaultPassPhrase { get; set; }

		/// <summary>
		///     This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
		///     This size of the IV (in bytes) must = (key-size / 16). The default key-size is 256, so the
		///     IV must be 16 bytes long.
		/// </summary>
		public byte[] InitVectorBytes { get; set; }

		/// <summary>
		///     Gets the default salt bytes.
		/// </summary>
		public byte[] DefaultSalt { get; set; }
	}
}
