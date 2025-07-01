using AluguelDeCarrosMVC.Models;
using AluguelDeCarrosMVC.Repositories; // <<-- Adicione este using
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AluguelDeCarrosMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarRepository _carRepository;

        // Pede o repositório de carros para funcionar
        public HomeController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        // Ação Index agora busca os carros
        public async Task<IActionResult> Index()
        {
            // Busca todos os carros e pega os 3 primeiros para destacar
            var carrosEmDestaque = (await _carRepository.GetAllAsync()).Take(3);
            return View(carrosEmDestaque);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}