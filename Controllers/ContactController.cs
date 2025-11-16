using Microsoft.AspNetCore.Mvc;
using GAB.Data;
using GAB.Models;

namespace GAB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public ContactController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // POST: api/contact
        [HttpPost]
        public async Task<IActionResult> PostMessage([FromBody] ContactMessage message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.ContactMessages.Add(message);
            await _context.SaveChangesAsync();

            // Optional: send email notification here
            // (see below step for that)

            return Ok(new { success = true });
        }

        // GET: api/contact
        [HttpGet]
        public IActionResult GetMessages()
        {
            var messages = _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .ToList();
            return Ok(messages);
        }
    }
}
