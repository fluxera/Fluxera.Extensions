namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using Fluxera.Guards;
	using Microsoft.Extensions.DependencyInjection;

	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		///     Decorate a service.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection</param>
		/// <returns>The builder.</returns>
		public static DecoratorBuilder<TService> Decorate<TService>(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			return new DecoratorBuilder<TService>(services);
		}

		/// <summary>
		///     Try to decorate a service.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="services">The service collection</param>
		/// <returns>The builder.</returns>
		public static TryDecoratorBuilder<TService> TryDecorate<TService>(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			return new TryDecoratorBuilder<TService>(services);
		}

		/// <summary>
		///     Decorate a service.
		/// </summary>
		/// <param name="services">The service collection</param>
		/// <param name="serviceType">The service type.</param>
		/// <returns>The builder.</returns>
		public static DecoratorBuilder Decorate(this IServiceCollection services, Type serviceType)
		{
			Guard.Against.Null(services, nameof(services));

			return new DecoratorBuilder(services, serviceType);
		}

		/// <summary>
		///     Try to decorate a service.
		/// </summary>
		/// <param name="services">The service collection</param>
		/// <param name="serviceType">The service type.</param>
		/// <returns>The builder.</returns>
		public static TryDecoratorBuilder TryDecorate(this IServiceCollection services, Type serviceType)
		{
			Guard.Against.Null(services, nameof(services));

			return new TryDecoratorBuilder(services, serviceType);
		}

		internal static IServiceCollection DecorateOpenGeneric(this IServiceCollection services, Type serviceType, Type decoratorType)
		{
			if(services.TryDecorateOpenGeneric(serviceType, decoratorType, out Exception exception))
			{
				return services;
			}

			throw exception;
		}

		internal static bool TryDecorateOpenGeneric(this IServiceCollection services, Type serviceType, Type decoratorType, [NotNullWhen(false)] out Exception error)
		{
			IList<Type> closedGenericServiceTypes = services
				.Where(x => !x.ServiceType.IsGenericTypeDefinition)
				.Where(x => HasSameTypeDefinition(x.ServiceType, serviceType))
				.Select(x => x.ServiceType)
				.Distinct()
				.ToList();

			if(closedGenericServiceTypes.Count == 0)
			{
				error = new InvalidOperationException($"Could not find any registered services for type '{serviceType.Name}'.");
				return false;
			}

			foreach(Type closedGenericServiceType in closedGenericServiceTypes)
			{
				Type[] arguments = closedGenericServiceType.GenericTypeArguments;

				Type closedServiceType = serviceType.MakeGenericType(arguments);
				try
				{
					Type closedDecoratorType = decoratorType.MakeGenericType(arguments);
					if(!services.TryDecorateDescriptors(closedServiceType, out error, x => x.Decorate(closedDecoratorType)))
					{
						return false;
					}
				}
				catch(ArgumentException)
				{
					// Intentionally left blank.
				}
			}

			error = default;
			return true;
		}

		internal static IServiceCollection DecorateDescriptors(this IServiceCollection services, Type serviceType, Func<ServiceDescriptor, ServiceDescriptor> decorator)
		{
			if(services.TryDecorateDescriptors(serviceType, out Exception error, decorator))
			{
				return services;
			}

			throw error;
		}

		internal static bool TryDecorateDescriptors(this IServiceCollection services, Type serviceType, [NotNullWhen(false)] out Exception exception, Func<ServiceDescriptor, ServiceDescriptor> decorator)
		{
			if(!services.TryGetDescriptors(serviceType, out ICollection<ServiceDescriptor> descriptors))
			{
				exception = new InvalidOperationException($"Could not find any registrations of service type '{serviceType.Name}'.");
				return false;
			}

			foreach(ServiceDescriptor descriptor in descriptors)
			{
				int index = services.IndexOf(descriptor);

				// Avoid reordering of the descriptors, in case there is a specific order expected.
				services[index] = decorator(descriptor);
			}

			exception = default;
			return true;
		}

		internal static ServiceDescriptor Decorate<TService>(this ServiceDescriptor descriptor, Func<TService, IServiceProvider, TService> decorator)
		{
			return descriptor.WithFactory(provider =>
			{
				TService instance = (TService)provider.GetInstance(descriptor);
				return decorator(instance, provider);
			});
		}

		internal static ServiceDescriptor Decorate<TService>(this ServiceDescriptor descriptor, Func<TService, TService> decorator)
		{
			return descriptor.WithFactory(provider =>
			{
				TService instance = (TService)provider.GetInstance(descriptor);
				return decorator(instance);
			});
		}

		internal static ServiceDescriptor Decorate(this ServiceDescriptor descriptor, Type decoratorType)
		{
			return descriptor.WithFactory(provider =>
			{
				object instance = provider.GetInstance(descriptor);
				return provider.CreateInstance(decoratorType, instance);
			});
		}

		private static bool HasSameTypeDefinition(Type firstType, Type secondType)
		{
			return firstType.IsGenericType && secondType.IsGenericType && firstType.GetGenericTypeDefinition() == secondType.GetGenericTypeDefinition();
		}

		private static bool TryGetDescriptors(this IServiceCollection services, Type serviceType, out ICollection<ServiceDescriptor> descriptors)
		{
			descriptors = services
				.Where(service => service.ServiceType == serviceType)
				.ToArray();

			return descriptors.Any();
		}

		private static ServiceDescriptor WithFactory(this ServiceDescriptor descriptor, Func<IServiceProvider, object> factory)
		{
			return ServiceDescriptor.Describe(descriptor.ServiceType, factory, descriptor.Lifetime);
		}

		private static object GetInstance(this IServiceProvider provider, ServiceDescriptor descriptor)
		{
			if(descriptor.ImplementationInstance != null)
			{
				return descriptor.ImplementationInstance;
			}

			return descriptor.ImplementationType != null
				? provider.GetServiceOrCreateInstance(descriptor.ImplementationType)
				: descriptor.ImplementationFactory?.Invoke(provider);
		}

		private static object GetServiceOrCreateInstance(this IServiceProvider provider, Type type)
		{
			return ActivatorUtilities.GetServiceOrCreateInstance(provider, type);
		}

		private static object CreateInstance(this IServiceProvider provider, Type type, params object[] arguments)
		{
			return ActivatorUtilities.CreateInstance(provider, type, arguments);
		}
	}
}
