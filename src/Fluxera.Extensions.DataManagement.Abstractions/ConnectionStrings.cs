namespace Fluxera.Extensions.DataManagement
{
	using System;
	using System.Collections.Generic;
	using JetBrains.Annotations;

	/// <summary>
	///     The connection strings options.
	/// </summary>
	[PublicAPI]
	[Serializable]
	public sealed class ConnectionStrings : Dictionary<string, string>
	{
		/// <summary>
		///     The name of the default connection string.
		/// </summary>
		public const string DefaultConnectionStringName = "Default";

		/// <summary>
		///     Gets or sets the default connection string.
		/// </summary>
		public string Default
		{
			get => this.GetValueOrDefault(DefaultConnectionStringName);
			set => this[DefaultConnectionStringName] = value;
		}
	}
}
