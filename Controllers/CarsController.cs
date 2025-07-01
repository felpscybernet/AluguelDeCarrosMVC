using Microsoft.AspNetCore.Mvc;
using AluguelDeCarrosMVC.Models;
using AluguelDeCarrosMVC.Repositories;
using Microsoft.AspNetCore.Authorization; // Adicione para proteger o controller

[Authorize] // Protege todo o controller, exigindo login
public class CarsController : Controller
{
    private readonly ICarRepository _carRepository;

    public CarsController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    // GET: Cars
    public async Task<IActionResult> Index()
    {
        var carros = await _carRepository.GetAllAsync();
        return View(carros);
    }

    // GET: Cars/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    // GET: Cars/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Cars/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Marca,Modelo,Ano,Placa,PrecoDiaria,Disponivel")] Car car)
    {
        if (ModelState.IsValid)
        {
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

            // Mensagem de sucesso para a notificação
            TempData["SuccessMessage"] = "Carro cadastrado com sucesso!";

            return RedirectToAction(nameof(Index));
        }
        return View(car);
    }

    // GET: Cars/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    // POST: Cars/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Marca,Modelo,Ano,Placa,PrecoDiaria,Disponivel")] Car car)
    {
        if (id != car.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _carRepository.Update(car);
            await _carRepository.SaveChangesAsync();

            // Mensagem de sucesso para a notificação
            TempData["SuccessMessage"] = "Dados do carro atualizados com sucesso!";

            return RedirectToAction(nameof(Index));
        }
        return View(car);
    }

    // GET: Cars/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var car = await _carRepository.GetByIdAsync(id.Value);
        if (car == null)
        {
            return NotFound();
        }

        return View(car);
    }

    // --- A CORREÇÃO PRINCIPAL ESTÁ AQUI ---
    // POST: Cars/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    // O nome do método foi corrigido de Create para DeleteConfirmed
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car != null)
        {
            _carRepository.Delete(car);
            await _carRepository.SaveChangesAsync();

            // Mensagem de sucesso para a notificação
            TempData["SuccessMessage"] = "Carro excluído com sucesso!";
        }

        return RedirectToAction(nameof(Index));
    }
}