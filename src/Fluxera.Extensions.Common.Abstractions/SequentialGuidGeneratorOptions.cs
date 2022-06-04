namespace Fluxera.Extensions.Common
{
	using JetBrains.Annotations;

	/// <summary>
	///     The options for the sequential guid generator.
	/// </summary>
	[PublicAPI]
	public sealed class SequentialGuidGeneratorOptions
	{
		/// <summary>
		///     Initializes a new instance of the <see cref="SequentialGuidGeneratorOptions" /> type.
		/// </summary>
		public SequentialGuidGeneratorOptions()
		{
			this.DefaultSequentialGuidType = SequentialGuidType.SequentialAtEnd;
		}

		/// <summary>
		///     Gets or sets the sequence type. Default value: <see cref="SequentialGuidType.SequentialAtEnd" />.
		/// </summary>
		public SequentialGuidType DefaultSequentialGuidType { get; set; }
	}
}
