namespace Fluxera.Extensions.Http
{
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///		Provides the remote service configuration.
	/// </summary>
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

		/// <summary>
		///		Gets or sets the base URL of the remote service.
		/// </summary>
		public string? BaseUrl
		{
			get => this.GetOrDefault(nameof(this.BaseUrl));
			set => this[nameof(this.BaseUrl)] = value!;
		}

		/// <summary>
		///		Gets or sets the version of the remote service.
		/// </summary>
		public string? Version
		{
			get => this.GetOrDefault(nameof(Version));
			set => this[nameof(this.Version)] = value!;
		}
	}
}
