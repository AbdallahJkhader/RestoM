using Microsoft.EntityFrameworkCore;
using RestoBackEnd.Data;
using RestoBackEnd.Models;

namespace RestoBackEnd.Services
{
    public class TableService : ITableService
    {
        private readonly RestoDbContext _context;

        public TableService(RestoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _context.Tables.ToListAsync();
        }

        public async Task<Table?> GetTableByIdAsync(int id)
        {
            return await _context.Tables.FindAsync(id);
        }

        public async Task<bool> UpdateTableAsync(int id, Table table)
        {
            if (id != table.Id)
            {
                return false;
            }

            _context.Entry(table).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Tables.AnyAsync(e => e.Id == id))
                {
                    return false;
                }
                throw;
            }
        }
    }
}
