using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : Controller
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            if (string.IsNullOrEmpty(id)) { return NotFound(); }

            return await Task.Factory.StartNew(Ok);
        }

        [HttpPost("{message}")]
        public async Task<IActionResult> Post(string message)
        {
            return await Task.Factory.StartNew(Ok);
        }
    }
}
