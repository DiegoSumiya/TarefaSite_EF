using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tarefas.Dominio.Models;
using Tarefas.Dominio.Repositorio;
using TarefaSiteEF.Data;

namespace Tarefas.Infra.Repositorio
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly TarefaSiteEFContext _context;

        public TarefaRepositorio(TarefaSiteEFContext context)
        {
            _context = context;
        }

        public List<Tarefa> Buscar(string email)
        {
            return _context.Tarefa.Where(obj => obj.Usuario.Email == email).ToList();
        }

        public Tarefa Buscar(int id, string email)
        {
            return _context.Tarefa.FirstOrDefault(obj => obj.Id == id && obj.Usuario.Email == email);
        }


        public void Inserir(Tarefa tarefa)
        {
            _context.Add(tarefa);
            _context.SaveChanges();
        }

        public void Atualizar(Tarefa tarefa, string email)
        {
            bool existeTarefa = _context.Tarefa.Any(t => t.Id == tarefa.Id);
            if(!existeTarefa)
            {
                throw new Exception("Tarefa não encontrada");
            }
            try
            {
                _context.Update(tarefa);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public void Excluir(int id, string email)
        {
            var tarefa = _context.Tarefa.FirstOrDefault(obj => obj.Id == id && obj.Usuario.Email == email);
            _context.Remove(tarefa);
            _context.SaveChanges();
        }
    }
}
