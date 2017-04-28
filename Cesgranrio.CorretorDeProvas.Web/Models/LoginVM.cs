using System.ComponentModel.DataAnnotations;

namespace Cesgranrio.CorretorDeProvas.Web.Models
{
    /// <summary>
    /// View model para login de usuário
    /// </summary>
    public class LoginVM
    {
        [Required(ErrorMessage="Por favor informe seu CPF")]
        [Display(Name = "CPF")]
        [MinLength(3,ErrorMessage ="O CPF deve conter ao menos 3 dígitos"), MaxLength(14, ErrorMessage="O CPF deve conter no máximo 14 dígitos")]
        public string CPF
        {
            get;
            set;
        }

        [Required(ErrorMessage="Por favor informe sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [MinLength(8, ErrorMessage = "A senha deve conter ao menos 8 caracteres"), MaxLength(50, ErrorMessage ="A senha deve conter no máximo 50 caracteres")]
        public string Senha
        {
            get;
            set;
        }
    }
}