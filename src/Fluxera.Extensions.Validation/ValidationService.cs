namespace Fluxera.Extensions.Validation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using FluentValidation;
	using FluentValidation.Results;
	using Fluxera.Utilities.Extensions;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[UsedImplicitly]
	internal sealed class ValidationService : IValidationService
	{
		private readonly IServiceProvider serviceProvider;

		public ValidationService(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		/// <inheritdoc />
		public ValidationResult Validate<T>(T item)
		{
			IEnumerable<IValidator<T>> validators = this.serviceProvider.GetServices<IValidator<T>>();

			IEnumerable<ValidationFailure> validationFailures = validators
				.Select(validator => validator.Validate(item))
				.SelectMany(validationResult => validationResult.Errors)
				.Where(validationFailure => validationFailure is not null);

			return new ValidationResult(validationFailures);
		}

		/// <inheritdoc />
		public void ValidateAndThrow<T>(T item)
		{
			ValidationResult result = this.Validate(item);

			if(!result.IsValid)
			{
				throw new ValidationException(result.Errors);
			}
		}

		/// <inheritdoc />
		public async Task<ValidationResult> ValidateAsync<T>(T item, CancellationToken cancellationToken = default)
		{
			IList<ValidationFailure> validationFailures = new List<ValidationFailure>();

			IEnumerable<IValidator<T>> validators = this.serviceProvider.GetServices<IValidator<T>>();
			foreach(IValidator<T> validator in validators)
			{
				ValidationResult validationResult = await validator.ValidateAsync(item, cancellationToken);
				IEnumerable<ValidationFailure> failures = validationResult.Errors.Where(validationFailure => validationFailure is not null);
				validationFailures.AddRange(failures);
			}

			return new ValidationResult(validationFailures);
		}

		/// <inheritdoc />
		public async Task ValidateAndThrowAsync<T>(T item, CancellationToken cancellationToken = default)
		{
			ValidationResult result = await this.ValidateAsync(item, cancellationToken);

			if(!result.IsValid)
			{
				throw new ValidationException(result.Errors);
			}
		}
	}
}
