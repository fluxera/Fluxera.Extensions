namespace Fluxera.Extensions.Common
{
	using System;

	/// <summary>
	///     Implements <see cref="IGuidGenerator" /> by using <see cref="Guid.NewGuid" />.
	/// </summary>
	internal sealed class SimpleGuidGenerator : IGuidGenerator
	{
		public Guid Create()
		{
			return Guid.NewGuid();
		}
	}
}
