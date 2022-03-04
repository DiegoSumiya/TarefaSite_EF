using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarefas.Dominio.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Tarefa Tarefa { get; set; }

        public Categoria(int id, string descricao)
        {
            Id = id;
            Descricao = descricao;
        }
    }
}
