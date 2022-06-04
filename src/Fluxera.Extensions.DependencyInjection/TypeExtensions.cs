namespace Fluxera.Extensions.DependencyInjection
{
	using System;
	using System.Reflection;

	/// <summary>
	///     Extensions for the <see cref="Type" /> type.
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		///     Checks if the given type is a generic type definition.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public static bool IsOpenGeneric(this Type type)
		{
			return type.GetTypeInfo().IsGenericTypeDefinition;
		}
	}
}
