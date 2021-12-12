namespace Fluxera.Extensions.Http
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IBaseAddressProvider
	{
		Uri BaseAddress { get; }
	}
}
