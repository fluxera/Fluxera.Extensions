namespace Fluxera.Extensions.Http
{
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///		Provides the remote service configurations.
	/// </summary>
	[PublicAPI]
	public sealed class RemoteServiceConfigurationDictionary : Dictionary<string, RemoteServiceConfiguration>
	{
		public const string DefaultRemoteServiceName = "Default";

		/// <summary>
		///		Gets or sets the default remote service configuration.
		/// </summary>
		public RemoteServiceConfiguration? Default
		{
			get => this.GetOrDefault(DefaultRemoteServiceName);
			set => this[DefaultRemoteServiceName] = value!;
		}
	}
}
