namespace Hackaton_API.Controllers
{
    internal interface RegraCapacidadeMaxima
    {
        public int aplicaRegra(int capacidade) => (int)(capacidade * 0.4);
    }
}