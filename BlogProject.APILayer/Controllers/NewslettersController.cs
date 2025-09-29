using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogProject.APILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewslettersController : ControllerBase
    {
        private readonly INewsletterService _service;

        public NewslettersController(INewsletterService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Newsletter>>> GetAll()
        {
            var list = _service.GetAllBL();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Newsletter>> GetById(int id)
        {
            var entity = _service.GetByIdBL(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Newsletter>> Create(Newsletter model)
        {
            _service.InsertBL(model);
            return Ok("Eklendi");
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


