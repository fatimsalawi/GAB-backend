using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAB.Data;
using GAB.Models;

namespace GAB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public MenuItemsController(AppDbContext db) => _db = db;

        // flat list (kept for compatibility)
        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _db.MenuItems.OrderBy(x => x.Category).ThenBy(x => x.Id).ToListAsync());

        // grouped by Category: { "Sandwiches": [...], "Desserts": [...] }
        [HttpGet("grouped")]
        public async Task<IActionResult> GetGrouped()
        {
            var items = await _db.MenuItems
                .OrderBy(x => x.Category).ThenBy(x => x.Id)
                .ToListAsync();

            var grouped = items
                .GroupBy(x => x.Category)
                .ToDictionary(g => g.Key, g => g.ToList());

            return Ok(grouped);
        }
    }
}
