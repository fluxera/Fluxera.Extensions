namespace Fluxera.Extensions.DataManagement
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	/// <summary>
	///		A contract for a data see script executor.
	/// </summary>
	[PublicAPI]
	public interface IDataSeeder
	{
		/// <summary>
		///		Executes all given seed script contributors.
		/// </summary>
		/// <param name="contributors">The data seed scripts to execute.</param>
		Task SeedAsync(IEnumerable<IDataSeedContributor> contributors);
	}
}
