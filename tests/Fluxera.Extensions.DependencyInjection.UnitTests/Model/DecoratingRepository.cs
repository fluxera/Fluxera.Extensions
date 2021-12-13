namespace Fluxera.Extensions.DependencyInjection.UnitTests.Model
{
	public class DecoratingRepository<T> : IRepository<T>
	{
		public DecoratingRepository(IRepository<T> innerService)
		{
			this.InnerService = innerService;
		}

		public IRepository<T> InnerService { get; }
	}
}
