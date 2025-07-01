using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AluguelDeCarrosMVC.Models;
using AluguelDeCarrosMVC.Repositories; // <<-- Usando repositórios

namespace AluguelDeCarrosMVC.Controllers
{
    public class AluguelsController : Controller
    {
        // O controller agora depende das interfaces dos repositórios
        private readonly IAluguelRepository _aluguelRepository;
        private readonly ICarRepository _carRepository;
        private readonly IClienteRepository _clienteRepository;

        public AluguelsController(IAluguelRepository aluguelRepository, ICarRepository carRepository, IClienteRepository clienteRepository)
        {
            _aluguelRepository = aluguelRepository;
            _carRepository = carRepository;
            _clienteRepository = clienteRepository;
        }

        // GET: Aluguels
        public async Task<IActionResult> Index()
        {
            var alugueis = await _aluguelRepository.GetAllAsync();
            return View(alugueis);
        }

        // GET: Aluguels/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var aluguel = await _aluguelRepository.GetByIdAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }
            return View(aluguel);
        }

        // GET: Aluguels/Create
        public async Task<IActionResult> Create()
        {
            // CORREÇÃO AQUI: Usamos o repositório e mostramos o "Modelo" do carro
            ViewData["CarroId"] = new SelectList(await _carRepository.GetAllAsync(), "Id", "Modelo");
            ViewData["ClienteId"] = new SelectList(await _clienteRepository.GetAllAsync(), "Id", "Nome");
            return View();
        }

        // POST: Aluguels/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarroId,ClienteId,DataRetirada,DataDevolucao")] Aluguel aluguel)
        {
            // Validações Manuais
            if (aluguel.DataDevolucao == null)
            {
                ModelState.AddModelError("DataDevolucao", "A data de devolução é obrigatória.");
            }
            else if (aluguel.DataDevolucao < aluguel.DataRetirada)
            {
                ModelState.AddModelError("DataDevolucao", "A data de devolução não pode ser anterior à data de retirada.");
            }

            if (ModelState.IsValid)
            {
                var carro = await _carRepository.GetByIdAsync(aluguel.CarroId);

                // COMBINAMOS AS DUAS VERIFICAÇÕES DE NULIDADE AQUI
                if (carro != null && aluguel.DataDevolucao != null)
                {
                    // Agora o compilador tem 100% de certeza que ambos não são nulos
                    var numeroDeDias = (aluguel.DataDevolucao.Value - aluguel.DataRetirada).Days;

                    if (numeroDeDias < 1)
                    {
                        numeroDeDias = 1;
                    }

                    aluguel.ValorTotal = numeroDeDias * carro.PrecoDiaria;

                    await _aluguelRepository.AddAsync(aluguel);
                    await _aluguelRepository.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Aluguel registrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

                // Se o carro for nulo, adicionamos o erro aqui
                if (carro == null)
                {
                    ModelState.AddModelError("CarroId", "O carro selecionado é inválido.");
                }
            }

            // Se o modelo for inválido, o código chegará aqui
            ViewData["CarroId"] = new SelectList(await _carRepository.GetAllAsync(), "Id", "Modelo", aluguel.CarroId);
            ViewData["ClienteId"] = new SelectList(await _clienteRepository.GetAllAsync(), "Id", "Nome", aluguel.ClienteId);
            return View(aluguel);
        }
    }
}