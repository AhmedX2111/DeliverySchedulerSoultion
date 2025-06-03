using DeliveryScheduler.Domain.Entities;
using DeliveryScheduler.Domain.Enums;
using DeliveryScheduler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryScheduler.Infrastructure.Repositories
{
	public class InMemoryProductRepository : IProductRepository
	{
		private readonly List<Product> _products = new()
		{
			new Product { Id = Guid.Parse("a0f47a90-5fc4-4f31-bbb9-0f5f4a0a98e3"), Name = "Milk", Type = ProductType.FreshFood },
			new Product { Id = Guid.Parse("b1c25c1f-9dc5-4cd1-9a17-89099e1ad1e6"), Name = "Soap", Type = ProductType.InStock },
			new Product { Id = Guid.Parse("c2d57d4a-3e25-4a2b-a2e2-29c72a29b8e9"), Name = "Imported Cheese", Type = ProductType.External },
		};


		public Task<List<Product>> GetProductsByIdsAsync(List<Guid> ids)
		{
			return Task.FromResult(_products.Where(p => ids.Contains(p.Id)).ToList());
		}
	}
}
