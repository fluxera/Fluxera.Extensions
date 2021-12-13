namespace Fluxera.Extensions.Validation
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	public sealed class ValidationBuilder
	{
		public ValidationBuilder(IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			this.Services = services;
		}

		public IServiceCollection Services { get; }

		public ValidationBuilder AddValidatorFactory<T>() where T : class, IValidatorFactory
		{
			this.Services.TryAddTransient<T>();
			this.Services.AddTransient<IValidatorFactory, T>();

			return this;
		}

		public ValidationBuilder AddValidatorFactoryNamed<T>(string name) where T : class, IValidatorFactory
		{
			this.Services.AddNamedTransient<IValidatorFactory>(builder =>
			{
				builder.AddNameFor<T>(name);
			});

			return this;
		}
	}
}
