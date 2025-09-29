using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogProject.APILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WritersController : ControllerBase
    {
        private readonly IWriterService _service;

        public WritersController(IWriterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Writer>>> GetAll()
        {
            var list = _service.GetAllBL();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Writer>> GetById(int id)
        {
            var entity = _service.GetByIdBL(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Writer>> Create(Writer model)
        {
            _service.InsertBL(model);
            return Ok("Eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Writer model)
        {
            if (id != model.WriterId) return BadRequest();
            _service.UpdateBL(model);
            return Ok("Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = _service.GetByIdBL(id);
            if (entity == null) return NotFound();
            _service.DeleteBL(entity);
            return Ok("Silindi");
        }
    }
}


