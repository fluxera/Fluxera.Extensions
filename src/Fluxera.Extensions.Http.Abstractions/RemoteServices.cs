namespace Fluxera.Extensions.Http
{
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///     The remote services options.
	/// </summary>
	[PublicAPI]
	public sealed class RemoteServices : Dictionary<string, RemoteService>
	{
		/// <summary>
		///     The name of the default remote service.
		/// </summary>
		public const string DefaultRemoteServiceName = "Default";

		/// <summary>
		///     Gets or sets the default remote service configuration.
		/// </summary>
		public RemoteService Default
		{
			get => this.GetOrDefault(DefaultRemoteServiceName);
			set => this[DefaultRemoteServiceName] = value;
		}
	}
}
