namespace AluguelDeCarrosMVC.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public decimal PrecoDiaria { get; set; }
        public bool Disponivel { get; set; }
    }
}