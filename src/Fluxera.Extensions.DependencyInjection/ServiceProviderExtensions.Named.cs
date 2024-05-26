namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Collections.Generic;
	using Microsoft.Extensions.DependencyInjection;

	public static partial class ServiceProviderExtensions
	{
		/// <summary>
		///     Gets a service of the specified type for the given name.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="serviceProvider">The service provider.</param>
		/// <param name="name">The name of the service.</param>
		/// <returns>The service instance.</returns>
		public static TService GetNamedService<TService>(this IServiceProvider serviceProvider, string name)
		{
			Guard.ThrowIfNull(serviceProvider);

			NamedServiceMapper<TService> namedServiceMapper = serviceProvider.GetRequiredService<NamedServiceMapper<TService>>();
			Type implementationType = namedServiceMapper.GetImplementationType(name);

			return implementationType is not null
				? (TService)serviceProvider.GetService(implementationType)
				: default;
		}

		/// <summary>
		///     Gets all services of the specified type for the given name.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="serviceProvider">The service provider.</param>
		/// <param name="name">The name of the service.</param>
		/// <returns>The service instance.</returns>
		public static IEnumerable<TService> GetNamedServices<TService>(this IServiceProvider serviceProvider, string name)
		{
			Guard.ThrowIfNull(serviceProvider);

			NamedServiceMapper<TService> namedServiceMapper = serviceProvider.GetRequiredService<NamedServiceMapper<TService>>();
			IEnumerable<Type> implementationTypes = namedServiceMapper.GetImplementationTypes(name);
			foreach(Type implementationType in implementationTypes)
			{
				yield return (TService)serviceProvider.GetService(implementationType);
			}
		}

		/// <summary>
		///     Gets a service of the specified type for the given name.
		/// </summary>
		/// <typeparam name="TService">The service type.</typeparam>
		/// <param name="serviceProvider">The service provider.</param>
		/// <param name="name">The name of the service.</param>
		/// <returns>The service instance.</returns>
		/// <exception cref="InvalidOperationException">
		///     Thrown when there is no service of type <typeparamref name="TService" />
		///     with the given name.
		/// </exception>
		public static TService GetRequiredNamedService<TService>(this IServiceProvider serviceProvider, string name)
			where TService : notnull
		{
			Guard.ThrowIfNull(serviceProvider);

			NamedServiceMapper<TService> namedServiceMapper = serviceProvider.GetRequiredService<NamedServiceMapper<TService>>();
			Type implementationType = namedServiceMapper.GetImplementationType(name);

			if(implementationType is null)
			{
				throw new InvalidOperationException($"No named service implementation registered for name '{name}'.");
			}

			return (TService)serviceProvider.GetRequiredService(implementationType);
		}
	}
}
