namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A builder to help configure decorators.
	/// </summary>
	[PublicAPI]
	public sealed class DecoratorBuilder
	{
		private readonly IServiceCollection services;
		private readonly Type serviceType;

		internal DecoratorBuilder(IServiceCollection services, Type serviceType)
		{
			Guard.ThrowIfNull(services);
			Guard.ThrowIfNull(serviceType);

			this.services = services;
			this.serviceType = serviceType;
		}

		/// <summary>
		///     Decorates the <see cref="serviceType" /> with the given decorator type.
		/// </summary>
		/// <param name="decoratorType">The decorator type.</param>
		/// <returns>The builder.</returns>
		public DecoratorBuilder With(Type decoratorType)
		{
			Guard.ThrowIfNull(decoratorType);

			if(this.serviceType.IsOpenGeneric() && decoratorType.IsOpenGeneric())
			{
				this.services.DecorateOpenGeneric(this.serviceType, decoratorType);
				return this;
			}

			this.services.DecorateDescriptors(this.serviceType, descriptor => descriptor.Decorate(decoratorType));
			return this;
		}

		/// <summary>
		///     Decorates the <see cref="serviceType" /> with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>The builder.</returns>
		public DecoratorBuilder With(Func<object, IServiceProvider, object> decorator)
		{
			Guard.ThrowIfNull(decorator);

			this.services.DecorateDescriptors(this.serviceType, descriptor => descriptor.Decorate(decorator));
			return this;
		}

		/// <summary>
		///     Decorates the <see cref="serviceType" /> with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>The builder.</returns>
		public DecoratorBuilder With(Func<object, object> decorator)
		{
			Guard.ThrowIfNull(decorator);

			this.services.DecorateDescriptors(this.serviceType, descriptor => descriptor.Decorate(decorator));
			return this;
		}
	}

	/// <summary>
	///     A builder to help configure decorators.
	/// </summary>
	[PublicAPI]
	public sealed class DecoratorBuilder<TService>
	{
		private readonly IServiceCollection services;

		internal DecoratorBuilder(IServiceCollection services)
		{
			Guard.ThrowIfNull(services);

			this.services = services;
		}

		/// <summary>
		///     Decorates the <typeparamref name="TService" /> type with the given <typeparamref name="TDecorator" /> type.
		/// </summary>
		/// <typeparam name="TDecorator">The decorator type</typeparam>
		/// <returns>The builder.</returns>
		public DecoratorBuilder<TService> With<TDecorator>() where TDecorator : TService
		{
			this.services.DecorateDescriptors(typeof(TService), descriptor => descriptor.Decorate(typeof(TDecorator)));
			return this;
		}

		/// <summary>
		///     Decorates the <typeparamref name="TService" /> type with the given decorator function.
		/// </summary>
		/// <returns>The builder.</returns>
		public DecoratorBuilder<TService> With(Func<TService, IServiceProvider, TService> decorator)
		{
			Guard.ThrowIfNull(decorator);

			this.services.DecorateDescriptors(typeof(TService), descriptor => descriptor.Decorate(decorator));
			return this;
		}

		/// <summary>
		///     Decorates the <typeparamref name="TService" /> type with the given decorator function.
		/// </summary>
		/// <returns>The builder.</returns>
		public DecoratorBuilder<TService> With(Func<TService, TService> decorator)
		{
			Guard.ThrowIfNull(decorator);

			this.services.DecorateDescriptors(typeof(TService), descriptor => descriptor.Decorate(decorator));
			return this;
		}
	}
}
