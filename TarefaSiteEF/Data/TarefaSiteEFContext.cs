using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<Tarefas.Dominio.Models.Usuario> Usuario { get; set; }
    }
}
