namespace Fluxera.Extensions.Common
{
	using System;
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;
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
			if (plainText == null)
			{
				return null;
			}

			if (passPhrase == null)
			{
				passPhrase = options.DefaultPassPhrase;
			}

			if (salt == null)
			{
				salt = options.DefaultSalt;
			}

			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, salt))
			{
				byte[] keyBytes = password.GetBytes(options.KeySize / 8);
				using (Aes symmetricKey = Aes.Create())
				{
					symmetricKey.Mode = CipherMode.CBC;
					using (ICryptoTransform cryptoTransform =
						symmetricKey.CreateEncryptor(keyBytes, options.InitVectorBytes))
					{
						using (MemoryStream memoryStream = new MemoryStream())
						{
							using (CryptoStream cryptoStream =
								new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
							{
								cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
								cryptoStream.FlushFinalBlock();
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
			if (string.IsNullOrEmpty(cipherText))
			{
				return null;
			}

			if (passPhrase == null)
			{
				passPhrase = options.DefaultPassPhrase;
			}

			if (salt == null)
			{
				salt = options.DefaultSalt;
			}

			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, salt))
			{
				byte[] keyBytes = password.GetBytes(options.KeySize / 8);
				using (Aes symmetricKey = Aes.Create())
				{
					symmetricKey.Mode = CipherMode.CBC;
					using (ICryptoTransform cryptoTransform =
						symmetricKey.CreateDecryptor(keyBytes, options.InitVectorBytes))
					{
						using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
						{
							using (CryptoStream cryptoStream =
								new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
							{
								byte[] plainTextBytes = new byte[cipherTextBytes.Length];
								int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
								return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
							}
						}
					}
				}
			}
		}
	}
}
