using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Repositories
{
    public class AssessoresRepositorio : RepositorioBase<Model.Models.Assessor>, IAssessoresRepositorio
    {
        const string GET_ASSESSORES_PROC = "AGR_CADASTRO.PKG_JA_DADOS_ACESSO.PR_GET_ASSESSORES";
        const string GET_ASSESSOR_PROC = "AGR_CADASTRO.PKG_JA_DADOS_ACESSO.PR_GET_DADOS_ASSESSOR";

        public AssessoresRepositorio(IConnectionFactory factory) : base(factory)
        {
        }
        public async Task<IEnumerable<Model.Models.Assessor>> Listar()
        {
            IEnumerable<Model.Models.Assessor> AssessoresRetorno = new List<Model.Models.Assessor>();
            OracleDynamicParameters parameters;

            try
            {
                AssessoresRetorno = await Task.Run(() =>
                {
                    using (OracleConnection conn = (OracleConnection)_factory.CriaConexao())
                    {
                        conn.Open();
                        parameters = new OracleDynamicParameters();
                        parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output, 0);

                        return conn.Query<dynamic>(GET_ASSESSORES_PROC, param: parameters, commandType: CommandType.StoredProcedure)
                            .Select(item => new Model.Models.Assessor()
                            {
                                Codigo = Convert.ToInt32(item.CD_ASSESSOR),
                                Nome = item.NO_ASSESSOR

                            }).ToList();

                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return AssessoresRetorno;
        }
    }
}
