namespace AluguelDeCarrosMVC.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string Placa { get; set; } = string.Empty;
        public decimal PrecoDiaria { get; set; }
        public bool Disponivel { get; set; }
    }
}