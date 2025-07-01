using Microsoft.AspNetCore.Mvc;
using AluguelDeCarrosMVC.Models;
using AluguelDeCarrosMVC.Repositories;
using Microsoft.AspNetCore.Authorization; 

namespace AluguelDeCarrosMVC.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClientesController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return View(clientes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create([Bind("Id,Nome,CPF,Telefone,Email")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteRepository.AddAsync(cliente);
                await _clienteRepository.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cliente atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
     
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPF,Telefone,Email")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _clienteRepository.Update(cliente);
                await _clienteRepository.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cliente excluído com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente != null)
            {
                _clienteRepository.Delete(cliente);
                await _clienteRepository.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}