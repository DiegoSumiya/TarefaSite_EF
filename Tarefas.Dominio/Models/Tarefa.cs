using System;
using System.Collections.Generic;


namespace Tarefas.Dominio.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public bool Notificacao { get; set; }
        public int IdCategoria { get; set; }
        public Usuario Usuario { get; set; }
        public List<Categoria> Categorias { get; set; }
       

        public Tarefa(int id, string descricao, DateTime data, bool notificacao, int idCategoria, Usuario usuario)
        {
            Id = id;
            Data = data;
            Notificacao = notificacao;
            IdCategoria = idCategoria;
            Usuario = usuario;
        }

        public Tarefa()
        {
        }
    }
}
