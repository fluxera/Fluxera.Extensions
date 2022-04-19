namespace Fluxera.Extensions.OData
{
	using System;
	using JetBrains.Annotations;

	[PublicAPI]
	internal static class Errors
	{
		public static Exception CanNotUpdateTransientItem()
		{
			const string message = "A transient item can not be updated. Add the item first.";
			return new InvalidOperationException(message);
		}

		public static Exception CanNotAddExistingItem()
		{
			const string message = "A non-transient item can not be added.";
			return new InvalidOperationException(message);
		}

		public static Exception CanNotDeleteTransientItem()
		{
			const string message = "A transient item can not be deleted. Add the item first.";
			return new InvalidOperationException(message);
		}
	}
}
