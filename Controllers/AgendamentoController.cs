using Hackaton_API.Context;
using Hackaton_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hackaton_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly ApiContext _context;
        public AgendamentoController(ApiContext context) => _context = context;

        [Authorize]
        [HttpGet("disponiveis/{localId}")]
        public ActionResult DiasDisponiveis([FromRoute] int localId)
        {
            var today = DateTime.Today;
            var local = _context.Locais.Where(x => x.Id == localId).FirstOrDefault();

            if (local is null)
                return BadRequest(new { erro = "Local inválido, tente novamente mais tarde" });

            var agendamentosPermitidos = local.Capacidade * 0.4;

            var dias = new List<DiaAgendamento>();

            for (int i = 1; i <= 7; i++)
            {
                var diaAgendamento = new DiaAgendamento(today.AddDays(i));
                dias.Add(diaAgendamento);
            };

            foreach (var dia in dias)
            {
                if (dia.Data.DayOfWeek == DayOfWeek.Saturday || dia.Data.DayOfWeek == DayOfWeek.Sunday)
                {
                    dia.Disponivel = false;
                    continue;
                }

                int funcionarioId = int.Parse(User.FindFirst("Id").Value);

                bool jaAgendouHoje = _context.Agendamentos.Where(x => x.FuncionarioId == funcionarioId && x.Data == dia.Data).FirstOrDefault() != null;

                if (jaAgendouHoje)
                {
                    dia.Disponivel = false;
                    continue;
                };

                int agendamentosCount = _context.Agendamentos.Where(x => x.Data == dia.Data).Count();

                if (agendamentosCount == agendamentosPermitidos) dia.Disponivel = false;
            }

            return Ok(dias);
        }
    }
}