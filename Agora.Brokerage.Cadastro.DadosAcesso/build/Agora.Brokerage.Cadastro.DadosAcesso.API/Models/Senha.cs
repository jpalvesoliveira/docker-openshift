using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Agora.Brokerage.Cadastro.DadosAcesso.API.Models
{
    /// <summary>
    /// Modelo para gravação de senhas
    /// </summary>
    [DataContract]
    public class Senha
    {
        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [DataMember(Name = "CPF")]
        public long Cpf { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        [Required]
        [DataMember(Name = "Senha")]
        public string senha { get; set; }
    }
}
