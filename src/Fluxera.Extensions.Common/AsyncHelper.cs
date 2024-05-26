// ReSharper disable once CheckNamespace

namespace Fluxera.Utilities
{
	using System;
	using System.Globalization;
	using System.Reflection;
	using System.Security.Principal;
	using System.Threading;
	using System.Threading.Tasks;
	using Fluxera.Extensions.Common;

	/// <summary>
	///     Helper for calling async code correctly in a synchronous context.
	/// </summary>
	/// <remarks>
	///     http://stackoverflow.com/questions/9343594/how-to-call-asynchronous-method-from-synchronous-method-in-c
	///     https://stackoverflow.com/a/9343733
	/// </remarks>
	internal static class AsyncHelper
	{
		private static readonly TaskFactory taskFactory = new TaskFactory(CancellationToken.None,
			TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

		/// <summary>
		///     Runs the given function synchronous and returns its result.
		/// </summary>
		/// <typeparam name="TResult">The type if the result.</typeparam>
		/// <param name="func">The wrapper function to run.</param>
		/// <returns>The result of the function.</returns>
		public static TResult RunSync<TResult>(Func<Task<TResult>> func)
		{
			// http://stackoverflow.com/a/27240734
			IPrincipal principal = Thread.CurrentPrincipal;
			CultureInfo cultureUi = CultureInfo.CurrentUICulture;
			CultureInfo culture = CultureInfo.CurrentCulture;

			return taskFactory.StartNew(() =>
				{
					Thread.CurrentPrincipal = principal;
					Thread.CurrentThread.CurrentCulture = culture;
					Thread.CurrentThread.CurrentUICulture = cultureUi;
					return func();
				})
				.Unwrap()
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		///     Runs the given function synchronous.
		/// </summary>
		/// <param name="func">The wrapper function to run.</param>
		public static void RunSync(Func<Task> func)
		{
			// http://stackoverflow.com/a/27240734
			IPrincipal principal = Thread.CurrentPrincipal;
			CultureInfo cultureUi = CultureInfo.CurrentUICulture;
			CultureInfo culture = CultureInfo.CurrentCulture;

			taskFactory.StartNew(() =>
				{
					Thread.CurrentPrincipal = principal;
					Thread.CurrentThread.CurrentCulture = culture;
					Thread.CurrentThread.CurrentUICulture = cultureUi;
					return func();
				})
				.Unwrap()
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		/// <summary>
		///     Checks if given method is an async method.
		/// </summary>
		/// <param name="method">A method to check</param>
		public static bool IsAsync(this MethodInfo method)
		{
			Guard.ThrowIfNull(method);

			Type type = method.ReturnType;
			return type == typeof(Task) ||
				type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>);
		}
	}
}
