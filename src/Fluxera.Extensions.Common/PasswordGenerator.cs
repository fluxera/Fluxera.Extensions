namespace Fluxera.Extensions.Common
{
	using System;
	using System.Security.Cryptography;
	using System.Text;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[UsedImplicitly]
	internal sealed class PasswordGenerator : IPasswordGenerator
	{
		// ReSharper disable StringLiteralTypo
		private static readonly char[] pwdCharArray =
			"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890!$+-".ToCharArray();
		// ReSharper restore StringLiteralTypo

		private readonly RNGCryptoServiceProvider rng;

		public PasswordGenerator()
		{
			ConsecutiveCharacters = false;
			RepeatCharacters = true;
			rng = new RNGCryptoServiceProvider();
		}

		public bool RepeatCharacters { get; set; }

		public bool ConsecutiveCharacters { get; set; }

		/// <inheritdoc />
		public string GeneratePassword(int length)
		{
			// Pick random length between minimum and maximum   
			int pwdLength = GetCryptographicRandomNumber(length, length);

			StringBuilder pwdBuffer = new StringBuilder
			{
				Capacity = length,
			};

			// Generate random characters
			char nextCharacter = '\n';

			// Initial dummy character flag
			char lastCharacter = nextCharacter;

			for (int i = 0; i < pwdLength; i++)
			{
				nextCharacter = GetRandomCharacter();

				if (!ConsecutiveCharacters)
				{
					while (lastCharacter == nextCharacter)
					{
						nextCharacter = GetRandomCharacter();
					}
				}

				if (!RepeatCharacters)
				{
					string temp = pwdBuffer.ToString();
					int duplicateIndex = temp.IndexOf(nextCharacter);
					while (-1 != duplicateIndex)
					{
						nextCharacter = GetRandomCharacter();
						duplicateIndex = temp.IndexOf(nextCharacter);
					}
				}

				pwdBuffer.Append(nextCharacter);
				lastCharacter = nextCharacter;
			}

			return pwdBuffer.ToString();
		}

		private int GetCryptographicRandomNumber(int minimumValue, int maximumValue)
		{
			byte[] randomNumber = new byte[1];

			rng.GetBytes(randomNumber);

			double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

			// We are using Math.Max, and subtracting 0.00000000001, 
			// to ensure "multiplier" will always be between 0.0 and .99999999999
			// Otherwise, it's possible for it to be "1", which causes problems in our rounding.
			double multiplier = Math.Max(0, asciiValueOfRandomCharacter / 255d - 0.00000000001d);

			// We need to add one to the range, to allow for the rounding done with Math.Floor
			int range = maximumValue - minimumValue + 1;

			double randomValueInRange = Math.Floor(multiplier * range);
			return (int)(minimumValue + randomValueInRange);
		}

		private char GetRandomCharacter()
		{
			int randomCharPosition = GetCryptographicRandomNumber(0, pwdCharArray.Length - 1);

			char randomChar = pwdCharArray[randomCharPosition];

			return randomChar;
		}
	}
}
