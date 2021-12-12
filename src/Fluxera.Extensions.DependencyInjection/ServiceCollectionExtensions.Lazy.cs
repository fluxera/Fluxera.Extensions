namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using Fluxera.Guards;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	public static partial class ServiceCollectionExtensions
	{
		/// <summary>
		///		Add a <see cref="Lazy{T}"/> implementation for service discovery.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddLazy(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			services.TryAddTransient(typeof(Lazy<>), typeof(LazyService<>));

			return services;
		}
	}
}
