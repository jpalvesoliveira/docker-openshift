using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace Agora.Brokerage.Cadastro.DadosAcesso.Core.Repositories
{
    public class OracleDynamicParameters : SqlMapper.IDynamicParameters
    {
        private IDbCommand executedCommand;

        private readonly DynamicParameters dynamicParameters = new DynamicParameters();

        private readonly List<OracleParameter> oracleParameters = new List<OracleParameter>();

        public void Add(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null)
        {
            dynamicParameters.Add(name, value, dbType, direction, size);
        }

        public void Add(string name, OracleDbType oracleDbType, ParameterDirection direction, int size)
        {
            var oracleParameter = new OracleParameter(name, oracleDbType, direction);
            oracleParameter.Size = size;
            oracleParameters.Add(oracleParameter);
        }

        public void AddParameters(IDbCommand command, SqlMapper.Identity identity)
        {
            executedCommand = command;

            ((SqlMapper.IDynamicParameters)dynamicParameters).AddParameters(command, identity);

            var oracleCommand = command as OracleCommand;

            if (oracleCommand != null)
            {
                oracleCommand.Parameters.AddRange(oracleParameters.ToArray());
            }
        }

        public T Get<T>(string name)
        {
            return dynamicParameters.Get<T>(name);
        }

        public object GetOracleParameter(string name)
        {
            if (executedCommand != null)
            {
                var oracleCommand = executedCommand as OracleCommand;
                return oracleCommand.Parameters[name].Value;
            }
            return null;
        }

        public static implicit operator DynamicParameters(OracleDynamicParameters v)
        {
            throw new NotImplementedException();
        }
    }
}
