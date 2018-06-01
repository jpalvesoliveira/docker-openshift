using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Agora.Brokerage.Cadastro.DadosAcesso.API.Models
{
    /// <summary>
    /// Assessores
    /// </summary>
    [DataContract]
    public class Assessor
    {
        /// <summary>
        /// Codigo do assessor
        /// </summary>
        [Required]
        [DataMember(Name = "Codigo")]
        public long Codigo { get; set; }

        /// <summary>
        /// Codigo do assessor
        /// </summary>
        [Required]
        [DataMember(Name = "Nome")]
        public string Nome { get; set; }
    }   
}
