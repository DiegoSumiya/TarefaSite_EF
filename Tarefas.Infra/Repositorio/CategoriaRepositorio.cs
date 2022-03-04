using System.Collections.Generic;
using Tarefas.Dominio.Models;
using Tarefas.Dominio.Repositorio;
using TarefaSiteEF.Data;
using System.Linq;

namespace Tarefas.Infra.Repositorio
{
    public class CategoriaRepositorio :ICategoriaRepositorio
    {
        private readonly TarefaSiteEFContext _context;

        public CategoriaRepositorio(TarefaSiteEFContext context)
        {
            _context = context;
        }

        public List<Categoria> Buscar()
        {
            return _context.Categoria.ToList();
        }
    }
}
