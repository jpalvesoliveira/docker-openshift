using System;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Agora.Brokerage.Cadastro.DadosAcesso.Model.Interfaces;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Repositories
{
    public class DadosAcessoRepositorio : RepositorioBase<Model.Models.DadosAcesso>, IDadosAcessoRepositorio
    {

        const string CRIAR_DADOS_ACESSO_PROC = "AGR_CADASTRO.PKG_JA_DADOS_ACESSO.PR_SET_DADOS_ACESSO";
        const string GET_DADOS_ACESSO_PROC = "AGR_CADASTRO.PKG_JA_DADOS_ACESSO.PR_GET_DADOS_ACESSO";
        const string DELETE_DADOS_ACESSO_PROC = "AGR_CADASTRO.PKG_JA_DADOS_ACESSO.PR_DEL_DADOS_ACESSO";
        const string UPDATE_DADOS_ACESSO_PROC = "AGR_CADASTRO.PKG_JA_DADOS_ACESSO.PR_UPD_DADOS_ACESSO";

        public OracleConnection _cadastroConnection { get; set; }

        public DadosAcessoRepositorio(IConnectionFactory factory) : base(factory)
        {

        }

        public async Task<bool> Create(Model.Models.DadosAcesso dadosDeAcesso)
        {
            IEnumerable<Model.Models.DadosAcesso> DadosAcessoRetorno = new List<Model.Models.DadosAcesso>();
            OracleDynamicParameters parameters;
            bool valid = false;
            var pPessoa = "";
            switch (dadosDeAcesso.TipoPessoa)
            {
                case Model.Enums.ETipoPessoa.PF:
                    pPessoa = "PF";
                    break;
                case Model.Enums.ETipoPessoa.PJ:
                    pPessoa = "PJ";
                    break;
            }

            valid = await Task.Run(() =>
            {
                using (OracleConnection conn = (OracleConnection)_factory.CriaConexao())
                {
                    conn.Open();
                    try
                    {
                        parameters = new OracleDynamicParameters();
                        parameters.Add("pTipoPessoa", pPessoa.Substring(1, 1));
                        parameters.Add("pDataNascimento", dadosDeAcesso.DataNascimento);
                        parameters.Add("pNomeCliente", dadosDeAcesso.NomeCompleto);
                        parameters.Add("pEmail", dadosDeAcesso.Email);
                        parameters.Add("pCPF", dadosDeAcesso.CpfCnpj);
                        parameters.Add("pCodigoAssessor", dadosDeAcesso.Assessor);
                        parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output, 0);

                        conn.Query<dynamic>(CRIAR_DADOS_ACESSO_PROC, param: parameters, commandType: CommandType.StoredProcedure);
                    }
                    catch (Exception e)
                    {
                        valid = false;
                        Console.WriteLine(e);
                    }
                    valid = true;
                }
                return valid;
            });

            return valid;

        }

        public async Task<Model.Models.DadosAcesso> Read(long pCPF)
        {
            Model.Models.DadosAcesso DadosAcessoRetorno = new Model.Models.DadosAcesso();
            OracleDynamicParameters parameters;

            try
            {
                DadosAcessoRetorno = await Task.Run(() =>
                {
                    using (OracleConnection conn = (OracleConnection)_factory.CriaConexao())
                    {
                        conn.Open();
                        parameters = new OracleDynamicParameters();
                        parameters.Add("pCpfCgc", pCPF);
                        parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output, 0);

                        return conn.Query<dynamic>(GET_DADOS_ACESSO_PROC, param: parameters, commandType: CommandType.StoredProcedure)
                            .Select(item => new Model.Models.DadosAcesso()
                            {
                                TipoPessoa = (Model.Enums.ETipoPessoa) Enum.Parse(typeof(Model.Enums.ETipoPessoa), Convert.ToString("PF")),
                                CpfCnpj = item.CD_CPFCGC,
                                NomeCompleto = item.NM_CLIENTE,
                                DataNascimento = item.DT_NASC_FUND,
                                Assessor = item.CD_ASSESSOR,
                                Email = item.NM_EMAIL
                            }).FirstOrDefault();
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return DadosAcessoRetorno;
        }

        public async Task<bool> Update(Model.Models.DadosAcesso dadosDeAcesso)
        {
            IEnumerable<Model.Models.DadosAcesso> DadosAcessoRetorno = new List<Model.Models.DadosAcesso>();
            OracleDynamicParameters parameters;
            var valid = false;
            string pPessoa = (dadosDeAcesso.TipoPessoa == Model.Enums.ETipoPessoa.PF) ? "F" : "J";

            valid = await Task.Run(() =>
            {
                using (OracleConnection conn = (OracleConnection)_factory.CriaConexao())
                {
                    conn.Open();
                    try
                    {
                        parameters = new OracleDynamicParameters();
                        parameters.Add("pCPF", dadosDeAcesso.CpfCnpj);
                        parameters.Add("pAssessor", dadosDeAcesso.Assessor);
                        parameters.Add("results", OracleDbType.RefCursor, ParameterDirection.Output, 0);

                        conn.Query<dynamic>(UPDATE_DADOS_ACESSO_PROC, param: parameters, commandType: CommandType.StoredProcedure);
                    }
                    catch (Exception e)
                    {
                        valid = false;
                        Console.WriteLine(e.Message);
                    }
                }
                return valid;
            });
            return valid;
        }

        public async Task<bool> Delete(long pCPF)
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
                        conn.Query<dynamic>(DELETE_DADOS_ACESSO_PROC, param: parameters, commandType: CommandType.StoredProcedure);

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
