using System;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Resources;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Validations;
using FluentValidation;

/// <summary>
/// Objetivo: Agrupar validações do cadastro referentes ao passo "Dados de Acesso"
/// </summary>
namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Validations
{
    public class DadosAcessoModelValidator : AbstractValidator<Models.DadosAcesso>
    {
        public DadosAcessoModelValidator()
        {
            Initialize();
        }

        private void Initialize()
        {
                NomeCompletoValidator();
                EmailValidator();
                DataNascimentoValidator();
                CPFValidator();
        }

        #region Tests

        private void NomeCompletoValidator()
        {
            RuleFor(c => c.NomeCompleto)
                .NotNull()
                .NotEmpty().WithMessage(Mensagem.GetMensagem("NomeCompletoNuloOuVazio"))
                .MinimumLength(3).WithMessage(Mensagem.GetMensagem("NomeCompletoQuantidadeCaracteres"))
                .MaximumLength(60).WithMessage(Mensagem.GetMensagem("NomeCompletoQuantidaMaximaCaracteres"));
        }


        private void EmailValidator()
        {
            HelperValidations h = new HelperValidations();

            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty().WithMessage(Mensagem.GetMensagem("EmailNuloOuVazio"))
                .MaximumLength(255).WithMessage(Mensagem.GetMensagem("EmailQuantidadeMaximaCaracteres"))
                .Must(HelperValidations.ValidaEmail).WithMessage(Mensagem.GetMensagem("EmailInvalido"));
        }

        private void DataNascimentoValidator()
        {
            //TODO: Conferir regras de negocio para todos as propriedades 
            HelperValidations h = new HelperValidations();
            RuleFor(c => c.DataNascimento)
                .NotNull()
                .NotEmpty().WithMessage(Mensagem.GetMensagem("DataNascimentoNula"))
               .Must(h.DataFormatoValido).WithMessage(Mensagem.GetMensagem("DataNascimentoInvalida"))
               .LessThan(DateTime.Now.Date).WithMessage(Mensagem.GetMensagem("DataNascimentoMaiorDataCorrente"));
        }

        private void CPFValidator()
        {
            HelperValidations h = new HelperValidations();
            //TODO: Conferir regras de negocio para todos as propriedades 

            RuleFor(c => c.CpfCnpj)
                .NotEqual(0).WithMessage(Mensagem.GetMensagem("CpfNuloOuVazio"))
                .Must(HelperValidations.IsValidCPF).WithMessage(Mensagem.GetMensagem("CpfInvalido"));

        }
        #endregion

    }
}
