namespace Fluxera.Extensions.DataManagement
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;

	/// <summary>
	///		Provides the connection strings.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ConnectionStrings : Dictionary<string, string>
	{
		public const string DefaultConnectionStringName = "Default";

		public string? Default
		{
			get => this.GetOrDefault(DefaultConnectionStringName);
			set => this[DefaultConnectionStringName] = value!;
		}
	}
}
