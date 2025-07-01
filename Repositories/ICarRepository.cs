using AluguelDeCarrosMVC.Models;

namespace AluguelDeCarrosMVC.Repositories
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task AddAsync(Car car);
        void Update(Car car);
        void Delete(Car car);
        Task<bool> SaveChangesAsync();
    }
}