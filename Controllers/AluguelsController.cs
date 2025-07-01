using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AluguelDeCarrosMVC.Models;
using AluguelDeCarrosMVC.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace AluguelDeCarrosMVC.Controllers
{
    [Authorize]
    public class AluguelsController : Controller
    {

        private readonly IAluguelRepository _aluguelRepository;
        private readonly ICarRepository _carRepository;
        private readonly IClienteRepository _clienteRepository;

        public AluguelsController(IAluguelRepository aluguelRepository, ICarRepository carRepository, IClienteRepository clienteRepository)
        {
            _aluguelRepository = aluguelRepository;
            _carRepository = carRepository;
            _clienteRepository = clienteRepository;
        }


        public async Task<IActionResult> Index()
        {
            var alugueis = await _aluguelRepository.GetAllAsync();
            return View(alugueis);
        }


        public async Task<IActionResult> Details(int id)
        {
            var aluguel = await _aluguelRepository.GetByIdAsync(id);
            if (aluguel == null)
            {
                return NotFound();
            }
            return View(aluguel);
        }


        public async Task<IActionResult> Create()
        {

            ViewData["CarroId"] = new SelectList(await _carRepository.GetAllAsync(), "Id", "Modelo");
            ViewData["ClienteId"] = new SelectList(await _clienteRepository.GetAllAsync(), "Id", "Nome");
            return View();
        }



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
        
            else
            {
                // Só verificamos a disponibilidade se as datas forem válidas
                bool carroJaAlugado = await _aluguelRepository.CarroJaAlugadoNoPeriodo(aluguel.CarroId, aluguel.DataRetirada, aluguel.DataDevolucao.Value);
                if (carroJaAlugado)
                {
                    // Adiciona um erro geral se o carro já estiver alugado
                    ModelState.AddModelError("", "Este carro já está reservado para o período selecionado. Por favor, escolha outras datas ou outro veículo.");
                }
            }

            if (ModelState.IsValid)
            {
                var carro = await _carRepository.GetByIdAsync(aluguel.CarroId);

                if (carro != null && aluguel.DataDevolucao != null)
                {
                    var numeroDeDias = (aluguel.DataDevolucao.Value - aluguel.DataRetirada).Days;
                    if (numeroDeDias < 1) numeroDeDias = 1;

                    aluguel.ValorTotal = numeroDeDias * carro.PrecoDiaria;

                    await _aluguelRepository.AddAsync(aluguel);
                    await _aluguelRepository.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Aluguel registrado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }

                if (carro == null)
                {
                    ModelState.AddModelError("CarroId", "O carro selecionado é inválido.");
                }
            }

            // Se chegar aqui, alguma validação falhou. Recarrega a página com os erros.
            ViewData["CarroId"] = new SelectList(await _carRepository.GetAllAsync(), "Id", "Modelo", aluguel.CarroId);
            ViewData["ClienteId"] = new SelectList(await _clienteRepository.GetAllAsync(), "Id", "Nome", aluguel.ClienteId);
            return View(aluguel);
        }
    }
}