using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AluguelDeCarrosMVC.Models
{
    public class Aluguel
    {
        [Key]
        public int Id { get; set; }

        // Chave estrangeira para o Carro
        public int CarroId { get; set; }

        // Propriedade de navegação para o Carro (DEVE SE CHAMAR 'Car')
        [ForeignKey("CarroId")]
        public Car? Car { get; set; } // <<-- O NOME AQUI É 'Car'

        // Chave estrangeira para o Cliente
        public int ClienteId { get; set; }

        // Propriedade de navegação para o Cliente
        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }

        [Display(Name = "Data da Retirada")]
        public DateTime DataRetirada { get; set; }

        [Display(Name = "Data da Devolução")]
        public DateTime? DataDevolucao { get; set; }

        [Display(Name = "Valor Total")]
        public decimal? ValorTotal { get; set; }
    }
}