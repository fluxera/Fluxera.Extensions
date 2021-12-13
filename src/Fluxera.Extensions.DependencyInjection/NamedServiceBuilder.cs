namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using JetBrains.Annotations;
	using Microsoft.Extensions.DependencyInjection;

	[PublicAPI]
	public abstract class NamedServiceBuilder<TService> where TService : class
	{
		private readonly IDictionary<string, Type> implementationTypeMap = new ConcurrentDictionary<string, Type>();

		internal NamedServiceBuilder(IServiceCollection services)
		{
			this.Services = services;
		}

		protected IServiceCollection Services { get; }

		internal NamedServiceMapper<TService> BuildMapper(NamedServiceMapper<TService>? namedServiceMapper = null)
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

		protected internal void AddTypeMap<TImplementation>(string name) where TImplementation : class, TService
		{
			this.implementationTypeMap.Add(name, typeof(TImplementation));
		}
	}
}
