using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;
using System.Threading.Tasks;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Services
{

    public class DadosAcessoServico : ServicoBase<Model.Models.DadosAcesso>, IDadosAcessoServico
    {
        public DadosAcessoServico(IDadosAcessoRepositorio repo) : base(repo)
        {

        }

        public async Task<bool> Create(Model.Models.DadosAcesso DadosAcesso)
        {
            return await ((IDadosAcessoRepositorio)_repositorio).Create(DadosAcesso);
        }

        public Task<Model.Models.DadosAcesso> Read(long pCPF)
        {
            return ((IDadosAcessoRepositorio)_repositorio).Read(pCPF);
        }

        public async Task<bool> Update(Model.Models.DadosAcesso DadosAcesso)
        {
            return await ((IDadosAcessoRepositorio)_repositorio).Update(DadosAcesso);

        }

        public async Task<bool> Delete(long CPF)
        {
            return await ((IDadosAcessoRepositorio)_repositorio).Delete(CPF);

        }

    }
}
