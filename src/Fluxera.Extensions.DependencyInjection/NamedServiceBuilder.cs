namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	///     A base-class for builders that help configure named services.
	/// </summary>
	/// <typeparam name="TService"></typeparam>
	[PublicAPI]
	public abstract class NamedServiceBuilder<TService> where TService : class
	{
		private readonly IDictionary<string, IList<Type>> implementationTypeMap = new ConcurrentDictionary<string, IList<Type>>();

		internal NamedServiceBuilder(IServiceCollection services)
		{
			this.Services = services;
		}

		/// <summary>
		///     Gets the service collection.
		/// </summary>
		protected IServiceCollection Services { get; }

		internal NamedServiceMapper<TService> BuildMapper(NamedServiceMapper<TService> namedServiceMapper = null)
		{
			if(namedServiceMapper == null)
			{
				namedServiceMapper = new NamedServiceMapper<TService>(this.implementationTypeMap);
			}
			else
			{
				namedServiceMapper.Add(this.implementationTypeMap);
			}

			return namedServiceMapper;
		}

		/// <summary>
		///     Adds a named type map.
		/// </summary>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="name"></param>
		protected internal void AddTypeMap<TImplementation>(string name) where TImplementation : class, TService
		{
			if(!this.implementationTypeMap.ContainsKey(name))
			{
				this.implementationTypeMap.Add(name, new List<Type>());
			}

			this.implementationTypeMap[name].Add(typeof(TImplementation));
		}
	}
}
