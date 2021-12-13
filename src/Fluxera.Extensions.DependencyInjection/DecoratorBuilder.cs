namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Reflection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public sealed class DecoratorBuilder
	{
		private readonly IServiceCollection services;
		private readonly Type serviceType;

		internal DecoratorBuilder(IServiceCollection services, Type serviceType)
		{
			Guard.Against.Null(services, nameof(services));
			Guard.Against.Null(serviceType, nameof(serviceType));

			this.services = services;
			this.serviceType = serviceType;
		}

		/// <summary>
		///		Decorates the <see cref="serviceType"/> with the given decorator type.
		/// </summary>
		/// <param name="decoratorType">The decorator type.</param>
		/// <returns>The builder.</returns>
		public DecoratorBuilder With(Type decoratorType)
		{
			Guard.Against.Null(decoratorType, nameof(decoratorType));

			if(this.serviceType.GetTypeInfo().IsGenericTypeDefinition && decoratorType.GetTypeInfo().IsGenericTypeDefinition)
			{
				this.services.DecorateOpenGeneric(this.serviceType, decoratorType);
			}

			this.services.DecorateDescriptors(this.serviceType, descriptor => descriptor.Decorate(decoratorType));

			return this;
		}

		/// <summary>
		///		Decorates the <see cref="serviceType"/> with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>The builder.</returns>
		public DecoratorBuilder With(Func<object, IServiceProvider, object> decorator)
		{
			Guard.Against.Null(decorator, nameof(decorator));

			this.services.DecorateDescriptors(this.serviceType, descriptor => descriptor.Decorate(decorator));

			return this;
		}

		/// <summary>
		///		Decorates the <see cref="serviceType"/> with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>The builder.</returns>
		public DecoratorBuilder Wit(Func<object, object> decorator)
		{
			Guard.Against.Null(decorator, nameof(decorator));

			this.services.DecorateDescriptors(this.serviceType, descriptor => descriptor.Decorate(decorator));

			return this;
		}
	}

	[PublicAPI]
	public sealed class DecoratorBuilder<TService>
	{
		private readonly IServiceCollection services;

		internal DecoratorBuilder(IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			this.services = services;
		}

		/// <summary>
		///		Decorates the <see cref="TService"/> type with the given <see cref="TDecorator"/> type.
		/// </summary>
		/// <typeparam name="TDecorator">The decorator type</typeparam>
		/// <returns>The builder.</returns>
		public DecoratorBuilder<TService> With<TDecorator>() where TDecorator : TService
		{
			this.services.DecorateDescriptors(typeof(TService), descriptor => descriptor.Decorate(typeof(TDecorator)));

			return this;
		}

		/// <summary>
		///		Decorates the <see cref="TService"/> type with the given decorator function.
		/// </summary>
		/// <returns>The builder.</returns>
		public DecoratorBuilder<TService> With(Func<TService, IServiceProvider, TService> decorator)
		{
			Guard.Against.Null(decorator, nameof(decorator));

			this.services.DecorateDescriptors(typeof(TService), descriptor => descriptor.Decorate(decorator));

			return this;
		}

		/// <summary>
		///		Decorates the <see cref="TService"/> type with the given decorator function.
		/// </summary>
		/// <returns>The builder.</returns>
		public DecoratorBuilder<TService> With(Func<TService, TService> decorator)
		{
			this.services.DecorateDescriptors(typeof(TService), descriptor => descriptor.Decorate(decorator));

			return this;
		}
	}
}
