namespace RestoBackEnd.Models
{
    public class Table
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsOccupied { get; set; }
        public int Capacity { get; set; }
    }
}
