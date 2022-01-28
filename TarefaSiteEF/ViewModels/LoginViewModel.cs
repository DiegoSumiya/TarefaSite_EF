using System.ComponentModel.DataAnnotations;

namespace TarefaSiteEF.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Senha { get; set; }
    }
}
