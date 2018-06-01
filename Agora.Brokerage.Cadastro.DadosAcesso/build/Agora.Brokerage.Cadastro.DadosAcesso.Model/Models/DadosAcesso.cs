using System;
using FluentValidation.Attributes;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Validations;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Enums;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Models
{
    [Validator(typeof(DadosAcessoModelValidator))]
    public class DadosAcesso
    {
        public ETipoPessoa TipoPessoa { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public long CpfCnpj { get; set; }
        public long Assessor { get; set; }
    }

}
