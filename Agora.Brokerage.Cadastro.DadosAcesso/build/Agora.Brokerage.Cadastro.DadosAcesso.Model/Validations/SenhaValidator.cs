using System;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Resources;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Validations;
using FluentValidation;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Validations
{
    public class SenhaValidator : AbstractValidator<Models.Senha>
    {
        public SenhaValidator()
        {
            CpfValidator();
            PwdValidator();
        }

        private void PwdValidator()
        {
            RuleFor(c => c.Pwd)
              .NotNull()
              .NotEmpty().WithMessage(Mensagem.GetMensagem("SenhaNuloOuVazio"));
            
            //RuleFor(c => c.Pwd)
             //.MaximumLength(6).WithMessage(Mensagem.GetMensagem("SenhaMaxDigitos"));

            RuleFor(c => c.Pwd)
               .Matches("^[0-9]{6}$").WithMessage(Mensagem.GetMensagem("SenhaInvalida"));

        }

        private void CpfValidator()
        {
            RuleFor(c => c.CPF)
              .NotNull()
              .NotEmpty().WithMessage(Mensagem.GetMensagem("CpfNuloOuVazio"))
              .Must(HelperValidations.IsValidCPF).WithMessage(Mensagem.GetMensagem("CpfInvalido"));
        }
    }
}
