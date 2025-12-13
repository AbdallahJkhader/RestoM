using System.Text.Json.Serialization;

namespace RestoBackEnd.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? TableId { get; set; } // Nullable if order is takeout
        public int? EmployeeId { get; set; } // Link to Employee
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public string Status { get; set; } = "New"; // New, Preparing, Completed
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public string? PaymentMethod { get; set; } // Cash, Card
        public decimal PaidAmount { get; set; } // Amount paid by customer
        public bool IsPaid { get; set; } // Payment completion status
        public string? Notes { get; set; }
        
        public List<OrderItem> Items { get; set; } = new();
    }
}
