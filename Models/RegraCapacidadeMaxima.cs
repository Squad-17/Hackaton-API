namespace Hackaton_API.Models
{
    public interface RegraCapacidadeMaxima 
    {
        public int aplicaRegra(int capacidade) => (int)(capacidade * 0.4);
    }
}