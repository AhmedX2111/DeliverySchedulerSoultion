using DeliveryScheduler.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryScheduler.Domain.Interfaces
{
	public interface IDeliverySlotService
	{
		DateTime GetEarliestDeliveryDate(List<Product> products, DateTime now);
		List<DeliverySlot> GenerateSlots(DateTime fromDate, DateTime now, List<Product> products);
	}
}
