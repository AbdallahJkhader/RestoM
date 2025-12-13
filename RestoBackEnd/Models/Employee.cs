namespace RestoBackEnd.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Role { get; set; } // Waiter, Chef, Manager
    }
}
