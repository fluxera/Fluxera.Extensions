namespace Fluxera.Extensions.Common
{
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.Extensions.Options;

	internal sealed class StringEncryptionOptionsValidator : IValidateOptions<StringEncryptionOptions>
	{
		public ValidateOptionsResult Validate(string name, StringEncryptionOptions options)
		{
			IList<string> failures = new List<string>();

			if(options.KeySize % 8 != 0)
			{
				failures.Add("The key size must be a multiple of 8");
			}

			if(string.IsNullOrEmpty(options.DefaultPassPhrase))
			{
				failures.Add("A default pass phrase must be configured");
			}

			if(options.InitVectorBytes is null || !options.InitVectorBytes.Any())
			{
				failures.Add("The init vector bytes must be configured");
			}
			else
			{
				if(options.InitVectorBytes.Length != options.KeySize / 16)
				{
					failures.Add("The init vector must be of length key-size / 8");
				}
			}

			if(options.DefaultSalt is null || !options.DefaultSalt.Any())
			{
				failures.Add("The default salt must be configured");
			}

			return failures.Any()
				? ValidateOptionsResult.Fail(failures)
				: ValidateOptionsResult.Success;
		}
	}
}
