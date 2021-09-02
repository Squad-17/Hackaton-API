using Microsoft.AspNetCore.Mvc;

namespace Hackaton_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetFuncionarios()
        {
            return Ok(new string[] { "Matheus", "Renan", "Anderson" });
        }
    }
}
