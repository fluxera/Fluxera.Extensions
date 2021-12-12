namespace Fluxera.Extensions.DataManagement
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using JetBrains.Annotations;

	[UsedImplicitly]
	internal sealed class DataSeeder : IDataSeeder
	{
		public async Task SeedAsync(IEnumerable<IDataSeedContributor> contributors)
		{
			foreach(IDataSeedContributor contributor in contributors)
			{
				bool needsDataSeed = await contributor.NeedsDataSeedAsync();
				if(needsDataSeed)
				{
					await contributor.SeedAsync();
				}
			}
		}
	}
}
