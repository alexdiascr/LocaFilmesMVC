using LocaFilme.Business.Interfaces;
using LocaFilme.Business.Models;
using LocaFilme.Business.Models.Validations;

namespace LocaFilme.Business.Services
{
    public class LocacaoService : BaseService, ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IClienteRepository _enderecoRepository;

        public LocacaoService(ILocacaoRepository locacaoRepository,
                                  IClienteRepository enderecoRepository,
                                  INotificador notificador) : base(notificador)
        {
            _locacaoRepository = locacaoRepository;
            _enderecoRepository = enderecoRepository;
        }


        public async Task Adicionar(Locacao locacao)
        {
            if (!ExecutarValidacao(new LocacaoValidation(), locacao)
                || !ExecutarValidacao(new ClienteValidation(), locacao.Cliente)) return;

            //if (_fornecedorRepository.Buscar(f => f.Locacao == Locacao.Documento).Result.Any())
            //{
            //    Notificar("Já existe um fornecedor com este documento infomado.");
            //    return;
            //}

            await _locacaoRepository.Adicionar(locacao);
        }

        public async Task Atualizar(Locacao locacao)
        {
            if (!ExecutarValidacao(new LocacaoValidation(), locacao)) return;

            if (_locacaoRepository.Buscar(f => f.Id != locacao.Id).Result.Any())
            {
                Notificar("Locacao já existente");
                return;
            }

            await _locacaoRepository.Atualizar(locacao);
        }

        public async Task AtualizarCliente(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), cliente)) return;

            await _enderecoRepository.Atualizar(cliente);
        }

        public async Task Remover(Guid id)
        {
            if (_locacaoRepository.ObterLocacaoFilmesCliente(id).Result.Produtos.Any())
            {
                Notificar("A locacao possui filmes cadastrados!");
                return;
            }

            var cliente = await _enderecoRepository.ObterClientePorLocacao(id);

            if (cliente != null)
            {
                await _enderecoRepository.Remover(cliente.Id);
            }

            await _locacaoRepository.Remover(id);
        }

        public void Dispose()
        {
            _locacaoRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }


}
