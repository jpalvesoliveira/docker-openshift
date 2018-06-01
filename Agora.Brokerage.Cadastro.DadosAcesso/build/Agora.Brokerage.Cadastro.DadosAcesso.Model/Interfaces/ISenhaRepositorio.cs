using System.Threading.Tasks;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Models;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces
{
    public interface ISenhaRepositorio : IRepositorioBase<Models.Senha>
    {
        Task<bool> Create(Senha senha);
    }
}
