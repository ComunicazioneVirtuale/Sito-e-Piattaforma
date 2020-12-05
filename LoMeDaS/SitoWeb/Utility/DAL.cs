using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SitoWeb.Utility
{
    public enum DAObjectType
    {
        DataTable,
        DataReader,
        Scalar,
        Function,
        ResultNonQuery,
        SQLSchema
    }

    public class DAObject : IDisposable
    {
        public DAObjectType Type { get; private set; }
        public int? RetCode { get; private set; }
        public string ErrCode { get; private set; }
        public string Message { get; private set; }
        public bool isOk { get; private set; }
        public IDataReader ResultRecordSet { get; private set; }
        public DataTable ResultRecordSetDataTable { get; private set; }
        public DbParameterCollection ParametersOut { get; private set; }
        public string ResultScalar { get; private set; }
        public Int64? ResultNonQuery { get; private set; }
        public Int64? Identity { get; private set; }

        public DAObject(DAObjectType _type, object _data, bool _isOk = true, string _message = "", int? _RetCode = null, string _ErrCore = "", DbParameterCollection _parametersOut = null, Int64? _Identity = null)
        {
            Type = _type;
            RetCode = _RetCode;
            ErrCode = _ErrCore;
            Message = _message;
            isOk = _isOk;
            ParametersOut = _parametersOut;
            Identity = _Identity;

            switch (_type)
            {
                case DAObjectType.DataTable:
                case DAObjectType.SQLSchema:
                    ResultRecordSetDataTable = (DataTable)_data;
                    break;
                case DAObjectType.DataReader:
                    ResultRecordSet = (IDataReader)_data;
                    break;
                case DAObjectType.Scalar:
                    ResultScalar = (string)_data;
                    break;
                case DAObjectType.ResultNonQuery:
                    ResultNonQuery = (int)_data;
                    break;
                default:
                    break;
            }
        }

        public void Dispose() { }
    }

    public class DAL
    {
        public static DAObject ExecuteQueryDataTable(string query, bool isStroreProcedure = false, Dictionary<string, object> patameters = null, SqlConnection conn = null)
        {
            DataTable dt = new DataTable("myTable");
            SqlCommand cmd = null;
            SqlConnection connNew = null;

            try
            {
                connNew = OpenConnection(conn);
                cmd = MakeCommand(query, isStroreProcedure, patameters, connNew);

                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(dt);

                return MakeDAObject(DAObjectType.DataTable, dt, cmd.Parameters);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            finally
            {
                CloseConnection(connNew);
            }
        }

        public static DAObject ExecuteQueryDataReader(string query, bool isStroreProcedure = false, Dictionary<string, object> patameters = null, SqlConnection conn = null, SqlTransaction trx = null)
        {
            SqlDataReader reader = null;
            SqlConnection connNew = null;
            SqlCommand cmd = null;

            try
            {
                connNew = OpenConnection(conn);
                cmd = MakeCommand(query, isStroreProcedure, patameters, connNew, trx);

                reader = cmd.ExecuteReader();

                return MakeDAObject(DAObjectType.DataReader, reader, cmd.Parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(connNew, trx);
            }
        }

        public static DAObject ExecuteScalar(string query, bool isStroreProcedure = false, Dictionary<string, object> patameters = null, SqlConnection conn = null, SqlTransaction trx = null)
        {
            string retVal = string.Empty;
            SqlConnection connNew = null;
            SqlCommand cmd = null;

            try
            {
                connNew = OpenConnection(conn);
                cmd = MakeCommand(query, isStroreProcedure, patameters, connNew, trx);

                retVal = (cmd.ExecuteScalar() != null) ? cmd.ExecuteScalar().ToString() : string.Empty;
                return MakeDAObject(DAObjectType.Scalar, retVal, cmd.Parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(connNew, trx);
            }
        }

        public static DAObject ExecuteNonQuery(string query, bool isStroreProcedure = false, Dictionary<string, object> patameters = null, SqlConnection conn = null, SqlTransaction trx = null)
        {
            Int64? sqlIdentity = null;
            SqlConnection connNew = null;
            SqlCommand cmd = null;

            try
            {
                connNew = OpenConnection(conn);
                cmd = MakeCommand(query, isStroreProcedure, patameters, connNew, trx);
                int countInst = cmd.ExecuteNonQuery();

                string sIdentity = ExecuteScalar("select @@IDENTITY", false, null, connNew, trx).ResultScalar;

                if (sIdentity.isNotNullOrEmpty())
                {
                    sqlIdentity = Convert.ToInt32(sIdentity);
                }


                return MakeDAObject(DAObjectType.ResultNonQuery, countInst, cmd.Parameters, sqlIdentity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection(connNew, trx);
            }
        }

        public static DAObject getSchemaInfo(string nomeTabella)
        {
            IDbCommand myCommand = null;
            IDataReader myReader = null;

            string query = string.Format("SELECT * FROM {0} where 1=0", nomeTabella.Trim());
            SqlConnection connNew = MakeDefaultConnection();
            myCommand = new SqlCommand(query, connNew);

            connNew.Open();
            myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.KeyInfo);
            DataTable dtInfo = new DataTable();
            dtInfo = myReader.GetSchemaTable();
            myReader.Close();

            return MakeDAObject(DAObjectType.SQLSchema, dtInfo);
        }
        private static SqlCommand MakeCommand(string query, bool isStroreProcedure = false, Dictionary<string, object> patameters = null, SqlConnection conn = null, SqlTransaction trx = null)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandTimeout = 9600;

            if (isStroreProcedure)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //List of all parameters from DB
                SqlCommandBuilder.DeriveParameters(cmd);

                //Init output params
                foreach (DbParameter parameter in cmd.Parameters)
                {
                    if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
            }
            else
            {
                cmd.CommandType = CommandType.Text;
            }

            if (patameters != null && patameters.Count() > 0)
            {
                foreach (var item in patameters)
                {
                    //Fix check start with @value
                    string key = item.Key.ToString();
                    if (!key.StartsWith("@"))
                    {
                        key = "@" + key;
                    }

                    if (cmd.Parameters.Contains(key))
                    {
                        cmd.Parameters[key].Value = item.Value;
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue(key, item.Value);
                    }
                }
            }

            if (trx != null)
                cmd.Transaction = trx;

            return cmd;
        }

        private static SqlConnection MakeDefaultConnection()
        {
            string connectionstring = @"Data Source=DESKTOP-2V29D5E\SQLEXPRESS;Initial Catalog=LoMeDaS;Integrated Security=True";
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionstring);
            sqlConnectionStringBuilder.ConnectTimeout = 9600;
            var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            return conn;
        }

        private static SqlConnection OpenConnection(SqlConnection cn)
        {
            SqlConnection newConn = null;

            if (cn != null)
            {
                newConn = cn;
            }
            else
            {
                newConn = MakeDefaultConnection();
            }

            if (newConn != null && newConn.State != ConnectionState.Open)
            {
                newConn.Open();
            }

            return newConn;
        }

        private static void CloseConnection(SqlConnection newConn, SqlTransaction trx = null)
        {
            //Close ONLY NOT in transaction
            if (trx == null)
            {
                if (newConn != null && newConn.State != ConnectionState.Closed)
                {
                    newConn.Close();
                }
            }
        }

        private static DAObject MakeDAObject(DAObjectType type, object data, DbParameterCollection parameters = null, Int64? sqlIdentity = null)
        {
            string errCode = string.Empty;
            string message = string.Empty;
            int retCode = 0;
            bool isOk = true;

            //Verifica return code
            if (parameters != null)
            {
                if (parameters.Contains("@RETURN_VALUE"))
                {
                    retCode = Convert.ToInt16(parameters["@RETURN_VALUE"].Value);
                    isOk = retCode == 0;
                }

                if (parameters.Contains("@c_cod_err"))
                {
                    errCode = (parameters["@c_cod_err"].Value != null) ? parameters["@c_cod_err"].Value.ToString() : "";
                }

                if (parameters.Contains("@c_cod_err"))
                {
                    message = (parameters["@c_desc_err"].Value != null) ? parameters["@c_desc_err"].Value.ToString() : "";
                }
            }

            DAObject daObj = new DAObject(type, data, isOk, message, retCode, errCode, parameters, sqlIdentity);
            return daObj;
        }
    }
}
