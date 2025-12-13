namespace RestoBackEnd.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } // For inventory
        public string? Category { get; set; } // main, appetizers, drinks
        public string? Description { get; set; }
    }
}
