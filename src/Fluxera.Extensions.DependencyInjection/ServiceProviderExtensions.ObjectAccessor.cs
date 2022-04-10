namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Collections.Generic;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public static partial class ServiceProviderExtensions
	{
		/// <summary>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public static T GetObject<T>(this IServiceProvider serviceProvider) where T : class
		{
			Guard.Against.Null(serviceProvider, nameof(serviceProvider));

			return serviceProvider.GetObjectOrDefault<T>()
				?? throw new InvalidOperationException($"Could not find an object of {typeof(T).AssemblyQualifiedName} in services. Be sure that you have used AddObjectAccessor before.");
		}

		/// <summary>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		public static T GetObjectOrDefault<T>(this IServiceProvider serviceProvider) where T : class
		{
			Guard.Against.Null(serviceProvider, nameof(serviceProvider));

			IObjectAccessor<T> objectAccessor = serviceProvider.GetRequiredService<IObjectAccessor<T>>();
			return objectAccessor.Value ?? null;
		}

		/// <summary>
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		public static IEnumerable<IObjectAccessor> GetObjectAccessors(this IServiceProvider serviceProvider)
		{
			Guard.Against.Null(serviceProvider, nameof(serviceProvider));

			return serviceProvider.GetServices<IObjectAccessor>();
		}
	}
}
