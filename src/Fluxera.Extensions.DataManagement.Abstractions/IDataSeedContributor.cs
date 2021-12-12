namespace Fluxera.Extensions.DataManagement
{
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for a script that contributes to the data seeding of an application.
	/// </summary>
	[PublicAPI]
	public interface IDataSeedContributor
	{
		/// <summary>
		///		Checks if this contributor needs to run.
		/// </summary>
		Task<bool> NeedsDataSeedAsync();

		/// <summary>
		///		Executes the data seed script.
		/// </summary>
		Task SeedAsync();
	}
}
