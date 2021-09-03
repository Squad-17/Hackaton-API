using Hackaton_API.Context;
using Hackaton_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Hackaton_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly ApiContext _context;

        public FuncionarioController(ApiContext context) => _context = context;

        [HttpGet]
        public List<Funcionario> GetFuncionarios()
        {
            return _context.Funcionarios.ToList();
        }
    }
}
