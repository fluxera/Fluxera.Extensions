namespace Fluxera.Extensions.Http
{
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class RemoteServiceOptions
	{
		public RemoteServiceOptions()
		{
			this.RemoteServices = new RemoteServiceConfigurationDictionary();
		}

		public RemoteServiceConfigurationDictionary RemoteServices { get; set; }
	}
}
