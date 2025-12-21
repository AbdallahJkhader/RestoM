namespace RestoBackEnd.Models
{
    public class KitchenOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Foreign key to Order
        public string ChefName { get; set; } = string.Empty;
        public DateTime PreparationStartTime { get; set; } = DateTime.Now;
        public int PreparationDuration { get; set; } = 10; // Default 10 minutes
        
        // Navigation property (optional)
        public Order? Order { get; set; }
    }
}
