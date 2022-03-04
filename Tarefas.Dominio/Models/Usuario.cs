using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tarefas.Dominio.Models
{
    public class Usuario
    {
        [Key]
        public string Email { get; set; }
        public string Nome{ get; set; }
       
        public string Senha { get; set; }
        public List<Tarefa> Tarefas { get; set; }

        public Usuario(string email, string nome,  string senha)
        {
            Email = email;
            Nome = nome;
            Senha = senha;
        }


    }
}
