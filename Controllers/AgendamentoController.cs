using Hackaton_API.Context;
using Hackaton_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hackaton_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly ApiContext _context;
        public AgendamentoController(ApiContext context) => _context = context;

        [HttpGet]
        public ActionResult getDias(){
            return Ok("teste");
        }
    }
}