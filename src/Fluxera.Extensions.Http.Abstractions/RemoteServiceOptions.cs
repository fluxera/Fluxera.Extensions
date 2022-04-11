namespace Fluxera.Extensions.Http
{
	using JetBrains.Annotations;

	/// <summary>
	///     The remote service options.
	/// </summary>
	[PublicAPI]
	public sealed class RemoteServiceOptions
	{
		/// <summary>
		///     Creates a new instance of the <see cref="RemoteServiceOptions" /> type.
		/// </summary>
		public RemoteServiceOptions()
		{
			this.RemoteServices = new RemoteServices();
		}

		/// <summary>
		///     Gets or sets the remote services.
		/// </summary>
		public RemoteServices RemoteServices { get; set; }
	}
}
