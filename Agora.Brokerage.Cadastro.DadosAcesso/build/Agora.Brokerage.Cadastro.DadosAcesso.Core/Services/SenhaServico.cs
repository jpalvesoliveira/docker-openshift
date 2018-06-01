using System.Threading.Tasks;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Services
{
    public class SenhaServico : ServicoBase<Model.Models.Senha>, ISenhaServico
    {
        public SenhaServico(ISenhaRepositorio repo) : base(repo)
        {
        }

        public async Task<bool> Create(Model.Models.Senha senha)
        {
            return await ((ISenhaRepositorio)_repositorio).Create(senha);
        }
    }
}
