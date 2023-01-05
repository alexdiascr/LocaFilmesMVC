using AutoMapper;
using LocaFilme.App.ViewModels;
using LocaFilme.Business.Interfaces;
using LocaFilme.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocaFilme.App.Controllers
{
    public class FilmesController : BaseController
    {
        private readonly IFilmeRepository _filmerepository;
        private readonly ILocacaoRepository _locacaorepository;
        private readonly IMapper _mapper;

        public FilmesController(IFilmeRepository filmeRepository,
                                    ILocacaoRepository locacaorepository,
                                    IMapper mapper,
                                    INotificador notificador) : base(notificador)
        {
            _filmerepository = filmeRepository;
            _locacaorepository = locacaorepository;
            _mapper = mapper;
        }

        [Route("lista-de-filmes")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FilmeViewModel>>(await _filmerepository.ObterFilmesPorLocacoes()));
        }

        [Route("dados-do-filme/{id:guid}")]
        public async Task <IActionResult> Details(Guid id)
        {
            var filmeViewModel = await ObterFilme(id);

            if (filmeViewModel == null)            
                return NotFound();

            return View(filmeViewModel);
        }

        [Route("novo-filme")]
        public async Task<IActionResult> Create()
        {
            var filmeViewModel = await PopularFornecedores(new FilmeViewModel());

            return View(filmeViewModel);
        }

        [Route("novo-filme")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FilmeViewModel filmeViewModel)
        {
            filmeViewModel = await PopularFornecedores(filmeViewModel);
            if (!ModelState.IsValid) return View(filmeViewModel);

            var imgPrefixo = Guid.NewGuid() + "_";

            if (!await UploadArquivo(filmeViewModel.ImagemUpload, imgPrefixo))
                return View(filmeViewModel);

            filmeViewModel.Imagem = imgPrefixo + filmeViewModel.ImagemUpload.FileName;
            await _filmerepository.Adicionar(_mapper.Map<Filme>(filmeViewModel));

            return RedirectToAction("index");
        }


        [Route("editar-filme/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var filmeViewModel = await ObterFilme(id);

            if (filmeViewModel == null)
                return NotFound();

            return View(filmeViewModel);
        }

        [Route("editar-filme/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FilmeViewModel filmeViewModel)
        {
            if (id != filmeViewModel.Id) return NotFound();

            var filmeAtualizacao = await ObterFilme(id);
            filmeViewModel.Locacao = filmeAtualizacao.Locacao;
            filmeViewModel.Imagem = filmeAtualizacao.Imagem;

            if (!ModelState.IsValid) return View(filmeViewModel);

            if(filmeViewModel.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "-";
                if (!await UploadArquivo(filmeViewModel.ImagemUpload, imgPrefixo))                
                    return View(filmeViewModel);

                filmeAtualizacao.Imagem = imgPrefixo + filmeViewModel.ImagemUpload.FileName;
            }

            filmeAtualizacao.Nome = filmeViewModel.Nome;
            filmeAtualizacao.Descricao = filmeViewModel.Descricao;
            filmeAtualizacao.Valor = filmeViewModel.Valor;
            filmeAtualizacao.Ativo = filmeViewModel.Ativo;

            await _filmerepository.Atualizar(_mapper.Map<Filme>(filmeAtualizacao));

            return RedirectToAction("index");
        }


        [Route("excluir-filme/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var filmeViewModel = await ObterFilme(id);

            if (filmeViewModel == null)
                return NotFound();

            return View(filmeViewModel);
        }

        [Route("excluir-filme/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var filme = await ObterFilme(id);

            if (filme == null)
                return NotFound();

            await _filmerepository.Remover(id);

            return RedirectToAction("Index");
        }



        private async Task<FilmeViewModel> ObterFilme(Guid id)
        {
            var filme = _mapper.Map<FilmeViewModel>(await _filmerepository.ObterFilmeLocacao(id));
            filme.Locacoes = _mapper.Map<IEnumerable<LocacaoViewModel>>(await _locacaorepository.ObterTodos());
            return filme;
        }

        private async Task<FilmeViewModel> PopularFornecedores(FilmeViewModel filme)
        {
            filme.Locacoes = _mapper.Map<IEnumerable<LocacaoViewModel>>(await _locacaorepository.ObterTodos());

            return filme;
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "já exite um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
