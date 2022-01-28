using System;
using System.Collections.Generic;


namespace Tarefas.Dominio.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public bool Notificacao { get; set; }
        public int IdCategoria { get; set; }
        public string EmailUsuario { get; set; }

        public Tarefa(int id, DateTime data, bool notificacao, int idCategoria, string emailUsuario)
        {
            Id = id;
            Data = data;
            Notificacao = notificacao;
            IdCategoria = idCategoria;
            EmailUsuario = emailUsuario;
        }
    }
}
