using System.Collections.Generic;
using System.Threading.Tasks;
using Tarefas.Dominio.Models;

namespace Tarefas.Dominio.Repositorio
{
    public interface IUsuarioRepositorio
    {
        public Usuario Buscar(string email);
        public void Inserir(Usuario usuario);
        public List<Usuario> Buscar();
        

    }
}
