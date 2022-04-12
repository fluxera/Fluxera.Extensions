[![Build Status](https://dev.azure.com/fluxera/Foundation/_apis/build/status/GitHub/fluxera.Fluxera.Extensions?branchName=main)](https://dev.azure.com/fluxera/Foundation/_build/latest?definitionId=65&branchName=main)

# Fluxera.Extensions
A library that extends the Microsoft.Extensions.* libraries with custom functionality and also provices custom extensions.

### ```Fluxera.Extensions.Caching```

This extension contains ```Option``` classes for configuring a remote distributed cache server and
several useful extension methods for the ```IDistributedCache``` service.

### ```Fluxera.Extensions.Common```

This extension contains several custom services:

- ```IDateTimeOffsetProvider``` A service that provides mockable access to static ```DateTimeOffset```.
- ```IDateTimeProvider``` A service that provides mockable access to static ```DateTime```.
- ```IGuidGenerator``` A service to generate ```Guid``` using different generators.
- ```IHashCalculator``` A service to calculate hashes from input values.
- ```IJitterCalculator``` A service that adds entropy to any given number.
- ```IPasswordGenerator``` A service that generates random passwords.
- ```IRetryDelayCalculator``` A service that calculates retry delay with (truncated) binary exponential back-off.
- ```IStringEncryptionService``` A service that can be used to simply encrypt/decrypt texts.

### ```Fluxera.Extensions.DataManagement```

This extension contains an infrastructure for insertng seed data to databases.

### ```Fluxera.Extensions.DependencyInjection```
 
This extension contains several additions to the dependency injection extension.

- Decorator
  - Add decorators to services.
- Named Services
  - Add named service implementations.
- Lazy Services
  - Add ```Lazy<T>``` as open generic sevice type. Any service will be resolved lazily from it.
- Object Accessor
  - Provides a way to access object instances from the ```IServiceCollection``` while still configuring services.

### ```Fluxera.Extensions.Http```

TODO

### ```Fluxera.Extensions.Localization```

This extension contains several extension methods ```IStringLocalizer``` service.

### ```Fluxera.Extensions.OData```

TODO

### ```Fluxera.Extensions.Validation```

This extension provides an abstraction over validation frameworks. Any one framework can
be used together with other ones. The validation results will merged by the extension.

At the moment ```System.ComponentModel.Annotations``` and ```FluentValidation``` is supported.
One can configure the validators to use like this:

```C#
IServiceCollection services = new ServiceCollection();

services.AddValidation(builder =>
{
    builder
        .AddDataAnnotations()
        .AddFluentValidation(registration =>
        {
            registration.AddValidator<PersonValidator>();
        });
});
```


## References

[Steve Collins](https://stevetalkscode.co.uk/)

- [Named Dependencies Part 1](https://stevetalkscode.co.uk/named-dependencies-part-1)
- [Named Dependencies Part 1](https://stevetalkscode.co.uk/named-dependencies-part-2)

