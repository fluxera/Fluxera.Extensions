namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public sealed class TryDecoratorBuilder
	{
		private readonly IServiceCollection services;
		private readonly Type serviceType;

		internal TryDecoratorBuilder(IServiceCollection services, Type serviceType)
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(serviceType, nameof(serviceType));

			this.services = services;
			this.serviceType = serviceType;
		}

		/// <summary>
		///		Try to decorate the <see cref="serviceType"/> with the given decorator type.
		/// </summary>
		/// <param name="decoratorType">The decorator type.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Type decoratorType)
		{
			Guard.Against.Null(decoratorType, nameof(decoratorType));

			if (serviceType.IsOpenGeneric() && decoratorType.IsOpenGeneric())
			{
				return this.services.TryDecorateOpenGeneric(this.serviceType, decoratorType, out _);
			}

			return this.services.TryDecorateDescriptors(this.serviceType, out _, descriptor => descriptor.Decorate(decoratorType));
		}

		/// <summary>
		///		Try to decorate the <see cref="serviceType"/> with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Func<object, IServiceProvider, object> decorator)
		{
			Guard.Against.Null(decorator, nameof(decorator));

			return this.services.TryDecorateDescriptors(this.serviceType, out _, descriptor => descriptor.Decorate(decorator));
		}

		/// <summary>
		///		Try to decorate the <see cref="serviceType"/> with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Func<object, object> decorator)
		{
			Guard.Against.Null(decorator, nameof(decorator));

			return this.services.TryDecorateDescriptors(this.serviceType, out _, descriptor => descriptor.Decorate(decorator));
		}
	}

	[PublicAPI]
	public sealed class TryDecoratorBuilder<TService>
	{
		private readonly IServiceCollection services;

		internal TryDecoratorBuilder(IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			this.services = services;
		}

		/// <summary>
		///		Try to decorate the <see cref="TService"/> type with the given <see cref="TDecorator"/> type.
		/// </summary>
		/// <typeparam name="TDecorator">The decorator type</typeparam>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With<TDecorator>() where TDecorator : TService
		{
			return this.services.TryDecorateDescriptors(typeof(TService), out _, descriptor => descriptor.Decorate(typeof(TDecorator)));
		}

		/// <summary>
		///		Try to decorate the <see cref="TService"/> type with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Func<TService, IServiceProvider, TService> decorator)
		{
			Guard.Against.Null(decorator, nameof(decorator));

			return this.services.TryDecorateDescriptors(typeof(TService), out _, descriptor => descriptor.Decorate(decorator));
		}

		/// <summary>
		///		Try to decorate the <see cref="TService"/> type with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Func<TService, TService> decorator)
		{
			return this.services.TryDecorateDescriptors(typeof(TService), out _, descriptor => descriptor.Decorate(decorator));
		}
	}
}
