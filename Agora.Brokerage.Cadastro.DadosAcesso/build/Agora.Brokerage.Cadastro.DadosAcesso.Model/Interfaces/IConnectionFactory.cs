using System.Data;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces
{
    public interface IConnectionFactory
    {
        IDbConnection CriaConexao();
    }
}
