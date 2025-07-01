using AluguelDeCarrosMVC.Data;
using AluguelDeCarrosMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AluguelDeCarrosMVC.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Car car)
        {
            // Usando o nome correto: Carros
            await _context.Carros.AddAsync(car);
        }

        public void Delete(Car car)
        {
            _context.Carros.Remove(car);
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Carros.ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _context.Carros.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Car car)
        {
            _context.Carros.Update(car);
        }
    }
}