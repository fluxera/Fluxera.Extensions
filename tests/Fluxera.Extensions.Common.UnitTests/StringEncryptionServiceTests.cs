namespace Fluxera.Extensions.Common.UnitTests
{
	using System;
	using System.Text;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class StringEncryptionServiceTests
	{
		[SetUp]
		public void SetUp()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddStringEncryptionService();
			services.Configure<StringEncryptionOptions>(options =>
			{
				options.DefaultPassPhrase = "password";
				options.DefaultSalt = Encoding.ASCII.GetBytes("12345678");
				options.InitVectorBytes = Encoding.ASCII.GetBytes("1234567890abcdef");
			});

			IServiceProvider serviceProvider = services.BuildServiceProvider();
			this.stringEncryptionService = serviceProvider.GetRequiredService<IStringEncryptionService>();
		}

		private IStringEncryptionService stringEncryptionService;

		[Test]
		public void ShouldDecryptString()
		{
			const string str = "Hello, World!";

			string encrypted = this.stringEncryptionService.Encrypt(str);
			string decrypted = this.stringEncryptionService.Decrypt(encrypted);

			decrypted.Should().Be(str);
		}

		[Test]
		public async Task ShouldDecryptStringAsync()
		{
			const string str = "Hello, World!";

			string encrypted = await this.stringEncryptionService.EncryptAsync(str);
			string decrypted = await this.stringEncryptionService.DecryptAsync(encrypted);

			decrypted.Should().Be(str);
		}

		[Test]
		public void ShouldEncryptString()
		{
			const string str = "Hello, World!";

			string encrypted = this.stringEncryptionService.Encrypt(str);

			encrypted.Should().NotBe(str);
		}

		[Test]
		public async Task ShouldEncryptStringAsync()
		{
			const string str = "Hello, World!";

			string encrypted = await this.stringEncryptionService.EncryptAsync(str);

			encrypted.Should().NotBe(str);
		}
	}
}
