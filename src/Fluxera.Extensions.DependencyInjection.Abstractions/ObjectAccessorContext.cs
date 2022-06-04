namespace Fluxera.Extensions.DependencyInjection
{
	using Fluxera.Guards;
	using JetBrains.Annotations;

	/// <summary>
	///     A context that is passed down along with a registered <see cref="IObjectAccessor{T}" /> instance.
	/// </summary>
	[PublicAPI]
	public sealed class ObjectAccessorContext
	{
		/// <summary>
		///     The default context.
		/// </summary>
		public static ObjectAccessorContext Default = new ObjectAccessorContext("Default");

		/// <summary>
		///     Initializes a new instance of the <see cref="ObjectAccessorContext" /> type.
		/// </summary>
		/// <param name="name"></param>
		public ObjectAccessorContext(string name)
		{
			this.Name = Guard.Against.NullOrWhiteSpace(name);
		}

		/// <summary>
		///     Gets the name of the context.
		/// </summary>
		public string Name { get; }

		/// <inheritdoc />
		public override string ToString()
		{
			return this.Name;
		}
	}
}
