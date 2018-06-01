using System.Data;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Factories
{
    public class ConexaoFactory : IConnectionFactory
    {
        public ConexaoFactory()
        {
        }

        public IDbConnection CriaConexao()
        {
            return new OracleConnection(System.Environment.GetEnvironmentVariable("ConnectionString"));
        }
    }
}
