using Hackaton_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hackaton_API.Context
{
    public class ApiContext : DbContext
    {
        public DbSet<Funcionario> Funcionarios { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
