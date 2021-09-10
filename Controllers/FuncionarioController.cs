using Hackaton_API.Context;
using Hackaton_API.Models;
using Hackaton_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        private int IdFuncionarioAutenticado => int.Parse(User.FindFirst("Id").Value);

        [HttpPost("cadastrar")]
        public ActionResult Cadastrar([FromBody] Funcionario funcionario)
        {
            try
            {
                ValidarNome(funcionario.Nome);
                ValidarEmail(funcionario.Email);
                ValidarSenha(funcionario.Senha);

                var count = _context.Funcionarios.Where(x => x.Email.ToLower() == funcionario.Email.ToLower()).Count();

                if (count > 0)
                    throw new Exception("Email já cadastrado");

                _context.Funcionarios.Add(funcionario);
                _context.SaveChanges();

                var token = TokenService.GenerateToken(funcionario);

                return Created("", new { token, funcionario.Nome, funcionario.Cargo });
            }
            catch (Exception e)
            {
                return BadRequest(new { erro = e.Message });
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Funcionario funcionario)
        {
            try
            {
                ValidarEmail(funcionario.Email);
                ValidarSenha(funcionario.Senha);

                funcionario = _context.Funcionarios.Where(x => x.Email.ToLower() == funcionario.Email.ToLower() && x.Senha == funcionario.Senha).FirstOrDefault();

                if (funcionario is null)
                    return BadRequest(new { erro = "Email ou senha incorretos" });

                var token = TokenService.GenerateToken(funcionario);

                return Ok(new { token, funcionario.Nome, funcionario.Cargo });
            }
            catch (Exception e)
            {
                return BadRequest(new { erro = e.Message });
            }
        }

        [HttpGet("info")]
        [Authorize]
        public ActionResult FuncionarioInfo()
        {
            var funcionario = _context.Funcionarios.Where(x => x.Id == IdFuncionarioAutenticado).FirstOrDefault();

            funcionario.Email = "";
            funcionario.Senha = "";

            return Ok(funcionario);
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
