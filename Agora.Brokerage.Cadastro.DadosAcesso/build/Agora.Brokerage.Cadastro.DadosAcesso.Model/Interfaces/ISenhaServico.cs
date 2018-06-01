using System.Threading.Tasks;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces
{
    public interface ISenhaServico: IServicoBase<Models.Senha>
    {
        Task<bool> Create(Models.Senha senha);
    }
}
