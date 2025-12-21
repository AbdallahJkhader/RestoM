using RestoBackEnd.Data;
using RestoBackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace RestoBackEnd.Services
{
    public interface IKitchenOrderService
    {
        Task<IEnumerable<KitchenOrder>> GetAllKitchenOrdersAsync();
        Task<KitchenOrder?> GetKitchenOrderByOrderIdAsync(int orderId);
        Task<KitchenOrder> CreateKitchenOrderAsync(KitchenOrder kitchenOrder);
        Task<bool> UpdateKitchenOrderAsync(int id, KitchenOrder kitchenOrder);
        Task<bool> DeleteKitchenOrderAsync(int id);
    }

    public class KitchenOrderService : IKitchenOrderService
    {
        private readonly RestoDbContext _context;

        public KitchenOrderService(RestoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KitchenOrder>> GetAllKitchenOrdersAsync()
        {
            return await _context.KitchenOrders
                .Include(ko => ko.Order)
                .ToListAsync();
        }

        public async Task<KitchenOrder?> GetKitchenOrderByOrderIdAsync(int orderId)
        {
            return await _context.KitchenOrders
                .Include(ko => ko.Order)
                .FirstOrDefaultAsync(ko => ko.OrderId == orderId);
        }

        public async Task<KitchenOrder> CreateKitchenOrderAsync(KitchenOrder kitchenOrder)
        {
            _context.KitchenOrders.Add(kitchenOrder);
            await _context.SaveChangesAsync();
            return kitchenOrder;
        }

        public async Task<bool> UpdateKitchenOrderAsync(int id, KitchenOrder kitchenOrder)
        {
            var existing = await _context.KitchenOrders.FindAsync(id);
            if (existing == null) return false;

            existing.ChefName = kitchenOrder.ChefName;
            existing.PreparationStartTime = kitchenOrder.PreparationStartTime;
            existing.PreparationDuration = kitchenOrder.PreparationDuration;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteKitchenOrderAsync(int id)
        {
            var kitchenOrder = await _context.KitchenOrders.FindAsync(id);
            if (kitchenOrder == null) return false;

            _context.KitchenOrders.Remove(kitchenOrder);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
