using Microsoft.AspNetCore.Mvc;
using RestoBackEnd.Models;
using RestoBackEnd.Services;

namespace RestoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitchenOrdersController : ControllerBase
    {
        private readonly IKitchenOrderService _kitchenOrderService;

        public KitchenOrdersController(IKitchenOrderService kitchenOrderService)
        {
            _kitchenOrderService = kitchenOrderService;
        }

        // GET: api/KitchenOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KitchenOrder>>> GetKitchenOrders()
        {
            var kitchenOrders = await _kitchenOrderService.GetAllKitchenOrdersAsync();
            return Ok(kitchenOrders);
        }

        // GET: api/KitchenOrders/byOrder/5
        [HttpGet("byOrder/{orderId}")]
        public async Task<ActionResult<KitchenOrder>> GetKitchenOrderByOrderId(int orderId)
        {
            var kitchenOrder = await _kitchenOrderService.GetKitchenOrderByOrderIdAsync(orderId);

            if (kitchenOrder == null)
            {
                return NotFound();
            }

            return kitchenOrder;
        }

        // POST: api/KitchenOrders
        [HttpPost]
        public async Task<ActionResult<KitchenOrder>> PostKitchenOrder(KitchenOrder kitchenOrder)
        {
            var createdKitchenOrder = await _kitchenOrderService.CreateKitchenOrderAsync(kitchenOrder);
            return CreatedAtAction(nameof(GetKitchenOrderByOrderId), new { orderId = createdKitchenOrder.OrderId }, createdKitchenOrder);
        }

        // PUT: api/KitchenOrders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKitchenOrder(int id, KitchenOrder kitchenOrder)
        {
            var updated = await _kitchenOrderService.UpdateKitchenOrderAsync(id, kitchenOrder);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/KitchenOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKitchenOrder(int id)
        {
            var deleted = await _kitchenOrderService.DeleteKitchenOrderAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
