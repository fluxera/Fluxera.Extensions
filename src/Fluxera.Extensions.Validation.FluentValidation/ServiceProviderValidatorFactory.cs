namespace Fluxera.Extensions.Validation.FluentValidation
{
	using System;
	using System.Reflection;
	using global::FluentValidation;

	internal sealed class ServiceProviderValidatorFactory : IValidatorFactory
	{
		private readonly IServiceProvider serviceProvider;

		public ServiceProviderValidatorFactory(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public IValidator<T> GetValidator<T>()
		{
			return (IValidator<T>)this.GetValidator(typeof(T));
		}

		public IValidator GetValidator(Type type)
		{
			IValidator validator = this.CreateInstance(typeof(IValidator<>).MakeGenericType(type));

			if(validator is null)
			{
				// Get base type and try to find validator for base type (used for polymorphic classes).
				Type baseType = type.GetTypeInfo().BaseType;
				if(baseType == null)
				{
					// Return no validator if no base type was found, i.e. we reached object.
					return null;
				}

				// Find a validator base-type-recursive.
				validator = this.GetValidator(baseType);
			}

			return validator;
		}

		private IValidator CreateInstance(Type validatorType)
		{
			return this.serviceProvider.GetService(validatorType) as IValidator;
		}
	}
}
