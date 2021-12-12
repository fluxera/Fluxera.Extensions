namespace Fluxera.Extensions.Common
{
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class SequentialGuidGeneratorOptions
	{
		public SequentialGuidGeneratorOptions()
		{
			DefaultSequentialGuidType = SequentialGuidType.SequentialAtEnd;
		}

		/// <summary>
		///     Default value: <see cref="SequentialGuidType.SequentialAtEnd" />.
		/// </summary>
		public SequentialGuidType DefaultSequentialGuidType { get; set; }
	}
}
