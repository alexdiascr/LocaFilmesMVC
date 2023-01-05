using FluentValidation;

namespace LocaFilme.Business.Models.Validations
{
    public class LocacaoValidation : AbstractValidator<Locacao>
    {
        public LocacaoValidation()
        {
            RuleFor(f => f.DataCadastro)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.DataAtualizacao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.DataDevolucao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(f => f.Valor)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            //When(f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, () =>
            //{
            //    RuleFor(f => f.Documento.Length).Equal(CpfValidacao.TamanhoCpf)
            //        .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            //    RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(true)
            //        .WithMessage("O documento fornecido é inválido.");
            //});

            //When(f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, () =>
            //{
            //    RuleFor(f => f.Documento.Length).Equal(CnpjValidacao.TamanhoCnpj)
            //        .WithMessage("O campo Documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");
            //    RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(true)
            //        .WithMessage("O documento fornecido é inválido.");
            //});
        }
    }
}
