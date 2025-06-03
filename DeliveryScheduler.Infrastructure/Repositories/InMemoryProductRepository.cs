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
		new Product { Id = Guid.Parse("..."), Name = "Milk", Type = ProductType.FreshFood },
		new Product { Id = Guid.Parse("..."), Name = "Soap", Type = ProductType.InStock },
		new Product { Id = Guid.Parse("..."), Name = "Imported Cheese", Type = ProductType.External },
	    };

		public Task<List<Product>> GetProductsByIdsAsync(List<Guid> ids)
		{
			return Task.FromResult(_products.Where(p => ids.Contains(p.Id)).ToList());
		}
	}
}
