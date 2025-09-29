using BlogProject.DataAccessLayer.Concrete;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.APILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageThreadsController : ControllerBase
    {
        private readonly Context _context;

        public MessageThreadsController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message2>>> GetAll()
        {
            var list = await _context.Messages2.AsNoTracking().ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message2>> GetById(int id)
        {
            var entity = await _context.Messages2.FindAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Message2>> Create(Message2 model)
        {
            _context.Messages2.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.MessageId }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Message2 model)
        {
            if (id != model.MessageId) return BadRequest();
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Messages2.FindAsync(id);
            if (entity == null) return NotFound();
            _context.Messages2.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}


