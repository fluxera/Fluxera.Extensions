namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using Fluxera.Guards;

	/// <summary>
	///     Extensions for the <see cref="IServiceProvider" /> type.
	/// </summary>
	public static partial class ServiceProviderExtensions
	{
		/// <summary>
		///     Checks if the <see cref="IServiceProvider" /> has a registration for the given service type.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="serviceProvider">The service provider.</param>
		/// <param name="result">Outputs the service instance if is is registered.</param>
		/// <returns>True, if the service was registered; false otherwise.</returns>
		public static bool IsRegistered<T>(this IServiceProvider serviceProvider, out T result) where T : class
		{
			Guard.Against.Null(serviceProvider);

			bool isRegistered = serviceProvider.IsRegistered(typeof(T), out object instance);
			result = (T)instance;
			return isRegistered;
		}

		/// <summary>
		///     Checks if the <see cref="IServiceProvider" /> has a registration for the given service type.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="serviceProvider">The service provider.</param>
		/// <returns>True, if the service was registered; false otherwise.</returns>
		public static bool IsRegistered<T>(this IServiceProvider serviceProvider) where T : class
		{
			Guard.Against.Null(serviceProvider);

			bool isRegistered = serviceProvider.IsRegistered(typeof(T), out _);
			return isRegistered;
		}

		/// <summary>
		///     Checks if the <see cref="IServiceProvider" /> has a registration for the given service type.
		/// </summary>
		/// <param name="serviceProvider">The service provider.</param>
		/// <param name="serviceType">The service type.</param>
		/// <param name="result">Outputs the service instance if is is registered.</param>
		/// <returns>True, if the service was registered; false otherwise.</returns>
		public static bool IsRegistered(this IServiceProvider serviceProvider, Type serviceType, out object result)
		{
			Guard.Against.Null(serviceProvider);

			result = serviceProvider.GetService(serviceType);
			return result != null;
		}

		/// <summary>
		///     Checks if the <see cref="IServiceProvider" /> has a registration for the given service type.
		/// </summary>
		/// <param name="serviceProvider">The service provider.</param>
		/// <param name="serviceType">The service type.</param>
		/// <returns>True, if the service was registered; false otherwise.</returns>
		public static bool IsRegistered(this IServiceProvider serviceProvider, Type serviceType)
		{
			Guard.Against.Null(serviceProvider);

			object result = serviceProvider.GetService(serviceType);
			return result != null;
		}
	}
}
