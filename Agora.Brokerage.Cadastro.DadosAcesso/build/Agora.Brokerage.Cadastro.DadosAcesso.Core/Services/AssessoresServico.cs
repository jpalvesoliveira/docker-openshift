using System.Threading.Tasks;
using System.Collections.Generic;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Services
{
    public class AssessoresServico : ServicoBase<Model.Models.Assessor>, IAssessoresServico
    {

        public AssessoresServico(IAssessoresRepositorio repo) : base(repo)
        {

        }

        public Task<IEnumerable<Model.Models.Assessor>> Listar()
        {
            return ((IAssessoresRepositorio)_repositorio).Listar();
        }
        
    }
}
