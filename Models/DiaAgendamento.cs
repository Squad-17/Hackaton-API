using System;

namespace Hackaton_API.Models
{
    public class DiaAgendamento
    {
        public DateTime Data { get; set; }
        public bool Disponivel { get; set; }

        public string DiaDaSemana
        {
            get
            {
                var diasDaSemana = new string[] { "Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado" };

                return diasDaSemana[(int)Data.DayOfWeek];
            }
        }

        public DiaAgendamento(DateTime data)
        {
            Data = data;
            Disponivel = true;
        }
    }
}
