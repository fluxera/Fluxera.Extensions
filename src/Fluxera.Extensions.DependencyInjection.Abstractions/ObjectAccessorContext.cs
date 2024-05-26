namespace Fluxera.Extensions.DependencyInjection
{
	using JetBrains.Annotations;
	using System;

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
			if(string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException("Value cannot be whitespace-only.", nameof(name));
			}

			this.Name = name;
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
