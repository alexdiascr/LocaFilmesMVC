using AutoMapper;
using LocaFilme.App.Extensions;
using LocaFilme.App.ViewModels;
using LocaFilme.Business.Interfaces;
using LocaFilme.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocaFilme.App.Controllers
{
    [Authorize]
    public class LocacoesController : BaseController
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ILocacaoService _locacaoService;
        private readonly IMapper _mapper;

        public LocacoesController(ILocacaoRepository locacaoRepository,
                                      IMapper mapper,
                                      ILocacaoService locacaoService,
                                      IClienteRepository clienteRepository,
                                      INotificador notificador) : base(notificador)
        {
            _locacaoRepository = locacaoRepository;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _locacaoService = locacaoService;
        }

        //[AllowAnonymous]
        [Route("lista-de-locacoes")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<LocacaoViewModel>>(await _locacaoRepository.ObterTodos()));
        }

        //[AllowAnonymous]
        [Route("dados-do-locacao/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var locacaoViewModel = await ObterFornecedorEndereco(id);

            if (locacaoViewModel == null)
            {
                return NotFound();
            }

            return View(locacaoViewModel);
        }

        //[ClaimsAuthorize("Fornecedor", "Adicionar")]
        [Route("novo-locacao")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Locacao", "Adicionar")]
        [Route("novo-locacao")]
        [HttpPost]
        public async Task<IActionResult> Create(LocacaoViewModel locacaoViewModel)
        {
            if (!ModelState.IsValid) return View(locacaoViewModel);

            var locacao = _mapper.Map<Locacao>(locacaoViewModel);
            await _locacaoService.Adicionar(locacao);

            if (!OperacaoValida()) return View(locacaoViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Locacao", "Editar")]
        [Route("editar-locacao/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var locacaoViewModel = await ObterFornecedorProdutosEndereco(id);

            if (locacaoViewModel == null)
            {
                return NotFound();
            }

            return View(locacaoViewModel);
        }

        [ClaimsAuthorize("Locacao", "Editar")]
        [Route("editar-locacao/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, LocacaoViewModel locacaoViewModel)
        {
            if (id != locacaoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(locacaoViewModel);

            var locacao = _mapper.Map<Locacao>(locacaoViewModel);
            await _locacaoService.Atualizar(locacao);

            if (!OperacaoValida()) return View(await ObterFornecedorProdutosEndereco(id));

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Locacao", "Excluir")]
        [Route("excluir-locacao/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await ObterFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [ClaimsAuthorize("Locacao", "Excluir")]
        [Route("excluir-locacao/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return NotFound();

            await _locacaoService.Remover(id);

            if (!OperacaoValida()) return View(fornecedor);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("obter-cliente-locacao/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesEndereco", fornecedor);
        }

        [ClaimsAuthorize("Locacao", "Editar")]
        [Route("atualizar-cliente-locacao/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var locacao = await ObterFornecedorEndereco(id);

            if (locacao == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEndereco", new LocacaoViewModel { Cliente = locacao.Cliente });
        }

        //[ClaimsAuthorize("Fornecedor", "Editar")]
        //[Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]

        public async Task<IActionResult> AtualizarEndereco(LocacaoViewModel locacaoViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", locacaoViewModel);

            await _locacaoService.AtualizarCliente(_mapper.Map<Cliente>(locacaoViewModel.Cliente));

            //if (!OperacaoValida()) return PartialView("_AtualizarEndereco", fornecedorViewModel);

            var url = Url.Action("ObterEndereco", "Fornecedores", new { id = locacaoViewModel.Cliente.LocacaoId });
            return Json(new { success = true, url });
        }

        private async Task<LocacaoViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<LocacaoViewModel>(await _locacaoRepository.ObterLocacaoCliente(id));
        }

        private async Task<LocacaoViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<LocacaoViewModel>(await _locacaoRepository.ObterLocacaoFilmesCliente(id));
        }
    }
}
