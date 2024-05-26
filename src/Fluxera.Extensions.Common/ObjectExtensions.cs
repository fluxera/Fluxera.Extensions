namespace Fluxera.Extensions.Common
{
	using System;

	internal static class ObjectExtensions
	{
		/// <summary>
		///     Executes the given <paramref name="action" /> by locking on the given <paramref name="source" /> object.
		/// </summary>
		/// <typeparam name="T">Type of the object (to be locked)</typeparam>
		/// <param name="source">Source object (to be locked)</param>
		/// <param name="action">Action (to be executed)</param>
		public static void Locking<T>(this T source, Action<T> action) where T : class
		{
			lock(source)
			{
				action(source);
			}
		}
	}
}
