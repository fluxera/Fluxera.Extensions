namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using Fluxera.Guards;

	internal sealed class NamedServiceMapper<TService>
	{
		private readonly IDictionary<string, Type> implementationTypeMap;

		public NamedServiceMapper(IDictionary<string, Type> implementationTypeMap)
		{
			Guard.Against.Null(implementationTypeMap, nameof(implementationTypeMap));

			this.implementationTypeMap = new ConcurrentDictionary<string, Type>(implementationTypeMap);
		}

		public Type GetImplementationType(string name)
		{
			this.implementationTypeMap.TryGetValue(name, out Type result);
			return result!;
		}

		public void Add(IDictionary<string, Type> implementationTypeMap)
		{
			foreach(KeyValuePair<string, Type> keyValuePair in implementationTypeMap)
			{
				this.implementationTypeMap.Add(keyValuePair);
			}
		}
	}
}
