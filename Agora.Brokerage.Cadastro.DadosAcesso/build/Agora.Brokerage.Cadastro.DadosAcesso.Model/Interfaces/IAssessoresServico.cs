using System.Threading.Tasks;
using System.Collections.Generic;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces
{
    public interface IAssessoresServico
    {
        Task<IEnumerable<Models.Assessor>> Listar();
    }
}
