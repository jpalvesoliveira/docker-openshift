using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Enums;

namespace Agora.Brokerage.Cadastro.DadosAcesso.API.Models
{
    /// <summary>
    /// Dados de acesso
    /// </summary>
    [DataContract]
    public class DadosAcesso
    {
        /// <summary>
        /// Tipo de pessoa cadastrada
        /// </summary>
        /// <value>Tipo de Pessoa - PF = 1, PJ = 2</value>
        [Required]
        [DataMember(Name = "Tipo Pessoa")]
        public ETipoPessoa TipoPessoa { get; set; }

        /// <summary>
        /// Nome completo
        /// </summary>
        [Required]
        [DataMember(Name = "NomeCompleto")]
        public string NomeCompleto { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [DataMember(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Data de nascimento
        /// </summary>
        [Required]
        [DataMember(Name = "DataNascimento")]
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Cpf/Cnpj
        /// </summary>
        [Required]
        [DataMember(Name = "CpfCnpj")]
        public long CpfCnpj { get; set; }

        /// <summary>
        /// Código do assessor
        /// </summary>
        [DataMember(Name = "Assessor")]
        public int Assessor { get; set; }
    }
}
