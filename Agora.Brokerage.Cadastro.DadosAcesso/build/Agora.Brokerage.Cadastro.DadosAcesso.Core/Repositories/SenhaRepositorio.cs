using System;
using Dapper;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Threading.Tasks;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;
using Agora.Brokerage.Cadastro.DadosAcesso.Shared.Security;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Repositories
{
    public class SenhaRepositorio : RepositorioBase<Model.Models.Senha>, ISenhaRepositorio
    {
        const string CRIAR_SENHA_PROC = "AGR_CADASTRO.PKG_JA_DADOS_ACESSO.PR_SET_SENHA";
        const string BUSCAR_SENHA_PROC = "AGR_CADASTRO.PKG_JA_DADOS_ACESSO.PR_GET_SENHA";

        public SenhaRepositorio(IConnectionFactory factory) : base(factory)
        {

        }
        public async Task<bool> Create(Model.Models.Senha senha)
        {
            OracleDynamicParameters parameters;
            Crypto senhaCliente = new Crypto();

            try
            {
                await Task.Run(() =>
                {
                    using (OracleConnection conn = (OracleConnection)_factory.CriaConexao())
                    {
                        conn.Open();
                        parameters = new OracleDynamicParameters();
                        parameters.Add("pSenha", senha.Pwd);
                        parameters.Add("pCPF", senha.CPF);                       
                        parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output, 0);
                        conn.Query<dynamic>(CRIAR_SENHA_PROC, param: parameters, commandType: CommandType.StoredProcedure);
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> Read(string pCPF)
        {
            OracleDynamicParameters parameters;

            try
            {
                await Task.Run(() =>
                {
                    using (OracleConnection conn = (OracleConnection)_factory.CriaConexao())
                    {
                        conn.Open();
                        parameters = new OracleDynamicParameters();
                        parameters.Add("pCPF", pCPF);                       
                        parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output, 0);
                        conn.Query<dynamic>(BUSCAR_SENHA_PROC, param: parameters, commandType: CommandType.StoredProcedure);
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}
