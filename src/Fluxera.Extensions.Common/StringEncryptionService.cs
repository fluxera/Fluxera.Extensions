namespace Fluxera.Extensions.Common
{
	using System;
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Utilities;
	using Microsoft.Extensions.Options;

	/// <summary>
	///     Can be used to simply encrypt/decrypt texts.
	/// </summary>
	internal sealed class StringEncryptionService : IStringEncryptionService
	{
		private readonly StringEncryptionOptions options;

		public StringEncryptionService(IOptions<StringEncryptionOptions> options)
		{
			this.options = options.Value;
		}

		public string Encrypt(string plainText, string passPhrase = null, byte[] salt = null)
		{
			return AsyncHelper.RunSync(() => this.EncryptAsync(plainText, passPhrase, salt, CancellationToken.None));
		}

		/// <inheritdoc />
		public async Task<string> EncryptAsync(string plainText, string passPhrase = null, byte[] salt = null, CancellationToken cancellationToken = default)
		{
			if(plainText == null)
			{
				return null;
			}

			passPhrase ??= this.options.DefaultPassPhrase;
			salt ??= this.options.DefaultSalt;

			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			using(Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, salt, 1000, HashAlgorithmName.SHA256))
			{
				byte[] keyBytes = password.GetBytes(this.options.KeySize / 8);
				using(Aes symmetricKey = Aes.Create())
				{
					symmetricKey.Mode = CipherMode.CBC;
					using(ICryptoTransform cryptoTransform = symmetricKey.CreateEncryptor(keyBytes, this.options.InitVectorBytes))
					{
						using(MemoryStream memoryStream = new MemoryStream())
						{
							await using(CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
							{
								await cryptoStream.WriteAsync(plainTextBytes, 0, plainTextBytes.Length, cancellationToken);
								await cryptoStream.FlushFinalBlockAsync(cancellationToken);
								byte[] cipherTextBytes = memoryStream.ToArray();
								return Convert.ToBase64String(cipherTextBytes);
							}
						}
					}
				}
			}
		}

		public string Decrypt(string cipherText, string passPhrase = null, byte[] salt = null)
		{
			return AsyncHelper.RunSync(() => this.DecryptAsync(cipherText, passPhrase, salt, CancellationToken.None));
		}

		/// <inheritdoc />
		public async Task<string> DecryptAsync(string cipherText, string passPhrase = null, byte[] salt = null, CancellationToken cancellationToken = default)
		{
			if(string.IsNullOrEmpty(cipherText))
			{
				return null;
			}

			passPhrase ??= this.options.DefaultPassPhrase;
			salt ??= this.options.DefaultSalt;

			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			using(Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, salt, 1000, HashAlgorithmName.SHA256))
			{
				byte[] keyBytes = password.GetBytes(this.options.KeySize / 8);
				using(Aes symmetricKey = Aes.Create())
				{
					symmetricKey.Mode = CipherMode.CBC;
					using(ICryptoTransform cryptoTransform = symmetricKey.CreateDecryptor(keyBytes, this.options.InitVectorBytes))
					{
						using(MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
						{
							await using(CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
							{
								byte[] plainTextBytes = new byte[cipherTextBytes.Length];
								int decryptedByteCount = await cryptoStream.ReadAsync(plainTextBytes, 0, plainTextBytes.Length, cancellationToken);
								return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
							}
						}
					}
				}
			}
		}
	}
}
