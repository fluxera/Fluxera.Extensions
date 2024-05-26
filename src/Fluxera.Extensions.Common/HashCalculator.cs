namespace Fluxera.Extensions.Common
{
	using System.Security.Cryptography;
	using System.Text;

	internal sealed class HashCalculator : IHashCalculator
	{
		public byte[] ComputeHash(byte[] input, HashAlgorithm algorithm)
		{
			Guard.ThrowIfNull(input);
			Guard.ThrowIfNull(algorithm);

			using(algorithm)
			{
				return algorithm.ComputeHash(input);
			}
		}

		/// <inheritdoc />
		public string ComputeHash(string input, HashAlgorithm algorithm = null)
		{
			Guard.ThrowIfNull(input);

			return this.ComputeHash(input, Encoding.UTF8, algorithm);
		}

		/// <inheritdoc />
		public string ComputeHash(string input, Encoding encoding, HashAlgorithm algorithm = null)
		{
			Guard.ThrowIfNull(input);
			Guard.ThrowIfNull(encoding);

			// Step 1, calculate hash from input.
			algorithm ??= MD5.Create();
			using(algorithm)
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(input);
				byte[] hashBytes = this.ComputeHash(inputBytes, algorithm);

				// Step 2, convert byte array to hex string.
				StringBuilder sb = new StringBuilder();
				foreach(byte b in hashBytes)
				{
					sb.Append(b.ToString("X2"));
				}

				return sb.ToString();
			}
		}
	}
}
