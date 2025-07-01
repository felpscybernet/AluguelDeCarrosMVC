using AluguelDeCarrosMVC.Models;

namespace AluguelDeCarrosMVC.Repositories
{
    public interface IAluguelRepository
    {
        Task<bool> CarroJaAlugadoNoPeriodo(int carroId, DateTime dataRetirada, DateTime dataDevolucao);
        Task<IEnumerable<Aluguel>> GetAllAsync();
        Task<Aluguel> GetByIdAsync(int id);
        Task AddAsync(Aluguel aluguel);
        void Update(Aluguel aluguel);
        void Delete(Aluguel aluguel);
        Task<bool> SaveChangesAsync();
    }
}