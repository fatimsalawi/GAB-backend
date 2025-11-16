using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAB.Data;
using GAB.Models;

namespace GAB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public LocationsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _db.Locations.OrderBy(x => x.Id).ToListAsync());
    }
}
