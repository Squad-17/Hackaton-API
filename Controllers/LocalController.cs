using Hackaton_API.Context;
using Hackaton_API.Models;
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
        Local local = new Local();

        [HttpGet]
        public ActionResult GetLocal()
        {
            return Ok(_context.Locais.ToList());
        }

      
    }
}