namespace Fluxera.Extensions.Http
{
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     The remote service options.
	/// </summary>
	[PublicAPI]
	public sealed class RemoteService : Dictionary<string, string>
	{
		/// <summary>
		///     Creates a new instance of the <see cref="RemoteService" /> type.
		/// </summary>
		public RemoteService()
		{
		}

		/// <summary>
		///     Creates a new instance of the <see cref="RemoteService" /> type.
		/// </summary>
		/// <param name="baseAddress"></param>
		/// <param name="version"></param>
		public RemoteService(string baseAddress, string version = "")
		{
			this.BaseAddress = baseAddress;
			this.Version = version;
		}

		/// <summary>
		///     Gets or sets the base URL of the remote service.
		/// </summary>
		public string BaseAddress
		{
			get => this.GetOrDefault(nameof(this.BaseAddress));
			set => this[nameof(this.BaseAddress)] = value;
		}

		/// <summary>
		///     Gets or sets the version of the remote service.
		/// </summary>
		public string Version
		{
			get => this.GetOrDefault(nameof(this.Version));
			set => this[nameof(this.Version)] = value;
		}
	}
}
