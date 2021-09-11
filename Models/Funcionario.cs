using Hackaton_API.Enums;
using System.Text.Json.Serialization;

namespace Hackaton_API.Models
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }
        public string Senha { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AvatarDePerfil Avatar { get; set; }
    }
}
