namespace Fluxera.Extensions.Validation
{
	using Fluxera.Extensions.DependencyInjection;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	internal sealed class ValidationBuilder : IValidationBuilder
	{
		public ValidationBuilder(IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			this.Services = services;
		}

		public IValidationBuilder AddValidatorFactory<T>() where T : class, IValidatorFactory
		{
			this.Services.TryAddTransient<T>();
			this.Services.AddTransient<IValidatorFactory, T>();

			return this;
		}

		public IValidationBuilder AddValidatorFactoryNamed<T>(string name) where T : class, IValidatorFactory
		{
			this.Services.AddNamedTransient<IValidatorFactory>(builder =>
			{
				builder.AddNameFor<T>(name);
			});

			return this;
		}

		/// <inheritdoc />
		public IServiceCollection Services { get; }
	}
}
