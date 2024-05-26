namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A safe decorator builder.
	/// </summary>
	[PublicAPI]
	public sealed class TryDecoratorBuilder
	{
		private readonly IServiceCollection services;
		private readonly Type serviceType;

		internal TryDecoratorBuilder(IServiceCollection services, Type serviceType)
		{
			Guard.ThrowIfNull(services);
			Guard.ThrowIfNull(serviceType);

			this.services = services;
			this.serviceType = serviceType;
		}

		/// <summary>
		///     Try to decorate the <see cref="serviceType" /> with the given decorator type.
		/// </summary>
		/// <param name="decoratorType">The decorator type.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Type decoratorType)
		{
			Guard.ThrowIfNull(decoratorType);

			if(this.serviceType.IsOpenGeneric() && decoratorType.IsOpenGeneric())
			{
				return this.services.TryDecorateOpenGeneric(this.serviceType, decoratorType, out _);
			}

			return this.services.TryDecorateDescriptors(this.serviceType, out _, descriptor => descriptor.Decorate(decoratorType));
		}

		/// <summary>
		///     Try to decorate the <see cref="serviceType" /> with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Func<object, IServiceProvider, object> decorator)
		{
			Guard.ThrowIfNull(decorator);

			return this.services.TryDecorateDescriptors(this.serviceType, out _, descriptor => descriptor.Decorate(decorator));
		}

		/// <summary>
		///     Try to decorate the <see cref="serviceType" /> with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Func<object, object> decorator)
		{
			Guard.ThrowIfNull(decorator);

			return this.services.TryDecorateDescriptors(this.serviceType, out _, descriptor => descriptor.Decorate(decorator));
		}
	}

	/// <summary>
	///     A safe decorator builder.
	/// </summary>
	/// <typeparam name="TService"></typeparam>
	[PublicAPI]
	public sealed class TryDecoratorBuilder<TService>
	{
		private readonly IServiceCollection services;

		internal TryDecoratorBuilder(IServiceCollection services)
		{
			Guard.ThrowIfNull(services);

			this.services = services;
		}

		/// <summary>
		///     Try to decorate the <typeparamref name="TService" /> type with the given <typeparamref name="TDecorator" /> type.
		/// </summary>
		/// <typeparam name="TDecorator">The decorator type</typeparam>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With<TDecorator>() where TDecorator : TService
		{
			return this.services.TryDecorateDescriptors(typeof(TService), out _, descriptor => descriptor.Decorate(typeof(TDecorator)));
		}

		/// <summary>
		///     Try to decorate the <typeparamref name="TService" /> type with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Func<TService, IServiceProvider, TService> decorator)
		{
			Guard.ThrowIfNull(decorator);

			return this.services.TryDecorateDescriptors(typeof(TService), out _, descriptor => descriptor.Decorate(decorator));
		}

		/// <summary>
		///     Try to decorate the <typeparamref name="TService" /> type with the given decorator function.
		/// </summary>
		/// <param name="decorator">The decorator function.</param>
		/// <returns>True, if the decorator was added; false otherwise.</returns>
		public bool With(Func<TService, TService> decorator)
		{
			return this.services.TryDecorateDescriptors(typeof(TService), out _, descriptor => descriptor.Decorate(decorator));
		}
	}
}
