using Microsoft.EntityFrameworkCore;
using RestoBackEnd.Data;
using RestoBackEnd.Models;

namespace RestoBackEnd.Services
{
    public class ReportService : IReportService
    {
        private readonly RestoDbContext _context;

        public ReportService(RestoDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStats> GetDashboardStatsAsync()
        {
            var today = DateTime.Today;

            // Daily Sales
            var todaysOrders = await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.OrderTime.Date == today)
                .ToListAsync();

            var dailySales = new DailySalesDto
            {
                Total = todaysOrders.Sum(o => o.TotalAmount),
                Orders = todaysOrders.Select(o => new DailyOrderSummary
                {
                    Id = o.Id,
                    Table = o.TableId.HasValue ? $"طاولة {o.TableId}" : "Takeout",
                    Amount = o.TotalAmount,
                    Time = o.OrderTime.ToString("HH:mm")
                }).ToList()
            };

            // Monthly Sales (last 30 days)
            var monthlySales = new MonthlySalesDto();
            for (int i = 0; i < 30; i++)
            {
                var date = today.AddDays(-29 + i);
                var daySales = await _context.Orders
                    .Where(o => o.OrderTime.Date == date)
                    .SumAsync(o => o.TotalAmount);

                monthlySales.Days.Add(new DailySalesData
                {
                    Day = date.Day,
                    Sales = daySales
                });
            }

            // Top Items
            var topItems = await _context.OrderItems
                .GroupBy(oi => oi.Name)
                .Select(g => new TopItemDto
                {
                    Name = g.Key,
                    Qty = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(ti => ti.Qty)
                .Take(5)
                .ToListAsync();

            // Employee Performance
            var employeeStats = await _context.Orders
                .Where(o => o.EmployeeId != null)
                .GroupBy(o => o.EmployeeId)
                .Select(g => new
                {
                    EmployeeId = g.Key,
                    OrdersCount = g.Count(),
                    TotalSales = g.Sum(o => o.TotalAmount)
                })
                .ToListAsync();

            var employeePerformance = new List<EmployeePerformanceDto>();
            foreach (var stat in employeeStats)
            {
                var employee = await _context.Employees.FindAsync(stat.EmployeeId);
                if (employee != null)
                {
                    employeePerformance.Add(new EmployeePerformanceDto
                    {
                        Name = employee.Name,
                        OrdersCount = stat.OrdersCount,
                        TotalSales = stat.TotalSales
                    });
                }
            }

            return new DashboardStats
            {
                DailySales = dailySales,
                MonthlySales = monthlySales,
                TopItems = topItems,
                EmployeeStats = employeePerformance
            };
        }
    }
}
