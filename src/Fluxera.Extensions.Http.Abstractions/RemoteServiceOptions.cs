namespace Fluxera.Extensions.Http
{
	using JetBrains.Annotations;

	/// <summary>
	///		Provides the remote service options.
	/// </summary>
	[PublicAPI]
	public sealed class RemoteServiceOptions
	{
		public RemoteServiceOptions()
		{
			this.RemoteServices = new RemoteServiceConfigurationDictionary();
		}

		/// <summary>
		///		Provides the remote service configurations.
		/// </summary>
		public RemoteServiceConfigurationDictionary RemoteServices { get; set; }
	}
}
