using System.Threading.Tasks;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces
{
    public interface IDadosAcessoServico : IServicoBase<Models.DadosAcesso>
    {
        Task<bool> Create(Models.DadosAcesso DadosAcesso);
        Task<Models.DadosAcesso> Read(long pCPF);
        Task<bool> Update(Models.DadosAcesso DadosAcesso);
        Task<bool> Delete(long pCPF);
    }
}
