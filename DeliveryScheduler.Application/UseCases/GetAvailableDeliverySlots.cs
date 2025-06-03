using DeliveryScheduler.Application.Interfaces;
using DeliveryScheduler.Domain.Entities;
using DeliveryScheduler.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryScheduler.Application.UseCases
{
	public class GetAvailableDeliverySlots
	{
		private readonly IProductRepository _productRepo;
		private readonly IDeliverySlotService _slotService;

		public GetAvailableDeliverySlots(IProductRepository repo, IDeliverySlotService slotService)
		{
			_productRepo = repo;
			_slotService = slotService;
		}

		public async Task<List<DeliverySlot>> ExecuteAsync(List<Guid> productIds, DateTime now)
		{
			var products = await _productRepo.GetProductsByIdsAsync(productIds);
			var earliest = _slotService.GetEarliestDeliveryDate(products, now);
			var slots = _slotService.GenerateSlots(earliest, now, products);
			return slots;
		}
	}
}
