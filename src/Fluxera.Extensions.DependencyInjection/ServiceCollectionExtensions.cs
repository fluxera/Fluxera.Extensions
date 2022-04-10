namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Linq;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	/// <summary>
	///     Extension methods for the <see cref="IServiceCollection" /> type.
	/// </summary>
	[PublicAPI]
	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		///     Checks if the <see cref="IServiceCollection" /> has a registration for the given service type.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>True, if the service was registered; false otherwise.</returns>
		public static bool IsRegistered<TService>(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			return services.IsRegistered(typeof(TService));
		}

		/// <summary>
		///     Checks if the <see cref="IServiceCollection" /> has a registration for the given service type.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="serviceType"></param>
		/// <returns>True, if the service was registered; false otherwise.</returns>
		public static bool IsRegistered(this IServiceCollection services, Type serviceType)
		{
			Guard.Against.Null(services, nameof(services));

			return services.Any(x => x.ServiceType == serviceType);
		}

		/// <summary>
		///     Replaces a singleton descriptor in the service collection.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <typeparam name="TImplementation">The implementing type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection ReplaceSingleton<TService, TImplementation>(this IServiceCollection services)
			where TService : class
			where TImplementation : class, TService
		{
			Guard.Against.Null(services, nameof(services));

			return services.Replace<TService, TImplementation>(ServiceLifetime.Singleton);
		}

		/// <summary>
		///     Replaces a singleton descriptor in the service collection.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="instance">The singleton instance.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection ReplaceSingleton<TService>(this IServiceCollection services, TService instance)
			where TService : class
		{
			Guard.Against.Null(services, nameof(services));

			ServiceDescriptor descriptor = ServiceDescriptor.Singleton(instance);
			return services.Replace(descriptor);
		}

		/// <summary>
		///     Replaces a transient descriptor in the service collection.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <typeparam name="TImplementation">The implementing type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection ReplaceTransient<TService, TImplementation>(this IServiceCollection services)
			where TService : class
			where TImplementation : class, TService
		{
			Guard.Against.Null(services, nameof(services));

			return services.Replace<TService, TImplementation>(ServiceLifetime.Transient);
		}

		/// <summary>
		///     Replaces a transient descriptor in the service collection.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="service">The service type.</param>
		/// <param name="implementation">The implementing type.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection ReplaceTransient(this IServiceCollection services,
			Type service, Type implementation)
		{
			Guard.Against.Null(services, nameof(services));

			return services.Replace(service, implementation, ServiceLifetime.Transient);
		}

		/// <summary>
		///     Replaces a scoped descriptor in the service collection.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <typeparam name="TImplementation">The implementing type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection ReplaceScoped<TService, TImplementation>(this IServiceCollection services)
			where TService : class
			where TImplementation : class, TService
		{
			Guard.Against.Null(services, nameof(services));

			return services.Replace<TService, TImplementation>(ServiceLifetime.Scoped);
		}

		/// <summary>
		///     Removes a singleton descriptor from the service collection.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection RemoveSingleton<TService>(this IServiceCollection services)
			where TService : class
		{
			Guard.Against.Null(services, nameof(services));

			ServiceDescriptor serviceDescriptor = services.First(s => s.ServiceType == typeof(TService));
			services.Remove(serviceDescriptor);

			return services;
		}

		/// <summary>
		///     Gets the instance of a singleton instance service.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The singleton instance or null, iof it was not available.</returns>
		public static TService GetSingletonInstanceOrDefault<TService>(this IServiceCollection services)
			where TService : class
		{
			Guard.Against.Null(services, nameof(services));

			ServiceDescriptor serviceDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(TService));
			return serviceDescriptor?.ImplementationInstance as TService;
		}

		/// <summary>
		///     Tries to get the instance of a singleton service.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="result">Outputs the singleton instance or null, iof it was not available.</param>
		/// <returns>True, if the service was found; false otherwise.</returns>
		public static bool TryGetSingletonInstance<TService>(this IServiceCollection services, out TService result) where TService : class
		{
			Guard.Against.Null(services, nameof(services));

			result = services.GetSingletonInstanceOrDefault<TService>();
			return result != null;
		}

		/// <summary>
		///     Gets the instance of a singleton service.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The singleton instance.</returns>
		/// <exception cref="InvalidOperationException">Thrown, if the singleton instance was not available.</exception>
		public static TService GetSingletonInstance<TService>(this IServiceCollection services) where TService : class
		{
			Guard.Against.Null(services, nameof(services));

			TService service = services.GetSingletonInstanceOrDefault<TService>();
			if(service == null)
			{
				throw new InvalidOperationException(
					$"Could not find a singleton instance for service {typeof(TService).Name}.");
			}

			return service;
		}

		private static IServiceCollection Replace<TService, TImplementation>(this IServiceCollection services,
			ServiceLifetime serviceLifetime)
			where TService : class
			where TImplementation : class, TService
		{
			return services.Replace(typeof(TService), typeof(TImplementation), serviceLifetime);
		}

		private static IServiceCollection Replace(this IServiceCollection services, Type service, Type implementation,
			ServiceLifetime serviceLifetime)
		{
			ServiceDescriptor descriptor = new ServiceDescriptor(
				service,
				implementation,
				serviceLifetime);
			services.Replace(descriptor);

			return services;
		}
	}
}
