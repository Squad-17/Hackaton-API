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

        private int IdFuncionarioAutenticado => int.Parse(User.FindFirst("Id").Value);

        [HttpGet]
        [Authorize]
        public ActionResult AgendamentosFeitos()
        {
            var agendamentos = _context.Agendamentos
                .Include(x => x.Local)
                .OrderBy(x => x.Data)
                .Where(x => x.FuncionarioId == IdFuncionarioAutenticado && x.Data >= DateTime.Today).ToList();

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

            var capacidadeMaxima = Convert.ToInt32(local.Capacidade * 0.4);

            var dias = new HashSet<DiaAgendamento>();
            var finaisDeSemana = new HashSet<DiaAgendamento>();

            for (int i = 1; i <= 7; i++)
            {
                var diaAgendamento = new DiaAgendamento(today.AddDays(i));
                dias.Add(diaAgendamento);
            };

            foreach (var dia in dias)
            {
                if (EFinalDeSemana(dia.Data))
                {
                    finaisDeSemana.Add(dia);
                    continue;
                }

                dia.Disponivel = DiaEstaDisponivel(dia.Data, capacidadeMaxima);
            }

            dias.ExceptWith(finaisDeSemana);

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

                if (EFinalDeSemana(agendamento.Data))
                    throw new Exception("Não é permitido fazer agendamento aos finais de semana");

                if (agendamento.LocalId == 0)
                    throw new Exception("Erro inesperado, tente novamente mais tarde");

                agendamento.FuncionarioId = IdFuncionarioAutenticado;
                var jaAgendouHoje = _context.Agendamentos.Where(x => x.FuncionarioId == agendamento.FuncionarioId && x.Data == agendamento.Data).FirstOrDefault() != null;

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

        [Authorize]
        [HttpDelete]
        public ActionResult Cancelar([FromBody] Agendamento agendamento)
        {
            if (agendamento.LocalId == 0)
                return BadRequest(new { erro = "Erro inesperado, tente novamente mais tarde." });

            if (agendamento.Data == DateTime.MinValue || agendamento.Data <= DateTime.Today)
                return BadRequest(new { erro = "Data inválida, tente novamente" });

            agendamento.FuncionarioId = IdFuncionarioAutenticado;

            _context.Agendamentos.Remove(agendamento);
            _context.SaveChanges();

            return Ok();
        }

        private bool DiaEstaDisponivel(DateTime data, int capacidadeMaxima)
        {
            if (EFeriado(data))
                return false;

            bool jaAgendouHoje = _context.Agendamentos.Where(x => x.FuncionarioId == IdFuncionarioAutenticado && x.Data == data).FirstOrDefault() != null;

            if (jaAgendouHoje)
                return false;

            int agendamentosCount = _context.Agendamentos.Where(x => x.Data == data).Count();

            if (agendamentosCount == capacidadeMaxima) return false;

            return true;
        }

        private bool EFinalDeSemana(DateTime data) => data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday;

        private bool EFeriado(DateTime data)
        {
            DateTime[] feriados = new DateTime[]
            {
                new DateTime(2021, 1, 1),
                new DateTime(2021, 2, 15),
                new DateTime(2021, 2, 16),
                new DateTime(2021, 2, 4),
                new DateTime(2021, 4, 21),
                new DateTime(2021, 5, 1),
                new DateTime(2021, 6, 3),
                new DateTime(2021, 9, 7),
                new DateTime(2021, 10, 12),
                new DateTime(2021, 11, 2),
                new DateTime(2021, 11, 15),
                new DateTime(2021, 12, 25)
            };

            return feriados.Any(x => x.Day == data.Day && x.Month == data.Month);
        }
    }
}