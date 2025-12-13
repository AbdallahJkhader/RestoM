using RestoBackEnd.Models;

namespace RestoBackEnd.Services
{
    public interface IReportService
    {
        Task<DashboardStats> GetDashboardStatsAsync();
    }
}
