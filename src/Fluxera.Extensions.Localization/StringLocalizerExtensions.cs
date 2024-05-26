namespace Fluxera.Extensions.Localization
{
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using Microsoft.Extensions.Localization;

	/// <summary>
	///     Extension methods for the <see cref="IStringLocalizer" /> type.
	/// </summary>
	[PublicAPI]
	public static class StringLocalizerExtensions
	{
		/// <summary>
		///     Gets the string resource with the given name.
		/// </summary>
		/// <param name="stringLocalizer">The <see cref="IStringLocalizer" />.</param>
		/// <param name="name">The name of the string resource.</param>
		/// <returns>The string resource as a <see cref="LocalizedString" />.</returns>
		public static string GetStringEx(this IStringLocalizer stringLocalizer, string name)
		{
			Guard.ThrowIfNull(stringLocalizer);
			Guard.ThrowIfNull(name);

			return stringLocalizer[name].Value;
		}

		/// <summary>
		///     Gets the string resource with the given name and formatted with the supplied arguments.
		/// </summary>
		/// <param name="stringLocalizer">The <see cref="IStringLocalizer" />.</param>
		/// <param name="name">The name of the string resource.</param>
		/// <param name="arguments">The values to format the string with.</param>
		/// <returns>The formatted string resource as a <see cref="LocalizedString" />.</returns>
		public static string GetStringEx(this IStringLocalizer stringLocalizer, string name, params object[] arguments)
		{
			Guard.ThrowIfNull(stringLocalizer);
			Guard.ThrowIfNull(name);

			return stringLocalizer[name, arguments].Value;
		}

		/// <summary>
		///     Gets all string resources including those for parent cultures.
		/// </summary>
		/// <param name="stringLocalizer">The <see cref="IStringLocalizer" />.</param>
		/// <returns>The string resources.</returns>
		public static IEnumerable<string> GetAllStringsEx(this IStringLocalizer stringLocalizer)
		{
			Guard.ThrowIfNull(stringLocalizer);

			return stringLocalizer.GetAllStrings(true).Select(x => x.ToString());
		}
	}
}
