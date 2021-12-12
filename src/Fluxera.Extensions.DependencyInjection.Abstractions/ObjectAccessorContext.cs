namespace Fluxera.Extensions.DependencyInjection
{
	using JetBrains.Annotations;

	/// <summary>
	///		A context that is passed down along with a registered <see cref="IObjectAccessor{T}"/> instance.
	/// </summary>
	[PublicAPI]
	public sealed class ObjectAccessorContext
	{
		public static ObjectAccessorContext Default = new ObjectAccessorContext(null);

		public ObjectAccessorContext(string? name)
		{
			this.Name = name;
		}

		/// <summary>
		///		Gets the name of the context.
		/// </summary>
		public string? Name { get; }

		public override string ToString()
		{
			return this.Name ?? "Default";
		}
	}
}
