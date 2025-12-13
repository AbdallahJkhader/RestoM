using RestoBackEnd.Models;

namespace RestoBackEnd.Services
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAllTablesAsync();
        Task<Table?> GetTableByIdAsync(int id);
        Task<bool> UpdateTableAsync(int id, Table table);
    }
}
