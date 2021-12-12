namespace Fluxera.Extensions.Common
{
	using System;
	using Fluxera.Guards;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;

	[PublicAPI]
	public static class ServiceCollectionExtensions
	{
		/////  <summary>
		///// 	Adds the principal factory service.
		/////  </summary>
		/////  <param name="services">The service collection.</param>
		/////  <param name="configureOptions"></param>
		/////  <returns>The service collection.</returns>
		//public static IServiceCollection AddPrincipalFactory(this IServiceCollection services, Action<PrincipalFactoryOptions> configureOptions)
		//{
		//	Guard.Against.Null(services, nameof(services));

		//	// Add logging infrastructure.
		//	services.AddLogging();

		//	// Add options infrastructure.
		//	services.AddOptions();

		//	services.Configure(configureOptions);

		//	services.TryAddTransient<IPrincipalFactory, PrincipalFactory>();

		//	return services;
		//}

		/// <summary>
		///     Adds the date-time provider service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			// Add logging infrastructure.
			services.AddLogging();

			// Add options infrastructure.
			services.AddOptions();

			services.TryAddTransient<IDateTimeProvider, DateTimeProvider>();

			return services;
		}

		/// <summary>
		///     Adds the date-time provider service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddDateTimeOffsetProvider(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			// Add logging infrastructure.
			services.AddLogging();

			// Add options infrastructure.
			services.AddOptions();

			services.TryAddTransient<IDateTimeOffsetProvider, DateTimeOffsetProvider>();

			return services;
		}

		/// <summary>
		///     Adds the password generator service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddPasswordGenerator(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			// Add logging infrastructure.
			services.AddLogging();

			// Add options infrastructure.
			services.AddOptions();

			services.TryAddTransient<IPasswordGenerator, PasswordGenerator>();

			return services;
		}

		/// <summary>
		///     Adds the hash calculator service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddHashCalculator(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			// Add logging infrastructure.
			services.AddLogging();

			// Add options infrastructure.
			services.AddOptions();

			services.TryAddTransient<IHashCalculator, HashCalculator>();

			return services;
		}

		/// <summary>
		///     Adds the jitter calculator service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddJitterCalculator(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			// Add logging infrastructure.
			services.AddLogging();

			// Add options infrastructure.
			services.AddOptions();

			services.TryAddTransient<IJitterCalculator, JitterCalculator>();

			return services;
		}

		/// <summary>
		///     Adds the retry delay calculator service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddRetryDelayCalculator(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			services.AddJitterCalculator();

			services.TryAddTransient<IRetryDelayCalculator, RetryDelayCalculator>();

			return services;
		}


		/// <summary>
		///     Adds the guid generator service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddGuidGenerator(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			return services.AddGuidGenerator(null);
		}

		/// <summary>
		///     Adds the guid generator service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="configureOptions">The optional options for the sequential guid generator.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddGuidGenerator(this IServiceCollection services,
			Action<SequentialGuidGeneratorOptions>? configureOptions)
		{
			Guard.Against.Null(services, nameof(services));

			// Add logging infrastructure.
			services.AddLogging();

			// Add options infrastructure.
			services.AddOptions();

			if (configureOptions is null)
			{
				services.TryAddTransient<IGuidGenerator, SimpleGuidGenerator>();
			}
			else
			{
				services.Configure(configureOptions);
				services.TryAddTransient<IGuidGenerator, SequentialGuidGenerator>();
			}

			return services;
		}

		/// <summary>
		///     Adds the string encryption service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddStringEncryptionService(this IServiceCollection services)
		{
			Guard.Against.Null(services, nameof(services));

			// Add logging infrastructure.
			services.AddLogging();

			// Add options infrastructure.
			services.AddOptions();

			services.AddTransient<IStringEncryptionService, StringEncryptionService>();

			return services;
		}

		/// <summary>
		///     Adds the string encryption service.
		/// </summary>
		/// <param name="services">The service collection.</param>
		/// <param name="configureOptions"></param>
		/// <returns>The service collection.</returns>
		public static IServiceCollection AddStringEncryptionService(this IServiceCollection services,
			Action<StringEncryptionOptions> configureOptions)
		{
			Guard.Against.Null(services, nameof(services));

			// Add logging infrastructure.
			services.AddLogging();

			// Add options infrastructure.
			services.AddOptions();

			services.Configure(configureOptions);

			services.AddTransient<IStringEncryptionService, StringEncryptionService>();

			return services;
		}
	}
}
