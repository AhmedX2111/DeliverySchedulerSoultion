using DeliveryScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryScheduler.Domain.Interfaces
{
	public interface IProductRepository
	{
		Task<List<Product>> GetProductsByIdsAsync(List<Guid> ids);
	}
}
