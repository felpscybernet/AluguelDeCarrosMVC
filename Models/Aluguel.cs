using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AluguelDeCarrosMVC.Models
{
    public class Aluguel
    {
        [Key]
        public int Id { get; set; }

        
        public int CarroId { get; set; }

        
        [ForeignKey("CarroId")]
        public Car? Car { get; set; } 

       
        public int ClienteId { get; set; }

        
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