namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A contract for an object accessor that stores the instances in a <see cref="IServiceCollection" />
	///     allowing to access singleton instances registered in it while still configuring the services.
	/// </summary>
	[PublicAPI]
	public interface IObjectAccessor : IDisposable
	{
		/// <summary>
		///     Gets the context of this object accessor.
		/// </summary>
		ObjectAccessorContext Context { get; }

		/// <summary>
		///     The stored instance of this object accessor.
		/// </summary>
		object Value { set; }
	}

	/// <summary>
	///     A contract for an object accessor that stores the instances in a <see cref="IServiceCollection" />
	///     allowing to access singleton instances registered in it while still configuring the services.
	/// </summary>
	/// <typeparam name="T">The service type.</typeparam>
	[PublicAPI]
	public interface IObjectAccessor<T> : IObjectAccessor where T : class
	{
		/// <summary>
		///     The stored instance of this object accessor.
		/// </summary>
		new T Value { get; set; }
	}
}
