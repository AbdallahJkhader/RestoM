using System.Text.Json.Serialization;

namespace RestoBackEnd.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; } // Link to Product
        public required string Name { get; set; } // Snapshot of name in case product changes
        public decimal Price { get; set; } // Snapshot of price
        public int Quantity { get; set; }
    }
}
