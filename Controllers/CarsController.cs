using Microsoft.AspNetCore.Mvc;
using AluguelDeCarrosMVC.Models;
using AluguelDeCarrosMVC.Repositories;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class CarsController : Controller
{
    private readonly ICarRepository _carRepository;

    public CarsController(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

   
    public async Task<IActionResult> Index()
    {
        var carros = await _carRepository.GetAllAsync();
        return View(carros);
    }

    
    public async Task<IActionResult> Details(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Marca,Modelo,Ano,Placa,PrecoDiaria,Disponivel")] Car car)
    {
        if (ModelState.IsValid)
        {
            await _carRepository.AddAsync(car);
            await _carRepository.SaveChangesAsync();

           
            TempData["SuccessMessage"] = "Carro cadastrado com sucesso!";

            return RedirectToAction(nameof(Index));
        }
        return View(car);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    
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

            
            TempData["SuccessMessage"] = "Dados do carro atualizados com sucesso!";

            return RedirectToAction(nameof(Index));
        }
        return View(car);
    }

    
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

    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var car = await _carRepository.GetByIdAsync(id);
        if (car != null)
        {
            _carRepository.Delete(car);
            await _carRepository.SaveChangesAsync();

            
            TempData["SuccessMessage"] = "Carro excluído com sucesso!";
        }

        return RedirectToAction(nameof(Index));
    }
}