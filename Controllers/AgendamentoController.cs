using Hackaton_API.Context;
using Hackaton_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        [Authorize]
        public ActionResult AgendamentosFeitos()
        {
            int funcionarioId = int.Parse(User.FindFirst("Id").Value);

            var agendamentos = _context.Agendamentos.Include(x => x.Local).Where(x => x.FuncionarioId == funcionarioId && x.Data >= DateTime.Today).ToList();

            return Ok(agendamentos);
        }

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

        [HttpPost]
        [Authorize]
        public ActionResult Agendar([FromBody] Agendamento agendamento)
        {
            try
            {
                if (agendamento.Data <= DateTime.Today)
                    throw new Exception("Data de agendamento deve ser no mínimo amanhã");

                if (agendamento.Data.DayOfWeek == DayOfWeek.Saturday || agendamento.Data.DayOfWeek == DayOfWeek.Sunday)
                    throw new Exception("Não é permitido fazer agendamento aos finais de semana");

                if (agendamento.FuncionarioId == 0 || agendamento.LocalId == 0)
                    throw new Exception("Erro inesperado, tente novamente mais tarde");

                var funcionarioId = int.Parse(User.FindFirst("Id").Value);
                var jaAgendouHoje = _context.Agendamentos.Where(x => x.FuncionarioId == funcionarioId && x.Data == agendamento.Data).FirstOrDefault() != null;

                if (jaAgendouHoje)
                    throw new Exception("Não é permitido fazer mais de um agendamento por dia");

                _context.Agendamentos.Add(agendamento);
                _context.SaveChanges();

                return Created("", agendamento);
            }
            catch (Exception e)
            {
                return BadRequest(new { erro = e.Message });
            }
        }
    }
}