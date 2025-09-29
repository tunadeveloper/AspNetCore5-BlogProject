using BlogProject.BusinessLayer.Abstract;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogProject.APILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAll()
        {
            var list = _service.GetAllBL();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetById(int id)
        {
            var entity = _service.GetByIdBL(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Comment>> Create(Comment model)
        {
            _service.InsertBL(model);
            return Ok("Eklendi");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Comment model)
        {
            if (id != model.CommentId) return BadRequest();
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


