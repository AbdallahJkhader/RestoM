namespace RestoBackEnd.Models
{
    // DTOs for Reports.html data structure

    public class DashboardStats
    {
        public DailySalesDto DailySales { get; set; } = new();
        public MonthlySalesDto MonthlySales { get; set; } = new();
        public List<TopItemDto> TopItems { get; set; } = new();
        public List<EmployeePerformanceDto> EmployeeStats { get; set; } = new();
    }

    public class DailySalesDto
    {
        public decimal Total { get; set; }
        public List<DailyOrderSummary> Orders { get; set; } = new();
    }

    public class DailyOrderSummary
    {
        public int Id { get; set; }
        public string Table { get; set; } = string.Empty; // Table Name or Number
        public decimal Amount { get; set; }
        public string Time { get; set; } = string.Empty; // "HH:mm"
    }

    public class MonthlySalesDto
    {
        public List<DailySalesData> Days { get; set; } = new();
    }

    public class DailySalesData
    {
        public int Day { get; set; } // 1-31
        public decimal Sales { get; set; }
    }

    public class TopItemDto
    {
        public required string Name { get; set; }
        public int Qty { get; set; }
    }

    public class EmployeePerformanceDto
    {
        public required string Name { get; set; }
        public int OrdersCount { get; set; }
        public decimal TotalSales { get; set; }
    }
}
