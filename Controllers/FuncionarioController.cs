using Hackaton_API.Context;
using Hackaton_API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        [HttpPost("cadastrar")]
        public ActionResult Cadastrar([FromBody] Funcionario funcionario)
        {
            try
            {
                ValidarNome(funcionario.Nome);
                ValidarEmail(funcionario.Email);
                ValidarSenha(funcionario.Senha);

                _context.Funcionarios.Add(funcionario);
                _context.SaveChanges();

                //retornar token
                return Created("", "");
            }
            catch (Exception e)
            {
                return BadRequest(new { erro = e.Message });
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Funcionario funcionario)
        {
            ValidarEmail(funcionario.Email);
            ValidarSenha(funcionario.Senha);

            var funcionarioLogado = _context.Funcionarios.Where(x => x.Email.ToLower() == funcionario.Senha && x.Senha == funcionario.Senha).FirstOrDefault();

            if (funcionarioLogado is null)
                return BadRequest(new { erro = "Email ou senha incorretos" });

            return Ok("");
        }

        private void ValidarNome(string nome)
        {
            var regex = new Regex(@"^[a-zA-Z].{3,}$");
            var match = regex.Match(nome ?? "");

            if (!match.Success)
                throw new Exception("Nome do funcionário deve conter pelo menos 3 caracteres e não possuir números");
        }

        private void ValidarEmail(string email)
        {
            var regex = new Regex(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+");
            var match = regex.Match(email ?? "");

            if (!match.Success)
                throw new Exception("Email inválido");
        }

        private void ValidarSenha(string senha)
        {
            var regex = new Regex(@"^(?=.*?[a-z])(?=.*?[0-9]).{8,}$");
            var match = regex.Match(senha ?? "");

            if (!match.Success)
                throw new Exception("Senha deve conter uma letra minuscula, 1 número e no mínimo 8 caracteres");
        }
    }
}
