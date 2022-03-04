using Microsoft.EntityFrameworkCore;
using Tarefas.Dominio.Models;

namespace TarefaSiteEF.Data
{
    public class TarefaSiteEFContext : DbContext
    {
        public TarefaSiteEFContext (DbContextOptions<TarefaSiteEFContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
    }
}


