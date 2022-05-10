namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Guards;
	using Microsoft.Extensions.DependencyInjection;

	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		///     Adds an object accessor using the default context.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		/// <exception cref="InvalidOperationException">Thrown when an object accessor was already registered for the service type.</exception>
		public static IServiceCollection AddObjectAccessor<T>(this IServiceCollection services)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.AddObjectAccessorInstance(new ObjectAccessor<T>());
		}

		/// <summary>
		///     Adds an object accessor using the given context.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="context">The context.</param>
		/// <returns>The service collection.</returns>
		/// <exception cref="InvalidOperationException">Thrown when an object accessor was already registered for the service type.</exception>
		public static IServiceCollection AddObjectAccessor<T>(this IServiceCollection services, ObjectAccessorContext context)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.AddObjectAccessorInstance(new ObjectAccessor<T>(context));
		}

		/// <summary>
		///     Adds an object accessor using the default context and given object instance.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="value">The instance of the object accessor.</param>
		/// <returns>The service collection.</returns>
		/// <exception cref="InvalidOperationException">Thrown when an object accessor was already registered for the service type.</exception>
		public static IServiceCollection AddObjectAccessor<T>(this IServiceCollection services, T value)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.AddObjectAccessorInstance(new ObjectAccessor<T>(value));
		}

		/// <summary>
		///     Adds an object accessor using the given context and object instance.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="value">The instance of the object accessor.</param>
		/// <param name="context">The context.</param>
		/// <returns>The service collection.</returns>
		/// <exception cref="InvalidOperationException">Thrown when an object accessor was already registered for the service type.</exception>
		public static IServiceCollection AddObjectAccessor<T>(this IServiceCollection services, T value, ObjectAccessorContext context)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.AddObjectAccessorInstance(new ObjectAccessor<T>(value, context));
		}

		/// <summary>
		///     Tries to add an object accessor using the default context.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>True, if the object accessor was added; false otherwise.</returns>
		public static bool TryAddObjectAccessor<T>(this IServiceCollection services)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.TryAddObjectAccessorInstance(new ObjectAccessor<T>());
		}

		/// <summary>
		///     Tries to add an object accessor using the given context.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="context">The context.</param>
		/// <returns>True, if the object accessor was added; false otherwise.</returns>
		public static bool TryAddObjectAccessor<T>(this IServiceCollection services, ObjectAccessorContext context)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.TryAddObjectAccessorInstance(new ObjectAccessor<T>(context));
		}

		/// <summary>
		///     Tries to add an object accessor using the default context and given object instance.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="value">The instance of the object accessor.</param>
		/// <returns>True, if the object accessor was added; false otherwise.</returns>
		public static bool TryAddObjectAccessor<T>(this IServiceCollection services, T value)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.TryAddObjectAccessorInstance(new ObjectAccessor<T>(value));
		}

		/// <summary>
		///     Tries to add an object accessor using the given context and object instance.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="value">The instance of the object accessor.</param>
		/// <param name="context">The context.</param>
		/// <returns>True, if the object accessor was added; false otherwise.</returns>
		public static bool TryAddObjectAccessor<T>(this IServiceCollection services, T value, ObjectAccessorContext context)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.TryAddObjectAccessorInstance(new ObjectAccessor<T>(value, context));
		}

		/// <summary>
		///     Gets a registered object accessor instance from the service collection.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The object accessor instance.</returns>
		/// <exception cref="InvalidOperationException">Thrown when an object accessor was not found for the service type.</exception>
		public static IObjectAccessor<T> GetObjectAccessor<T>(this IServiceCollection services)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			IObjectAccessor<T> accessor = services.GetSingletonInstance<IObjectAccessor<T>>();
			return accessor;
		}

		/// <summary>
		///     Tries to get a registered object accessor instance from the service collection.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <param name="objectAccessor">Outputs the object accessor instance.</param>
		/// <returns>True, if the object accessor was found; false otherwise.</returns>
		public static bool TryGetObjectAccessor<T>(this IServiceCollection services, out IObjectAccessor<T> objectAccessor)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.TryGetSingletonInstance(out objectAccessor);
		}

		/// <summary>
		///     Gets the singleton instance of an object accessor.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The service instance.</returns>
		/// <exception cref="InvalidOperationException">Thrown when an object accessor was not found.</exception>
		public static T GetObjectOrDefault<T>(this IServiceCollection services)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			services.TryGetObjectAccessor(out IObjectAccessor<T> objectAccessor);
			return objectAccessor?.Value;
		}

		/// <summary>
		///     Gets the singleton instance of an object accessor.
		/// </summary>
		/// <typeparam name="T">The service type.</typeparam>
		/// <param name="services">The service collection.</param>
		/// <returns>The service instance.</returns>
		/// <exception cref="InvalidOperationException">Thrown when an instance was not found.</exception>
		public static T GetObject<T>(this IServiceCollection services)
			where T : class
		{
			Guard.Against.Null(services, nameof(services));

			return services.GetObjectOrDefault<T>()
				?? throw new InvalidOperationException($"Could not find an instance of type {typeof(T).Name}. Be sure that you have used AddObjectAccessor before.");
		}

		/// <summary>
		///     Gets all available object accessors.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The object accessors.</returns>
		public static IEnumerable<IObjectAccessor> GetObjectAccessors(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			IEnumerable<ServiceDescriptor> descriptors = services.Where(d => d.ServiceType == typeof(IObjectAccessor));
			foreach(ServiceDescriptor descriptor in descriptors)
			{
				if(descriptor.ImplementationInstance != null)
				{
					yield return (IObjectAccessor)descriptor.ImplementationInstance;
				}
			}
		}

		private static IServiceCollection AddObjectAccessorInstance<T>(this IServiceCollection services, IObjectAccessor<T> accessor)
			where T : class
		{
			if(services.Any(s => s.ServiceType == typeof(IObjectAccessor<T>)))
			{
				throw new InvalidOperationException($"An object accessor is already registered for type {typeof(T).Name}");
			}

			// Add to the beginning for fast retrieve.
			services.Insert(0, ServiceDescriptor.Singleton(typeof(IObjectAccessor<T>), accessor));
			services.Add(ServiceDescriptor.Singleton(typeof(IObjectAccessor), accessor));

			return services;
		}

		private static bool TryAddObjectAccessorInstance<T>(this IServiceCollection services, IObjectAccessor<T> accessor)
			where T : class
		{
			if(services.Any(s => s.ServiceType == typeof(IObjectAccessor<T>)))
			{
				return false;
			}

			// Add to the beginning for fast retrieve.
			services.Insert(0, ServiceDescriptor.Singleton(typeof(IObjectAccessor<T>), accessor));
			services.Add(ServiceDescriptor.Singleton(typeof(IObjectAccessor), accessor));
			return true;
		}
	}
}
