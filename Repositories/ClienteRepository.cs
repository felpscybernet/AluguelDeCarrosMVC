using AluguelDeCarrosMVC.Data;
using AluguelDeCarrosMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AluguelDeCarrosMVC.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
        }

        public void Delete(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
        }

        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
        }
    }
}