using DeliveryScheduler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryScheduler.Domain.Entities
{
	public class Product
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public ProductType Type { get; set; }
	}

	public record DeliverySlot(DateTime Date, TimeSpan Start, TimeSpan End, bool IsGreen);
}
