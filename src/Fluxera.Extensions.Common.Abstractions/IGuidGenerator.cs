namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	public interface IGuidGenerator
	{
		/// <summary>
		///     Creates a new <see cref="Guid" />.
		/// </summary>
		Guid Create();
	}
}
