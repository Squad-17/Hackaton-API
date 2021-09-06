using System;

namespace Hackaton_API.Models
{
    public class DiaAgendamento
    {
        public DateTime Data { get; set; }
        public bool Disponivel { get; set; }

        public DiaAgendamento(DateTime data)
        {
            Data = data;
            Disponivel = true;
        }
    }
}
