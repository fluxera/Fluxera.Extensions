namespace Fluxera.Extensions.Common
{
	using System.Security.Cryptography;
	using System.Text;
	using Fluxera.Guards;

	internal sealed class HashCalculator : IHashCalculator
	{
		public byte[] ComputeHash(byte[] input, HashAlgorithm algorithm)
		{
			Guard.Against.Null(input, nameof(input));
			Guard.Against.Null(algorithm, nameof(algorithm));

			using(algorithm)
			{
				return algorithm.ComputeHash(input);
			}
		}

		/// <inheritdoc />
		public string ComputeHash(string input, HashAlgorithm algorithm = null)
		{
			Guard.Against.Null(input, nameof(input));

			return this.ComputeHash(input, Encoding.UTF8, algorithm);
		}

		/// <inheritdoc />
		public string ComputeHash(string input, Encoding encoding, HashAlgorithm algorithm = null)
		{
			Guard.Against.Null(input, nameof(input));
			Guard.Against.Null(encoding, nameof(encoding));

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
