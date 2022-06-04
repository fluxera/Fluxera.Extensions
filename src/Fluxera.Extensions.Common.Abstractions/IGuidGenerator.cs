namespace Fluxera.Extensions.Common
{
	using System;
	using JetBrains.Annotations;

	/// <summary>
	///     A context for services that generate <see cref="Guid" /> values.
	/// </summary>
	[PublicAPI]
	public interface IGuidGenerator
	{
		/// <summary>
		///     Creates a new <see cref="Guid" />.
		/// </summary>
		Guid Create();
	}
}
