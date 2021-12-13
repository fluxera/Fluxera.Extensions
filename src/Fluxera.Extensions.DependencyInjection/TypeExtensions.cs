namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Reflection;

	public static class TypeExtensions
	{
		public static bool IsOpenGeneric(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}
	}
}
