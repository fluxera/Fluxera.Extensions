namespace Fluxera.Extensions.Http
{
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class RemoteServiceConfiguration : Dictionary<string, string>
	{
		public RemoteServiceConfiguration()
		{
		}

		public RemoteServiceConfiguration(string baseUrl , string? version = null)
		{
			this[nameof(this.BaseUrl)] = baseUrl;
			this[nameof(Version)] = version!;
		}

		public string? BaseUrl
		{
			get => this.GetOrDefault(nameof(this.BaseUrl));
			set => this[nameof(this.BaseUrl)] = value!;
		}

		public string? Version
		{
			get => this.GetOrDefault(nameof(Version));
			set => this[nameof(Version)] = value!;
		}
	}
}
