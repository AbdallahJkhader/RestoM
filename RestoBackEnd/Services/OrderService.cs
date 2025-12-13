using Microsoft.EntityFrameworkCore;
using RestoBackEnd.Data;
using RestoBackEnd.Models;

namespace RestoBackEnd.Services
{
    public class OrderService : IOrderService
    {
        private readonly RestoDbContext _context;

        public OrderService(RestoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(string? status = null)
        {
            var query = _context.Orders.Include(o => o.Items).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status.ToLower() == status.ToLower());
            }

            return await query.OrderByDescending(o => o.OrderTime).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // Ensure OrderTime is set
            if (order.OrderTime == default)
            {
                order.OrderTime = DateTime.Now;
            }

            // Deduct inventory quantities for each order item
            foreach (var item in order.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found");
                }

                // Check if enough quantity is available
                if (product.Quantity < item.Quantity)
                {
                    throw new InvalidOperationException($"Insufficient quantity for product '{product.Name}'. Available: {product.Quantity}, Requested: {item.Quantity}");
                }

                // Deduct the quantity
                product.Quantity -= item.Quantity;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> UpdateOrderAsync(int id, Order order)
        {
            if (id != order.Id)
            {
                return false;
            }

            _context.Entry(order).State = EntityState.Modified;

            // Update order items
            foreach (var item in order.Items)
            {
                if (item.Id == 0)
                {
                    _context.OrderItems.Add(item);
                }
                else
                {
                    _context.Entry(item).State = EntityState.Modified;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Orders.AnyAsync(e => e.Id == id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, string newStatus)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return false;
            }

            order.Status = newStatus;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return false;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
