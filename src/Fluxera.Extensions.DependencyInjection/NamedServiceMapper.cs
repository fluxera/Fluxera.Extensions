namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using Fluxera.Guards;
	using Fluxera.Utilities.Extensions;

	internal sealed class NamedServiceMapper<TService>
	{
		private readonly IDictionary<string, IList<Type>> implementationTypeMap;

		public NamedServiceMapper(IDictionary<string, IList<Type>> implementationTypeMap)
		{
			Guard.Against.Null(implementationTypeMap, nameof(implementationTypeMap));

			this.implementationTypeMap = new ConcurrentDictionary<string, IList<Type>>(implementationTypeMap);
		}

		public Type GetImplementationType(string name)
		{
			this.implementationTypeMap.TryGetValue(name, out IList<Type> result);
			return result.FirstOrDefault();
		}

		public IEnumerable<Type> GetImplementationTypes(string name)
		{
			this.implementationTypeMap.TryGetValue(name, out IList<Type> result);

			foreach(Type type in result ?? Enumerable.Empty<Type>())
			{
				yield return type;
			}
		}

		public void Add(IDictionary<string, IList<Type>> typeMap)
		{
			foreach (KeyValuePair<string, IList<Type>> keyValuePair in typeMap)
			{
				if(!this.implementationTypeMap.ContainsKey(keyValuePair.Key))
				{
					this.implementationTypeMap.Add(keyValuePair);
				}

				foreach(Type type in keyValuePair.Value.ToList())
				{
					this.implementationTypeMap[keyValuePair.Key].Add(type);	
				}
			}
		}
	}
}
