using Hackaton_API.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Hackaton_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalController : ControllerBase
    {

        private readonly ApiContext _context;
        public LocalController(ApiContext context) => _context = context;

        [HttpGet]
        public ActionResult GetLocal()
        {
            return Ok(_context.Locais.ToList());
        }

    }
}