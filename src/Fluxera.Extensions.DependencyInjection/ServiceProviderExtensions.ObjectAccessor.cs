namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Collections.Generic;
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
			Guard.ThrowIfNull(serviceProvider);

			return serviceProvider.GetObjectOrDefault<T>()
				?? throw new InvalidOperationException($"Could not find an object of {typeof(T)} in services. Be sure that you have used AddObjectAccessor before.");
		}

		/// <summary>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		public static T GetObjectOrDefault<T>(this IServiceProvider serviceProvider) where T : class
		{
			Guard.ThrowIfNull(serviceProvider);

			IObjectAccessor<T> objectAccessor = serviceProvider.GetService<IObjectAccessor<T>>();
			return objectAccessor?.Value;
		}

		/// <summary>
		/// </summary>
		/// <param name="serviceProvider"></param>
		/// <returns></returns>
		public static IEnumerable<IObjectAccessor> GetObjectAccessors(this IServiceProvider serviceProvider)
		{
			Guard.ThrowIfNull(serviceProvider);

			return serviceProvider.GetServices<IObjectAccessor>();
		}
	}
}
