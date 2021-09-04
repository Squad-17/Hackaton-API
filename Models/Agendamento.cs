using System;

namespace Hackaton_API.Models
{
    public class Agendamento
    {
        public int IdLocal { get; set; }
        public Local Local { get; set; }
        public DateTime Data { get; set; }
        public int IdFuncionario { get; set; }
        public Funcionario Funcionario { get; set; }
    }
}