using Microsoft.AspNetCore.Mvc;
using RestoBackEnd.Models;
using RestoBackEnd.Services;

namespace RestoBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        // GET: api/Tables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        // GET: api/Tables/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);

            if (table == null)
            {
                return NotFound();
            }

            return table;
        }

        // PUT: api/Tables/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Table table)
        {
            var updated = await _tableService.UpdateTableAsync(id, table);
            
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
