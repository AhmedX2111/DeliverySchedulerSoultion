using DeliveryScheduler.Application.Interfaces;
using DeliveryScheduler.Domain.Entities;
using DeliveryScheduler.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryScheduler.Application.Services
{
	public class DeliverySlotService : IDeliverySlotService
	{
		public DateTime GetEarliestDeliveryDate(List<Product> products, DateTime now)
		{
			DateTime latest = now.Date;

			foreach (var product in products)
			{
				switch (product.Type)
				{
					case ProductType.InStock:
						if (now.TimeOfDay > TimeSpan.FromHours(18))
							latest = latest.AddDays(1);
						break;
					case ProductType.FreshFood:
						if (now.TimeOfDay > TimeSpan.FromHours(12))
							latest = latest.AddDays(1);
						break;
					case ProductType.External:
						latest = now.Date.AddDays(3);
						break;
				}
			}

			return AdjustToNextValidWeekday(latest, products);
		}

		public List<DeliverySlot> GenerateSlots(DateTime start, DateTime now, List<Product> products)
		{
			var slots = new List<DeliverySlot>();
			var end = now.Date.AddDays(14);
			var greenRanges = new List<(int Start, int End)>
		    {
			(13, 15),
			(20, 22)
		    };

			for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
			{
				if (!IsWeekday(date)) continue;
				if (products.Any(p => p.Type == ProductType.External) && date.DayOfWeek == DayOfWeek.Monday)
					continue;

				for (int hour = 8; hour < 22; hour++)
				{
					bool isGreen = greenRanges.Any(r => hour >= r.Start && hour < r.End);
					slots.Add(new DeliverySlot(date, TimeSpan.FromHours(hour), TimeSpan.FromHours(hour + 1), isGreen));
				}
			}

			return slots
				.OrderBy(s => s.Date)
				.ThenByDescending(s => s.IsGreen)
				.ThenBy(s => s.Start)
				.ToList();
		}

		private bool IsWeekday(DateTime date) =>
			date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday;

		private DateTime AdjustToNextValidWeekday(DateTime date, List<Product> products)
		{
			while (!IsWeekday(date) || (products.Any(p => p.Type == ProductType.External) && date.DayOfWeek == DayOfWeek.Monday))
			{
				date = date.AddDays(1);
			}
			return date;
		}
	}
}
