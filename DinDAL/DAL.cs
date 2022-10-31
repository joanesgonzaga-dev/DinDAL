using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace DinDAL
{
    public abstract class DAL
    {
        static DbProviderFactory providerFactory;
        static string _connString;
        static bool isInitialized;

        public static void Init(string providerName, string connString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);
            _connString = connString;
            isInitialized = true;
        }

        public static void Init()
        {
            Init(ConfigurationManager.ConnectionStrings["providerName"].ProviderName, ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        protected DbCommand CreateConnection(DbConnection conn, string sqlText)
        {
            if (!isInitialized)
            {
                Init();
            }

            DbCommand cmd = providerFactory.CreateCommand();
            cmd.Connection = conn;
            cmd.CommandText = sqlText;
            return cmd;
        }

        protected DbParameter CreateParameter(DbCommand cmd, string paramName, object value, DbType? paramType)
        {
            DbParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.Value = value;

            if (paramType.HasValue)
            {
                parameter.DbType = paramType.Value;
            }

            cmd.Parameters.Add(parameter);

            return parameter;
        }
    }
}
