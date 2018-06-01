using System.Threading.Tasks;

/// <summary>
/// Objetivo: Agrupar interfaces do cadastro referentes ao passo "Dados de Acesso"
/// </summary>
namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces
{
    public interface IDadosAcessoRepositorio : IRepositorioBase<Models.DadosAcesso>
    {
        Task<bool> Create(Models.DadosAcesso dadosDeAcesso);
        Task<Models.DadosAcesso> Read(long pCPF);
        Task<bool> Update(Models.DadosAcesso dadosDeAcesso);
        Task<bool> Delete(long pCPF);
    }
}
