using System;
using System.Collections.Generic;
using System.Linq;
using Tarefas.Dominio.Models;
using TarefaSiteEF.Data;
using Tarefas.Dominio.Repositorio;

namespace Tarefas.Infra.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly TarefaSiteEFContext _context;

        public UsuarioRepositorio(TarefaSiteEFContext context)
        {
            _context = context;
        }

        public Usuario Buscar(string email)
        {
            return _context.Usuario.Find(email);
        }

        public void Inserir(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
        }

        public List<Usuario> Buscar()
        {
            return _context.Usuario.ToList();
        }
    }
}
