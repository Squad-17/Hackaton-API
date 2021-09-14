using System;

namespace Hackaton_API.Models
{
    public class Agendamento
    {
        public int LocalId { get; set; }
        public Local Local { get; set; }
        public DateTime Data { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}