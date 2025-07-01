using System.ComponentModel.DataAnnotations;

namespace AluguelDeCarrosMVC.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        
        public string CPF { get; set; } = string.Empty;

        public string? Telefone { get; set; }

        [Display(Name = "E-mail")]
        public string? Email { get; set; }
    }
}