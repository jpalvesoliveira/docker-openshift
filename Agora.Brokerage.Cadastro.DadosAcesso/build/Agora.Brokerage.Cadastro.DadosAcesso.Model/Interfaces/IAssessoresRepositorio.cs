using System.Threading.Tasks;
using System.Collections.Generic;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces
{
    public interface IAssessoresRepositorio : IRepositorioBase<Models.Assessor>
    {
        Task<IEnumerable<Models.Assessor>> Listar();        
    }
}
