using DeliveryScheduler.Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryScheduler.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DeliveryController : ControllerBase
	{
		private readonly GetAvailableDeliverySlots _useCase;

		public DeliveryController(GetAvailableDeliverySlots useCase)
		{
			_useCase = useCase;
		}

		[HttpPost("slots")]
		public async Task<IActionResult> GetSlots([FromBody] List<Guid> productIds)
		{
			var slots = await _useCase.ExecuteAsync(productIds, DateTime.Now);
			return Ok(slots); 
		}
	}
}
