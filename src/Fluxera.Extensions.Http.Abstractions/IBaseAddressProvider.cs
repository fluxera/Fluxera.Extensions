namespace Fluxera.Extensions.Http
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A contract for a service that provides the base URL for HTTP clients.
	/// </summary>
	[PublicAPI]
	public interface IBaseAddressProvider
	{
		/// <summary>
		///     The base address.
		/// </summary>
		Uri BaseAddress { get; }
	}
}
