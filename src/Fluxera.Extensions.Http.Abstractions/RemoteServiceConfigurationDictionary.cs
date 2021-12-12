namespace Fluxera.Extensions.Http
{
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class RemoteServiceConfigurationDictionary : Dictionary<string, RemoteServiceConfiguration>
	{
		public const string DefaultRemoteServiceName = "Default";

		public RemoteServiceConfiguration? Default
		{
			get => this.GetOrDefault(DefaultRemoteServiceName);
			set => this[DefaultRemoteServiceName] = value!;
		}

		//public RemoteServiceConfiguration GetConfigurationOrDefault(string name)
		//{
		//	return this.GetOrDefault(name)
		//		   ?? this.Default
		//		   ?? throw new Exception(
		//			   $"Remote service '{name}' was not found and there is no default configuration.");
		//}
	}
}
