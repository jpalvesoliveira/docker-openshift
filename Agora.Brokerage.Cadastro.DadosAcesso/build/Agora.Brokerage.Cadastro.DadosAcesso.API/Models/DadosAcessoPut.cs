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
    public class DadosAcessoPut
    {
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
