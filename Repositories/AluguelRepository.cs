using AluguelDeCarrosMVC.Data;
using AluguelDeCarrosMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AluguelDeCarrosMVC.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        private readonly ApplicationDbContext _context;

        public AluguelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Aluguel aluguel)
        {
            await _context.Alugueis.AddAsync(aluguel);
        }

        public void Delete(Aluguel aluguel)
        {
            _context.Alugueis.Remove(aluguel);
        }

        // Este método é um pouco diferente, pois queremos carregar os dados
        // do Carro e do Cliente junto com o Aluguel.
        public async Task<IEnumerable<Aluguel>> GetAllAsync()
        {
            return await _context.Alugueis
                .Include(a => a.Car)       
                .Include(a => a.Cliente)   // Inclui os dados do Cliente relacionado
                .ToListAsync();
        }

        public async Task<Aluguel> GetByIdAsync(int id)
        {
            return await _context.Alugueis
                .Include(a => a.Car)
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Aluguel aluguel)
        {
            _context.Alugueis.Update(aluguel);
        }
        // Em Repositories/AluguelRepository.cs
        public async Task<bool> CarroJaAlugadoNoPeriodo(int carroId, DateTime dataRetirada, DateTime dataDevolucao)
        {
            // Verifica se existe algum aluguel para o mesmo carro que comece antes da data de devolução
            // e termine depois da data de retirada.
            return await _context.Alugueis.AnyAsync(a =>
                a.CarroId == carroId &&
                a.DataRetirada < dataDevolucao &&
                a.DataDevolucao > dataRetirada);
        }
    }
}