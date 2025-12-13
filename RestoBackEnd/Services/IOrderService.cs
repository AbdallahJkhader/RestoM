using RestoBackEnd.Models;

namespace RestoBackEnd.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(string? status = null);
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> UpdateOrderAsync(int id, Order order);
        Task<bool> UpdateOrderStatusAsync(int id, string newStatus);
        Task<bool> DeleteOrderAsync(int id);
    }
}
