using BlogProject.DataAccessLayer.Concrete;
using BlogProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogProject.APILayer.Controllers
{
    [ApiController]
    [Route("api/public/[controller]")]
    public class ContactsPublicController : ControllerBase
    {
        private readonly Context _context;

        public ContactsPublicController(Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Send(Contact model)
        {
            _context.Contacts.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }
    }
}


